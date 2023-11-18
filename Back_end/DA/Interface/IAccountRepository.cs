using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DTO.RoleDto;

namespace DAL.Interface
{
    public interface IAccountRepository 
    {
        Task<bool> CreateRoleAsync(CreateRoleDto role);
        string GenerateToken(User user1);
        Task<bool> Register(User user);
        Task<bool> Login(User user);
    }
}
