using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Back_end.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<List<ProducesDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<ProducesDto>> Getbyid(int id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProducesDto>> Create([FromBody] Produces dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<ProducesDto>> Update([FromBody] Produces dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<ProducesDto>> Delete(int id)
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
