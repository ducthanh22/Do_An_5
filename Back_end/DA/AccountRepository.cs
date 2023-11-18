
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
using static DTO.RoleDto;

namespace DAL
{
    public class AccountRepository : IAccountRepository
    {
       
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly Achino_DbContext _dbContext;
        private readonly IConfiguration _config;
    
        
        public AccountRepository(UserManager<User> userManager, Achino_DbContext dbContext,IConfiguration config  , RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _config = config;
            _roleManager = roleManager;
            
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
            };
            var result = await _userManager.CreateAsync(newUser, user.PasswordHash);
            if (result.Succeeded)
            {     
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Login(User user)
        {
            var checkUser = await _userManager.FindByNameAsync(user.UserName);

            if (checkUser is null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(checkUser, user.PasswordHash);
        }

        public string GenerateToken(User user)
        {
          
            var claims= new List<Claim>
            {
                new Claim("Username",user.UserName),
                new Claim("pass",user.PasswordHash),
                new Claim("id",user.Id),
                new Claim("Email",user.Email)
            };
            
            var SecurityKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var SigningCred = new SigningCredentials (SecurityKey,SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials:SigningCred
                ) ;


            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
