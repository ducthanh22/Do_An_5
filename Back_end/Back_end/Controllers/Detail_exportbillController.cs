using Model;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Back_end.Attribute;
using DTO.Enum;

namespace Back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Detail_exportbillController : ControllerBase
    {
        public IDetail_exportbillBus _Bus;
        public Detail_exportbillController(IDetail_exportbillBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Detail_exportbill>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<GetDetail_exportbillDto>>> Getbyid(Guid id)
        {
            var result = await _Bus.GETBYID(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<Detail_exportbillDto>> Create([FromBody] Detail_exportbill dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Detail_exportbillDto>> Update([FromBody] Detail_exportbill dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Detail_exportbillDto>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("CountProduct/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<countProduct>> CountProduct(Guid id)
        {
            var result = await _Bus.CountProduct(id);
            return Ok(result);
        }

    }
}
