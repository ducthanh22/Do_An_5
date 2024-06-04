using BLL.Interface;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        public readonly IStatisticalBus _Bus;
        public StatisticalController(IStatisticalBus bus)
        {
            _Bus = bus;
        }

        [HttpGet("Darhboarsh")]
        public async Task<ActionResult<StatisticalDto>> Darhboarsh([FromQuery] DateTime? start, [FromQuery] DateTime? end)
        {
            var createdEntity = await _Bus.Darhboarsh(start,end);

            return Ok(createdEntity);
        }
    }
}
