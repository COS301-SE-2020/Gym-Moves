using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers
{
    [Route("[controller]")]
    public class AdminWebsiteController: Controller
    {
        [Route("AdminLogin")]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [Route("AdminSignUp")]
        public IActionResult AdminSignUp()
        {
            return View();
        }

        [Route("AdminHome")]
        public IActionResult Admin()
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
