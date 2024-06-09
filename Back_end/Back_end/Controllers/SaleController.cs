using Back_end.Attribute;
using BLL.Interface;
using DTO;
using DTO.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        public ISaleBus _Bus;
        public SaleController(ISaleBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<Sale>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }
        [HttpGet("GetSale")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<GetSaleDto>> >GetSale(string? keyword, int active)
        {
            var result = await _Bus.GetSale(keyword,active);
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<Sale>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }
      

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<List<SaleDto>>> CREATE(List<SaleDto> dto)
        {
            var createdEntity = await _Bus.CREATE(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Sale>> Update([FromBody] Sale dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Sale>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("UpdateSalesPrices")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> UpdateSalesPrices()
        {
            var result = await _Bus.UpdateSalesPrices();
            return Ok(result);
        }
    }
}
