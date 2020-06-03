using GymMoves_WebAPI.Data.EntityModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymMoves_WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : Controller {
        [HttpPost("def")]
        public IActionResult DefPost(UserEntity user) {
            return Ok("message");
        }
    }
}
