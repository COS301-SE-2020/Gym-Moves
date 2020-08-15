using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    public class DashboardController : Controller {
        [Route("[controller]")]

        [Route("MainPageForDashboard")]
        public IActionResult MainPageForDashboard()
        {
            return View();
        }

        [Route("ManagerRatings")]
        public IActionResult GymManagerRating() 
        {
            return View();
        }

        [Route("AddMember")]
        public IActionResult AddMember() {
            return View();
        }
    }
}
