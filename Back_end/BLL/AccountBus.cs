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
        public async Task<bool> Register(User user)
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
        public async Task<string> ResetPassword(ResetPasswordModel model)
        {
            return await _accountRepository.ResetPassword(model);
        }
    }
}
