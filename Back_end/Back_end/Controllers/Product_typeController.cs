using Back_end.Attribute;
using DTO.Enum;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_typeController : ControllerBase
    {
        public readonly IProduct_typeBus _Bus;
        public Product_typeController(IProduct_typeBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<Product_type>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }
        [HttpGet("GetByCategory/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product_typeDto>>> GetByCategory(Guid id)
        {
            var result = await _Bus.GetByCategory(id);
            return Ok(result);
        }

        [HttpGet("GetByid")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<Product_typeDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<Product_typeDto>> Create([FromBody] Product_type dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Product_typeDto>> Update([FromBody] Product_type dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<Product_type>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }

        [HttpGet("Search")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<IActionResult> Search([FromQuery] Paging paging)
        {
            var result = await _Bus.Search(paging);


            return Ok(result);
        }
    }
}
