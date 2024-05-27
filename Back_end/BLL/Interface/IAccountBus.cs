using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DTO.RoleDto;

namespace BLL.Interface
{
    public interface IAccountBus
    {
         Task<GennToken> GenerateToken (UserDto user);
        Task<bool> Register(CreateUserDto user);
        Task<bool> Login(UserDto user);
        Task<bool> CreateRoleAsync(CreateRoleDto role);
        Task<bool> UpdateRole(CreateRoleDto role);
        Task<ForgotPasswordModel> ForgotPassword(ForgotPasswordModel model);
        Task<string> ResetPassword(ResetPasswordModel model);

        Task<List<Role>> GetAllRoles();
        Task<CreateRoleDto> getClaimByIdRole(string id);
        Task<bool> DeleteRole(string id);
        Task<BaseQuerieResponse<User>> GetUser(string status, Paging paging);
    }
}
