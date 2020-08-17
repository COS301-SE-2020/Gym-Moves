using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    public class DashboardController : Controller {
       

        [Route("[controller]/ManagerRatings")]
        public IActionResult GymManagerRating()
        {
            return View();
        }

        [Route("[controller]/ClassAttendance")]
        public IActionResult ClassAttendance()
        {
            return View();
        }

        [Route("[controller]/GymTraffic")]
        public IActionResult GymTraffic()
        {
            return View();
        }

        [Route("Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Route("[controller]/Home")]
        public IActionResult Home()
        {
            return View();
        }
    }
}
