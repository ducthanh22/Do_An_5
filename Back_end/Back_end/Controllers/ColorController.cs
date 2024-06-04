using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using DTO;
using Back_end.Attribute;
using DTO.Enum;


namespace Back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        public IColorBus _Bus;
        public ColorController(IColorBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Color>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [AllowAnonymous]

        public async Task<ActionResult<Color>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.Dashboard }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<Color>> Create([FromBody] Color dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.Dashboard }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Color>> Update([FromBody] Color dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.Dashboard }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Color>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        [HasPermission(new[] { (int)EnumModule.Module.Dashboard }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<Color>> Search([FromQuery] Paging paging)
        {
            var result = await _Bus.Search(paging);
            return Ok(result);
        }

    }
}
