using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wso2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Wso2controller : ControllerBase
    {
     
        [HttpGet("postvalua")]
        public string HIHI()
        {
            return "hihi";
        }

        [HttpGet("hihi")]
        public string haha()
        {
            return "haha";
        }
    }
}
