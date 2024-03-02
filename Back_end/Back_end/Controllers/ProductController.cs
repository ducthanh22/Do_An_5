using Model;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Interface;
using DTO;

namespace Back_end.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductsBus _productsBus;
        private IPriceBus _priceBus;

        public ProductController(IProductsBus productsBus, IPriceBus priceBus)
        {
            _productsBus = productsBus;
            _priceBus = priceBus;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductsDto>>> GetAll()
        {
            var result = await _productsBus.Getalls();
            return Ok(result);
        }

        //[HttpGet("GetByid")]
        //public async Task<ActionResult<ProductsDto>> Getbyid(int id)
        //{
        //    var result = await _productsBus.Getbyid(id);
        //    return Ok(result);
        //}
        [HttpGet("GetByids")]
        public async Task<ActionResult<ProductsDto>> GetByIds(Guid id)
        {
            var result = await _productsBus.GetByIds(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProductsDto>> Create([FromBody] ProductsDto dto)
        {
            var createdEntity = await _productsBus.Creates(dto);
           

            return Ok(createdEntity);
        }

        [HttpPut("update")]
        public async Task<ActionResult<ProductsDto>> Update([FromBody] ProductsDto dto)
        {
            var createdEntity = await _productsBus.Updates(dto);
            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<ProductsDto>> Delete(Guid id)
        {
            var result = await _productsBus.Delete(id);
            if (result != null)
            {
                await _priceBus.Delete(result.Id);
            }
            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string? keywork, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _productsBus.Search(keywork, page, pageSize);

            return Ok(result);
        }
        //[HttpPost("UpLoadFile")]
        //public async Task<IActionResult> UpLoadFile([FromForm] UpLoadFile product)
        //{
        //    var result = await _productsBus.UploadFile(product);

        //    return Ok(result);
        //}
    }
}
