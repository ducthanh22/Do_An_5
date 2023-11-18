using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    public interface IAccountBus
    {
        string GenerateToken (UserDto user);
        Task<bool> Register(User user);
        Task<bool> Login(UserDto user);
    }
}
