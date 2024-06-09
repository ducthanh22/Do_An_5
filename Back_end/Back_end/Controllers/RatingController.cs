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
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<Rating>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<Rating>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }
        [HttpGet("GetByProduct")]
        [AllowAnonymous]
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
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<CreateRatingDto>> Create([FromBody] CreateRatingDto dto)
        {
            var createdEntity = await _Bus.CreateS(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Rating>> Update([FromBody] Rating dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Rating>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<RatingDto>> Search([FromQuery] Paging paging)
        {
            var result = await _Bus.Search(paging);

            return Ok(result);
        }

    }
}
