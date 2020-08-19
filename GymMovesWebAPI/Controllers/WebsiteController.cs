using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {

    public class WebsiteController : Controller {

       
        [Route("ManagerLogin")]
        public IActionResult ManagerLogin() {
            return View();
        }
        
        [Route("ManagerDetails")]
        public IActionResult ManagerDetails() {
            return View();
        }

        [Route("GymManagers")]
        public IActionResult GymManagers()
        {
            return View();
        }

        [Route("GymMoves")]
        public IActionResult GymMovesIndex()
        {
            return View();
        }

        [Route("GymMembers")]
        public IActionResult GymMembers()
        {
            return View();
        }

        [Route("GymInstructors")]
        public IActionResult GymInstructors()
        {
            return View();
        }

        [Route("HowToRegisterMyGym")]
        public IActionResult HowToRegisterMyGym()
        {
            return View();
        }

        [Route("RegisterMyGym")]
        public IActionResult RegisterMyGym()
        {
            return View();
        }

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword() {
            return View();
        }

        [Route("ResetPassword")]
        public IActionResult ResetPassword() {
            return View();
        }






    }
}
