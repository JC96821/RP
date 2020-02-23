using Microsoft.AspNetCore.Mvc;

namespace EASYS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "SERVER START!";
        }
    }
}
