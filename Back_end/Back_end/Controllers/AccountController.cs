using BUS.Interface;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        public IAccountBus _Bus;
        public AccountController(IAccountBus Bus) 
        {
            _Bus = Bus;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register(User user)
        {
            var result = await _Bus.Register(user);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(UserDto user)
        {
            var result = await _Bus.Login(user);
           
            if(result==true)
            {
                var token = _Bus.GenerateToken(user);
                return Ok(token);
            }

            return BadRequest();
        }
    }
}
