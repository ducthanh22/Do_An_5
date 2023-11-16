using BUS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Back_end.Controllers
{
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
        public async Task<ActionResult<List<Detail_importbill>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<Detail_importbillDto>> Getbyid(int id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Detail_importbillDto>> Create([FromBody] Detail_importbill dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<Detail_importbillDto>> Update([FromBody] Detail_importbill dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<Detail_importbillDto>> Delete(int id)
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
