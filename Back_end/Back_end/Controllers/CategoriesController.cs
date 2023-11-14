using Back_end.Model;
using BUS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
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
        public async Task<ActionResult<List<CategoriesDto>>> GetAll()
        {
            var result= await _categoriesBus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<CategoriesDto>> Getbyid(int id)
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
        public async Task<ActionResult<Categories>> Delete(int id)
        {
            var result = await _categoriesBus.Delete(id);
            return Ok(result);
        }

        //[HttpGet("Search")]
        //public async Task<IActionResult> Search([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10 )
        //{
        //    var products = await _categoriesBus.Search(searchTerm, page, pageSize);


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
