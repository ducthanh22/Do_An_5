using DTO;
using Microsoft.AspNetCore.Http;
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
        Task<bool> UpdateRole(CreateRoleDto role);
        Task<GennToken> GenerateToken(UserDto user);
        Task<bool> Register(CreateUserDto user);
        Task<bool> Login(UserDto user);
        Task<ForgotPasswordModel> ForgotPassword(ForgotPasswordModel model);
        Task<string> ResetPassword(ResetPasswordModel model);
        Task<List<Role>> GetAllRoles();
        Task<CreateRoleDto> getClaimByIdRole(string id);
        Task<bool> DeleteRole(string id);
        Task<BaseQuerieResponse<User>> GetUser(string status, Paging paging);

    }
}
