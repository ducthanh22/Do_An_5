using BLL.Interface;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;


namespace Back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExportbillController : ControllerBase
    {
        public IExportbillBus _Bus;
        public ExportbillController(IExportbillBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ExportbillDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<ExportbillDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateExportbillDto>> Create([FromBody] CreateExportbillDto dto)
        {
            var createdEntity = await _Bus.CreateEX(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<ExportbillDto>> Update([FromBody] Exportbill dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Exportbill>> Delete(Guid id)
        {
            var result = await _Bus.DELETE(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(Paging paging)
        {
            var result = await _Bus.Search(paging);

            return Ok(result);
        }
    }
}
