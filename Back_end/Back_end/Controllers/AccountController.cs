using AutoMapper;
using BUS.Interface;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Security.Claims;
using static DTO.RoleDto;

namespace Back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        public IMapper _mapper;
        public IAccountBus _Bus;
        public AccountController(IAccountBus Bus, IMapper mapper)
        {
            _Bus = Bus;
            _mapper = mapper;
        }
        [AllowAnonymous]

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register(User user)
        {
            var result = await _Bus.Register(user);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("CreateRole")]
        public async Task<ActionResult<bool>> CreateRoleAsync(CreateRoleDto role)
        {
            var result = await _Bus.CreateRoleAsync(role);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(UserDto user)
        {
            var result = await _Bus.Login(user);
            
            if(result==true)
            {             
                var token = await _Bus.GenerateToken(user);
                return Ok(token);
            }

            return BadRequest();
        }

        [AllowAnonymous]

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<string>> ForgotPassword([FromForm] ForgotPasswordModel user)
        {
            var result = await _Bus.ForgotPassword(user);


            return Ok("ForgotPassword success");
        }
    }
}
