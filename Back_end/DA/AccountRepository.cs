
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
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;

namespace DAL
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly Achino_DbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ISendEmailRepository _sendEmailRepository;
        //private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;


        public AccountRepository(
            UserManager<User> userManager,
            Achino_DbContext dbContext,
            IConfiguration config,
            RoleManager<Role> roleManager,
            IHttpContextAccessor httpContextAccessor,
            ISendEmailRepository sendEmailRepository,
            //IUrlHelper urlHelper,
            LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _config = config;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _sendEmailRepository = sendEmailRepository;
            //_urlHelper = urlHelper;
            _linkGenerator = linkGenerator;
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


        public async Task<bool> Register(User user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                CCCD= user.CCCD,
                Address = user.Address,
                Status = user.Status,
                
            };
            var result = await _userManager.CreateAsync(newUser, user.PasswordHash);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Staff");
                await _dbContext.SaveChangesAsync();
                return true;
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
        public async Task<string> ForgotPassword(ForgotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var newtoken= Uri.EscapeDataString(token);

                var email = await _userManager.GetEmailAsync(user);

                //var callbackUrl = _linkGenerator.GetUriByAction(
                //    _httpContextAccessor.HttpContext,
                //    action: "ResetPassword",
                //    controller: "Account",
                //    values: new { token  });
                var callbackUrl = "http://localhost:4200/ResetPassword/" + newtoken +"/"+ email;
                // Gửi email
                await _sendEmailRepository.SendEmailAsync(model.Email, "Reset Password",
                    $"Vui lòng đặt lại mật khẩu của bạn bằng cách nhấp vào đây: <a href='{callbackUrl}'>link</a>");
            }
            return "Email sent successfully.";
        }
        public async Task<string> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var decodedToken = Uri.UnescapeDataString(model.Token);
                var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
                if (result.Succeeded)
                {
                    return "Password reset successfully.";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        return error.Description.ToString();
                    }
                }
            }
            return "Không tìm thấy người dùng";
        }






    }
}
