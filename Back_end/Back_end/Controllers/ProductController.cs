using Model;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Interface;
using DTO;
using Back_end.Attribute;
using DTO.Enum;

namespace Back_end.Controllers
{
    [Authorize]
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
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<GetProductsDto>>> GetAll()
        {
            var result = await _productsBus.Getalls();
            return Ok(result);
        }
        [HttpGet("GetBestSellingProducts")]
        [AllowAnonymous]

        public async Task<ActionResult<List<bestSellingProducts>>> GetBestSellingProducts()
        {
            var result = await _productsBus.GetBestSellingProducts();
            return Ok(result);
        }

        [HttpGet("GetProductNew")]
        [AllowAnonymous]

        public async Task<ActionResult<List<GetProductsDto>>> GetProductNew()
        {
            var result = await _productsBus.GetProductNew();
            return Ok(result);
        }
        [HttpGet("GetProductSale")]
        [AllowAnonymous]

        public async Task<ActionResult<List<GetProductsDto>>> GetProductSale()
        {
            var result = await _productsBus.GetProductSale();
            return Ok(result);
        }
        [HttpGet("GetByid/{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<GetProductsDto>> GetByIds(Guid id)
        {
            var result = await _productsBus.GetByIds(id);
            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<ProductsDto>> Create([FromBody] ProductsDto dto)
        {
            var createdEntity = await _productsBus.Creates(dto);
           

            return Ok(createdEntity);
        }

        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<ProductsDto>> Update([FromBody] ProductsDto dto)
        {
            var createdEntity = await _productsBus.Updates(dto);
            return Ok(createdEntity);
        }
        [HttpDelete("Delete/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Deleted })]

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
        [AllowAnonymous]

        public async Task<IActionResult> Search([FromQuery] string? keyword, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _productsBus.Search(keyword, pageIndex, pageSize);

            return Ok(result);
        }

        [HttpPost("UpLoadFile")]
        [HasPermission(new[] { (int)EnumModule.Module.QlPr }, new[] { (int)EnumPermission.Type.Create})]

        public async Task<IActionResult> UpLoadFile([FromForm] UpLoadFile product)
        {
            var result = await _productsBus.UploadFile(product);

            return Ok(result);
        }
    }
}
