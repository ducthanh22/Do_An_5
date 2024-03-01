using Back_end.Attribute;
using BUS.Interface;
using DTO;
using Model;
using DTO.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    

    public class CategoriesController : ControllerBase
    {
        private ICategoriesBus _categoriesBus;

        public CategoriesController(ICategoriesBus categoriesBus)
        {
            _categoriesBus = categoriesBus;
        }

        [HttpGet("GetAll")]
        //[HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Update })]
        public async Task<ActionResult<List<Categories>>> GetAll()
        {
            var result= await _categoriesBus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        //[Authorize(Policy = "AdminRole")]
        //[Authorize(Policy = "CustomerRole")]
        //[Authorize(Roles = "Admin,Customer")]
        //[HasPermission(new[] { (int)EnumModule.Module.QlDm }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Categories>> Getbyid(Guid id)
        {
            var result = await _categoriesBus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CategoriesDto>> Create([FromBody] Categories dto)
        {
            var createdEntity = await _categoriesBus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<CategoriesDto>> Update([FromBody] Categories dto)
        {
            var createdEntity = await _categoriesBus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<Categories>> Delete(Guid id)
        {
            var result = await _categoriesBus.Delete(id);
            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] Paging paging)
        {
            var result = await _categoriesBus.Search(paging);
          

            return Ok(result);
        }

    }
}
