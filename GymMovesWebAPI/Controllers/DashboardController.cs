using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    public class DashboardController : Controller {
        [Route("dashboard")]
        [Route("AddMembers")]
        public IActionResult AddMembers() {
            return View();
        }
    }
}
