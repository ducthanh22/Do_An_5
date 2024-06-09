using BLL.Interface;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using DTO;
using Back_end.Attribute;
using DTO.Enum;



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
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var result = await _Bus.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByid/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<ActionResult<OrderDto>> Getbyid(Guid id)
        {
            var result = await _Bus.Getbyids(id);
            return Ok(result);
        }
        [HttpGet("destroyOrder/{id}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Deleted })]
        public async Task<ActionResult<OrderDto>> destroyOrder(Guid id)
        {
            var result = await _Bus.destroyOrder(id);
            return Ok(result);
        }
        [HttpGet("GetByCustomer")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<OrderDto>>> GetbyCustomerGet(string id)
        {
            var result = await _Bus.GetbyCustomer(id);

            return Ok(result);
        }
        [HttpGet("GetOrderProduct")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]

        public async Task<ActionResult<List<GetorderDto>>> GetOrderProduct(string id, int status)
        {
            var result = await _Bus.GetOrderProduct(id, status);

            return Ok(result);
        }

        [HttpPost("create")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Create })]

        public async Task<ActionResult<CreateOrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            var createdEntity = await _Bus.CreateOrder(dto);

            return Ok(createdEntity);
        }
        [HttpPut("update")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Update })]

        public async Task<ActionResult<Order>> Update([FromBody] Order dto)
        {
            var createdEntity = await _Bus.Update(dto);

            return Ok(createdEntity);
        }
        [HttpDelete("Delete")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Deleted })]

        public async Task<ActionResult<OrderDto>> Delete(Guid id)
        {
            var result = await _Bus.Delete(id);
            return Ok(result);
        }
        [HttpGet("Search/{status}")]
        [HasPermission(new[] { (int)EnumModule.Module.QlDh }, new[] { (int)EnumPermission.Type.Read })]
        public async Task<IActionResult> Search([FromQuery] Paging paging, int status)
        {
            var result = await _Bus.Search(paging, status);


            return Ok(result);
        }


    }
}
