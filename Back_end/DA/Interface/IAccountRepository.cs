using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Interface
{
    public interface IAccountRepository 
    {
        Task<bool> CreateRoleAsync(CreateRoleDto role);
        Task<GennToken> GenerateToken(UserDto user);
        Task<bool> Register(User user);
        Task<bool> Login(UserDto user);
    }
}
