﻿using DTO;
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
        Task<bool> Register(User user);
        Task<bool> Login(UserDto user);
        Task<bool> CreateRoleAsync(CreateRoleDto role);
        Task<ForgotPasswordModel> ForgotPassword(ForgotPasswordModel model);
        Task<string> ResetPassword(ResetPasswordModel model);

    }
}
