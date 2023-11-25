using Back_end.Model;
using BUS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Authorize]
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

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string? keywork, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _productsBus.Search(keywork, page, pageSize);

            return Ok(result);
        }
        [HttpPost("UpLoadFile")]
        public async Task<IActionResult> UpLoadFile([FromForm] UpLoadFile product)
        {
            var result = await _productsBus.UploadFile(product);

            return Ok(result);
        }
    }
}
