using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    public class TestModel {
        public string tt { get; set; }
        public int nn { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {
        [HttpPost("poof")]
        public ActionResult<TestModel> poof(TestModel t) {
            Console.WriteLine("Request...");
            return Ok(t);
        }
    }
}
