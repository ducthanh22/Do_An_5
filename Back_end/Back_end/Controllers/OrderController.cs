using BUS.Interface;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using DTO;



namespace Back_end.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderBus _Bus;
        public OrderController(IOrderBus Bus)
        {
            _Bus = Bus;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid")]
        public async Task<ActionResult<OrderDto>> Getbyid(int id)
        {
            var result = await _Bus.Getbyid(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderDto dto)
        {
            var createdEntity = await _Bus.Create(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        public async Task<ActionResult<OrderDto>> Update([FromBody] OrderDto dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<OrderDto>> Delete(int id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }

        [HttpPost("CrateOrder")]
        public async Task<ActionResult<CreateOrderDto>> CreateOrder([FromBody] CreateOrderDto entity)
        {
            var result = await _Bus.CreateOrder(entity);
            return Ok(result);
        }
        
    }
}
