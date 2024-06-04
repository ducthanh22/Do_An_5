using BLL.Interface;
using DAL.Interface;
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
    public class Order_detailController : ControllerBase
    {
        public IOder_detailBus _Bus;
        public Order_detailController(IOder_detailBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Order_detailDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<Order_detailDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<Order_detail>> Create([FromBody] Order_detail dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Order_detail>> Update([FromBody] Order_detail dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Order_detail>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
    }
}
