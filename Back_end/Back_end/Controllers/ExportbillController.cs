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
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<ExportbillDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<CreateExportbillDto>> Create([FromBody] CreateExportbillDto dto)
        {
            var createdEntity = await _Bus.CreateEX(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<ExportbillDto>> Update([FromBody] Exportbill dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdb }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Exportbill>> Delete(Guid id)
        {
            var result = await _Bus.DELETE(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<ActionResult<ExportbillDto>> Search([FromQuery] Paging paging)
        {
            var result = await _Bus.Search(paging);

            return Ok(result);
        }
    }
}
