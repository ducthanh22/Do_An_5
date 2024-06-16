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
    public class Detail_importbillController : ControllerBase
    {
        public IDetail_importbillBus _Bus;
        public Detail_importbillController(IDetail_importbillBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<Detail_importbillDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<Detail_importbillDto>> Getbyid(Guid id)
        {
            var result = await _Bus.GETBYID(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<Detail_importbillDto>> Create([FromBody] Detail_importbill dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Detail_importbillDto>> Update([FromBody] Detail_importbill dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlHdn }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Detail_importbillDto>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        //[HttpGet("Search")]
        //public async Task<IActionResult> Search([FromQuery] string keywork, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    var products = await _Bus.Search(keywork, page, pageSize);
        //    var result = new
        //    {
        //        ItemsPerPage = pageSize,
        //        CurrentPage = page,
        //        Products = products
        //    };

        //    return Ok(result);
        //}
    }
}
