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
    public class Detail_warehouseController : ControllerBase
    {
      
        public IDetail_warehouseBus _Bus;
        public Detail_warehouseController(IDetail_warehouseBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]

        public async Task<ActionResult<List<Detail_warehouseDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<Detail_warehouseDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<Detail_warehouseDto>> Create([FromBody] Detail_warehouse dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Detail_warehouseDto>> Update([FromBody] Detail_warehouse dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Detail_warehouseDto>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }

        [HttpGet("CountProduct")]
        [AllowAnonymous]
        public async Task<ActionResult<countProduct>> CountProduct([FromQuery] Guid id, [FromQuery] Guid idSize)
        {
            var result = await _Bus.CountProduct(id, idSize);
            return Ok(result);
        }
        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<ActionResult<GetDetail_warehouseDto>> Search([FromQuery] Paging paging)
        {
            var result = await _Bus.Search(paging);
            return Ok(result);
        }
    }
}
