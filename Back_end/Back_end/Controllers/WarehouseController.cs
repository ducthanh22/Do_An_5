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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        public IWarehouseBus _Bus;
        public WarehouseController(IWarehouseBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Module.QlKh }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<WarehouseDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<WarehouseDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<WarehouseDto>> Create([FromBody] Warehouse dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<WarehouseDto>> Update([FromBody] Warehouse dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<WarehouseDto>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string? keywork, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            
            var result = await _Bus.Search(keywork, page, pageSize);
            

            return Ok(result);
        }
    }
}
