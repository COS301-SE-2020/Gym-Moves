using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    public class IndexController : Controller {
        [Route("")]
        public IActionResult Index() {
            return View();
        }
    }
}
