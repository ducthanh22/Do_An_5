
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

namespace DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly Achino_DbContext _dbContext;
        private readonly IConfiguration _config;
        public AccountRepository(UserManager<User> userManager, Achino_DbContext dbContext,IConfiguration config   )
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _config = config;
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

        public async Task<bool> Login(UserDto user)
        {
            var checkUser = await _userManager.FindByNameAsync(user.UserName);
            if (checkUser is null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(checkUser, user.PasswordHash);
        }

        public string GenerateToken(UserDto user)
        {
         
            var claims= new List<Claim>
            {
             
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Username",user.UserName),

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
