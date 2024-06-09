using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;
using Back_end.Attribute;
using DTO.Enum;

namespace Back_end.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProducesController : ControllerBase
    {
        public IProducesBus _Bus;
        public ProducesController(IProducesBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProducesDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [HasPermission(new[] { (int)EnumModule.Module.QlNcc }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<ProducesDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlNcc }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<ProducesDto>> Create([FromBody] Produces dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlNcc }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<ProducesDto>> Update([FromBody] Produces dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlNcc }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<ProducesDto>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        [HasPermission(new[] { (int)EnumModule.Module.QlNcc }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<IActionResult> Search([FromQuery] string? keywork, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _Bus.Search(keywork, page, pageSize);

            return Ok(result);
        }
        [HttpPost("UploadFile")]
        [HasPermission(new[] { (int)EnumModule.Module.QlNcc }, new[] { (int)EnumPermission.Type.Create })]
        public async Task<ActionResult<UpFile>>Uploadfile([FromForm]UpFile upFile)
        {
            var result = await _Bus.UpImg(upFile);
            return Ok(result);
        }
    }
}
