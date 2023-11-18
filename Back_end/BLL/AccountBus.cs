using BUS.Interface;
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

namespace BUS
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
        public async Task<bool> Login(User user)
        {
            return await _accountRepository.Login(user);
        }
        public  string GenerateToken(User user1)
        {
            return  _accountRepository.GenerateToken(user1);
        }
        public Task<bool> CreateRoleAsync(CreateRoleDto role)
        {
            return _accountRepository.CreateRoleAsync(role);
        }
    }
}
