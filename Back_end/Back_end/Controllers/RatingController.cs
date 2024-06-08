using BLL.Interface;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Back_end.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        public IRatingBus _Bus;
        public RatingController(IRatingBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Rating>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<Rating>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }
        [HttpGet("GetByProduct")]
        public async Task<IActionResult> Search([FromQuery] Guid id, [FromQuery] int page , [FromQuery] int pageSize)
        {
            var result = await _Bus.GetByProduct(id, page, pageSize);

            return Ok(result);
        }
        [HttpGet("GetByEvaluate")]
        public async Task<IActionResult> GetByEvaluate([FromQuery] int Evaluate, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _Bus.GetByEvaluate(Evaluate, page, pageSize);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateRatingDto>> Create([FromBody] CreateRatingDto dto)
        {
            var createdEntity = await _Bus.CreateS(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<Rating>> Update([FromBody] Rating dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Rating>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        [AllowAnonymous]

        public async Task<ActionResult<RatingDto>> Search([FromQuery] Paging paging)
        {
            var result = await _Bus.Search(paging);

            return Ok(result);
        }

    }
}
