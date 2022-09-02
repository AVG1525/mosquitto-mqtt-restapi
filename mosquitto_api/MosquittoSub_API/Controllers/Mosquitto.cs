using Microsoft.AspNetCore.Mvc;

namespace MosquittoSub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Mosquitto : ControllerBase
    {
        public readonly IMem _mem;
        public Mosquitto(IMem mem)
        {
            _mem = mem;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mem.GetAll());
        }

        [HttpPost]
        public IActionResult Post(object value)
        {
            _mem.Add(value.ToString());
            return Created(string.Empty, value);
        }
    }
}
