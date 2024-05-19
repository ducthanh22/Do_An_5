using BLL.Interface;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Back_end.Controllers
{
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
        public async Task<ActionResult<List<Sale>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }
        [HttpGet("GetSale")]
        public async Task<ActionResult<List<GetSaleDto>> >GetSale(string? keyword, int active)
        {
            var result = await _Bus.GetSale(keyword,active);
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<Sale>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }
      

        [HttpPost("create")]
        public async Task<ActionResult<List<SaleDto>>> CREATE(List<SaleDto> dto)
        {
            var createdEntity = await _Bus.CREATE(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<Sale>> Update([FromBody] Sale dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<Sale>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("UpdateSalesPrices")]
        public async Task<ActionResult<int>> UpdateSalesPrices()
        {
            var result = await _Bus.UpdateSalesPrices();
            return Ok(result);
        }
    }
}
