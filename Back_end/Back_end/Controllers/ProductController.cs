using Back_end.Model;
using BUS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductsBus _productsBus;

        public ProductController(IProductsBus productsBus)
        {
            _productsBus = productsBus;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductsDto>>> GetAll()
        {
            var result = await _productsBus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<ProductsDto>> Getbyid(int id)
        {
            var result = await _productsBus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProductsDto>> Create([FromBody] Products dto)
        {
            var createdEntity = await _productsBus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<ProductsDto>> Update([FromBody] Products dto)
        {
            var createdEntity = await _productsBus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<ProductsDto>> Delete(int id)
        {
            var result = await _productsBus.Delete(id);
            return Ok(result);
        }

        //[HttpGet("Search")]
        //public async Task<IActionResult> Search([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    var products = await _productsBus.Search(searchTerm, page, pageSize);


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
