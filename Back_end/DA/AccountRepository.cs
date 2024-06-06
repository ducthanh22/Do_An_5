
using DAL.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.NetworkInformation;


namespace DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly Achino_DbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ISendEmailRepository _sendEmailRepository;

        public AccountRepository(
            UserManager<User> userManager,
            Achino_DbContext dbContext,
            IConfiguration config,
            RoleManager<Role> roleManager,
            ISendEmailRepository sendEmailRepository
)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _config = config;
            _roleManager = roleManager;
            _sendEmailRepository = sendEmailRepository;
        }
        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
        public async Task<CreateRoleDto>getClaimByIdRole(string id)
        {
            var existRole = await _roleManager.FindByIdAsync(id);

            var claim = await _roleManager.GetClaimsAsync(existRole);

            var createRoleDto = new CreateRoleDto
            {
                Role = new RoleDto 
                {
                    Id=existRole.Id,
                    Name = existRole.Name
                },
                RoleClaims = claim.Select(claim => new ClaimDto
                {
                    Type = claim.Type,
                    Value = claim.Value
                }).ToList()
            };
            return createRoleDto ;
        }

        public async Task<BaseQuerieResponse<User>> GetUser(string status, Paging paging)
        {
            var users = from user in _userManager.Users
                        where (user.Status == status && string.IsNullOrEmpty(paging.Keyword)||user.Status == status && user.Email.Contains(paging.Keyword)|| user.Status == status && user.UserName.Contains(paging.Keyword)
                        || user.Status == status && user.PhoneNumber.Contains(paging.Keyword))
                        select new User
                        {
                            Id= user.Id,
                            UserName= user.UserName,
                            Status= user.Status,
                            Email= user.Email,
                            Address= user.Address,
                            PhoneNumber= user.PhoneNumber,
                            CCCD= user.CCCD,
                        };
            var totalCount = await users.LongCountAsync();
            var pageResults = await users.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<User>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                Keyword = paging.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }


        public async Task<bool> CreateRoleAsync(CreateRoleDto role)
        {
            // Check if the role already exists
            var roleExists = await _roleManager.RoleExistsAsync(role.Role.Name);
            // If not, create the role
            if (!roleExists)
            {
                var roleCreate = new Role
                {
                    Name = role.Role.Name,
                };
                // Create the role
                var result = await _roleManager.CreateAsync(roleCreate);
                if (result.Succeeded)
                {
                    foreach (var item in role.RoleClaims)
                    {
                        var claim = new Claim(item.Type, item.Value);
                         await _roleManager.AddClaimAsync(roleCreate, claim); 
                    }
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> UpdateRole(CreateRoleDto role)
        {
            try
            {
                var roleExists = await _roleManager.FindByIdAsync(role.Role.Id);
                if (roleExists != null)
                {
                    if (roleExists.Name != role.Role.Name)
                    {
                        roleExists.Name = role.Role.Name;
                        var updateResult = await _roleManager.UpdateAsync(roleExists);

                        if (!updateResult.Succeeded)
                        {
                            return false;
                        }
                    }
                    var existingClaims = await _roleManager.GetClaimsAsync(roleExists);
                    foreach (var existingClaim in existingClaims)
                    {
                        await _roleManager.RemoveClaimAsync(roleExists, existingClaim);
                    }
                    foreach (var roleClaim in role.RoleClaims)
                    {
                        var claim = new Claim(roleClaim.Type, roleClaim.Value);
                        var addClaimResult = await _roleManager.AddClaimAsync(roleExists, claim);

                        if (!addClaimResult.Succeeded)
                        {
                            return false;
                        }
                    }

                    return true; // Role updated successfully
                }

                return false; // Role does not exist
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool>DeleteRole(string id)
        { 
          var role = await _roleManager.FindByIdAsync(id);
            if(role != null)
            {
                await _roleManager.DeleteAsync(role);
                return true;
            }
            return false;
        }

        public async Task<bool> Register(CreateUserDto user)
        {
            var Check = await _userManager.FindByEmailAsync(user.Email);
            if(Check == null ) 
            {
                var newUser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    CCCD = user.CCCD,
                    Address = user.Address,
                    Status = user.Status,
                    PhoneNumber=user.PhoneNumber,
                    Created= DateTime.Now,
                };
                var result = await _userManager.CreateAsync(newUser, user.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, user.roleName);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Login(UserDto user)
        {
            var checkUser = await _userManager.FindByEmailAsync(user.Email);

            if (checkUser is null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(checkUser, user.PasswordHash);
        }

        public async Task<GennToken> GenerateToken(UserDto user)
        {
            var checkUser = await _userManager.FindByEmailAsync(user.Email);
            // Tạo danh sách claims với thông tin cơ bản
            var claims = new List<Claim>
            {
                new Claim("Username", checkUser.UserName),
                new Claim("Id", checkUser.Id),
                new Claim("Email", checkUser.Email),
                new Claim("Address", checkUser.Address),
                new Claim("Phone", checkUser.PhoneNumber),
                new Claim("status", checkUser.Status),
            };
            var roles = await _userManager.GetRolesAsync(checkUser);
            // Thêm các claims về vai trò vào danh sách claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                if (role != null)
                {
                    // Lấy danh sách Claims của vai trò
                    var claim = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(role));
                    // Thêm các Claims vào danh sách roleClaims
                    claims.AddRange(claim);
                }
            }
            // Tạo mã khóa bảo mật từ cấu hình
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            // Tạo các thông tin xác thực
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            // Tạo token với các thông tin cần thiết
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(365),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);
            // Trả về chuỗi token
            var token= new JwtSecurityTokenHandler().WriteToken(securityToken);
            return new GennToken
            {
                UserName= checkUser.UserName,
                Token=token,
            };
        }
        public async Task<ForgotPasswordModel> ForgotPassword(ForgotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null )
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var newtoken= Uri.EscapeDataString(token);
                var email = await _userManager.GetEmailAsync(user);
                var callbackUrl = "http://localhost:4200/ResetPassword/" + newtoken +"/"+ email;
                // Gửi email
                await _sendEmailRepository.SendEmailAsync(model.Email, "Đặt lại mật khẩu",
                    $"Vui lòng đặt lại mật khẩu của bạn bằng cách nhấp vào đây: <a href='{callbackUrl}'>link</a>");
            }
            else {
                return new ForgotPasswordModel { Email = "Email không tồn tại" };
            }
            return new ForgotPasswordModel { Email=user.Email};
        }

        public async Task<ResetPasswordModel> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var decodedToken = Uri.UnescapeDataString(model.Token);
                var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
                if (result.Succeeded)
                {
                    return new ResetPasswordModel
                    {
                        Email=model.Email,
                        Token=model.Token,
                        NewPassword=model.NewPassword
                    };
                }
                else
                {
                    return null;
                }
            }
            return  null; 
        }

        public async Task<updateUserDto> updateUser(updateUserDto model)
        {
            var checkUser= await _dbContext.User.FindAsync(model.Id);
            if(checkUser != null)
            {
                checkUser.Address = model.Address;
                checkUser.CCCD = model.CCCD;
                checkUser.UserName = model.UserName;
                checkUser.Modified = DateTime.Now;
                checkUser.PhoneNumber = model.PhoneNumber;
                // Đánh dấu người dùng đã thay đổi
                _dbContext.User.Update(checkUser);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                return model;
            }
            else
            {
                return null;
            }
        }




    }
}
