using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;


namespace Back_end.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        public IPriceBus _Bus;
        public PriceController(IPriceBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PriceDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<PriceDto>> Getbyid(int id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<PriceDto>> Create([FromBody] Price dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<PriceDto>> Update([FromBody] Price dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<PriceDto>> Delete(int id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] int? min, [FromQuery] int? max, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _Bus.Search(min, max, page, pageSize);

            return Ok(result);
        }
    }
}
