using BLL.Interface;
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
    public class ImportbillController : ControllerBase
    {
        public IImportbillBus _Bus;
        public ImportbillController(IImportbillBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ImportbillDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<ImportbillDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<CreateImportbillDto>> CreateIm([FromBody] CreateImportbillDto dto)
        {
            var createdEntity = await _Bus.CreateIm(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Importbill>> Update([FromBody] Importbill dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<ImportbillDto>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] Paging paging)
        {
            var result = await _Bus.Search(paging);

            return Ok(result);
        }
    }
}
