using AutoMapper;
using BLL.Interface;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Security.Claims;


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
        public async Task<ActionResult<Response>> Register(CreateUserDto user)
        {
            var result = await _Bus.Register(user);
            return Ok(result);
        }

        [AllowAnonymous]

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<List<Role>>> GetAllRoles()
        {
            var result = await _Bus.GetAllRoles();
            return Ok(result);
        }
        [AllowAnonymous]

        [HttpGet("GetUser/{status}")]
        public async Task<ActionResult<List<Role>>> GetUser( string status , [FromQuery]  Paging paging)
        {
            var result = await _Bus.GetUser(status,paging);
            return Ok(result);
        }

        [HttpGet("getClaimByIdRole/{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<CreateRoleDto>> getClaimByIdRole(string id)
        {
            var result = await _Bus.getClaimByIdRole(id);
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
        [HttpPost("UpdateRole")]
        public async Task<ActionResult<bool>> UpdateRole(CreateRoleDto role)
        {
            var result = await _Bus.UpdateRole(role);

            return Ok(result);
        }
        [HttpDelete("DeleteRole/{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<bool>> DeleteRole(string id)
        {
            var result = await _Bus.DeleteRole(id);
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
        public async Task<ActionResult<ForgotPasswordModel>> ForgotPassword([FromBody] ForgotPasswordModel user)
        {
            var result = await _Bus.ForgotPassword(user);


            return Ok(result);
        }
        [AllowAnonymous]

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ResetPasswordModel>> ResetPassword([FromBody] ResetPasswordModel user)
        {
            var result = await _Bus.ResetPassword(user);


            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPatch("update")]

        public async Task<ActionResult<updateUserDto>> updateUser([FromBody ] updateUserDto user)
        {
            var result = await _Bus.updateUser(user);


            return Ok(result);
        }
    }
}
