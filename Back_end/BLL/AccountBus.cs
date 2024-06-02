using BLL.Interface;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DTO.RoleDto;

namespace BLL
{
    public class AccountBus : IAccountBus
    {
        private readonly IAccountRepository _accountRepository;
        public AccountBus(IAccountRepository accountRepository)
        {

            _accountRepository = accountRepository;
        }
        public async Task<bool> Register(CreateUserDto user)
        {
            return await _accountRepository.Register(user);
        }
        public async Task<bool> Login(UserDto user)
        {
            return await _accountRepository.Login(user);
        }
        public async Task<GennToken> GenerateToken(UserDto user)
        {
            return await _accountRepository.GenerateToken(user);
        }
        public async Task<bool> CreateRoleAsync(CreateRoleDto role)
        {
            return await _accountRepository.CreateRoleAsync(role);
        }
        public async Task<ForgotPasswordModel> ForgotPassword(ForgotPasswordModel model)
        {
            return await _accountRepository.ForgotPassword(model);
        }
        public async Task<ResetPasswordModel> ResetPassword(ResetPasswordModel model)
        {
            return await _accountRepository.ResetPassword(model);
        }
        public async Task<List<Role>> GetAllRoles()
        {
            return await _accountRepository.GetAllRoles();
        }
        public async Task<CreateRoleDto> getClaimByIdRole(string id)
        {
            return await _accountRepository.getClaimByIdRole(id);
        }
        public async Task<bool> UpdateRole(CreateRoleDto role)
        {
            return await _accountRepository.UpdateRole(role);
        }
        public async Task<bool> DeleteRole(string id)
        {
            return await _accountRepository.DeleteRole(id);
        }
        public async Task<BaseQuerieResponse<User>> GetUser(string status, Paging paging)
        {
            return await _accountRepository.GetUser(status, paging);
        }
        public async Task<updateUserDto> updateUser(updateUserDto model)
        {
            return await _accountRepository.updateUser(model);
        }
    }
}
