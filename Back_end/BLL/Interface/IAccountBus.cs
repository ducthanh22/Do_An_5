﻿using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DTO.RoleDto;

namespace BUS.Interface
{
    public interface IAccountBus
    {
        string GenerateToken (User user1);
        Task<bool> Register(User user);
        Task<bool> Login(User user);
        Task<bool> CreateRoleAsync(CreateRoleDto role);
    }
}