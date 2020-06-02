using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymMoves_WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller {
        [HttpGet("a")]
        public IActionResult AFunc() {
            return Ok();
        }
    }
}
