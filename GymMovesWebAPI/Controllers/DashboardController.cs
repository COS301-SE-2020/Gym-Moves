using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    [Route("[controller]")]
    public class DashboardController : Controller {
        public IActionResult MainPageForDashboard()
        {
            return View();
        }

        [Route("ManagerRatings")]
        public IActionResult GymManagerRating() 
        {
            return View();
        }

        [Route("AddMembers")]
        public IActionResult AddMembers() {
            return View();
        }
    }
}
