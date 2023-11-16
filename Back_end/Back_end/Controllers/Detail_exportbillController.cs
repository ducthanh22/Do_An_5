using Back_end.Model;
using BUS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detail_exportbillController : ControllerBase
    {
        public IDetail_exportbillBus _Bus;
        public Detail_exportbillController(IDetail_exportbillBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Detail_exportbill>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<Detail_exportbillDto>> Getbyid(int id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Detail_exportbillDto>> Create([FromBody] Detail_exportbill dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<Detail_exportbillDto>> Update([FromBody] Detail_exportbill dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<Detail_exportbillDto>> Delete(int id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        
    }
}
