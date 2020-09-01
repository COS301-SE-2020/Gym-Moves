using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers
{
    [Route("[controller]")]
    public class DashboardController : Controller
    {

        [Route("ClassAttendance")]
        public IActionResult ClassAttendance()
        {
            return View();
        }

        [Route("GymTraffic")]
        public IActionResult GymTraffic()
        {
            return View();
        }

        [Route("Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Route("Home")]
        public IActionResult Home()
        {
            return View();
        }


        [Route("Ratings")]
        public IActionResult GymManagerRating()
        {
            return View();
        }

        [Route("AddMembers")]
        public IActionResult AddMembers()
        {
            return View();
        }

        [Route("AddStaffMember")]
        public IActionResult AddStaffMember()
        {
            return View();
        }
    }
}
