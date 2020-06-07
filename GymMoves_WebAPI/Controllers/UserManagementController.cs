using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymMoves_WebAPI.Data.Repositories.Implementation;

namespace GymMoves_WebAPI.Controllers
{
    [ApiController]
    public class UserManagementController : Controller
    {

        private readonly UserRepositoryInterface _repo;

        public UserManagementController(UserRepositoryInterface repo)
        {
            _repo = repo;
        }

    


        [Route("api/signup/user")]
        [HttpPost]
        public async Task<ActionResult<UserEntity>> SignUpUser(UserEntity user)
        {
            bool added = false;

            
            added = await _repo.Add(user);
           
            if (added)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "{\"Error\" : \"Failed to find this member.\"}");
            }
        }

        [Route("api/signup/instructor")]
        [HttpPost]
        public async Task<ActionResult<InstructorEntity>> SignUp(InstructorEntity user)
        {
            bool added = false;

            added = await _repo.AddInstructor(user);

            if (added)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "{\"Error\" : \"Failed to find this member.\"}");
            }
        }

        [Route("api/signup/manager")]
        [HttpPost]
        public async Task<ActionResult<UserEntity>> SignUpManager(UserEntity user)
        {
            bool added = false;

            added = await _repo.Add(user);

            if (added)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "{\"Error\" : \"Failed to find this member.\"}");
            }
        }



        [Route("api/login/user")]
        [HttpPost]
        public async Task<ActionResult<UserEntity>> LogInUser(UserEntity user)
        {
            UserEntity person = null;

            person = await _repo.GetUserWithID(user.UserID);

            if (person != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "{\"Error\" : \"Failed to find this member.\"}");
            }
        }

        [Route("api/login/instructor")]
        [HttpPost]
        public async Task<ActionResult<InstructorEntity>> LogInInstructor(InstructorEntity user)
        {
            InstructorEntity person = null;

            person = await _repo.GetInstructorWithID(user.InstructorID);

            if (person != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "{\"Error\" : \"Failed to find this member.\"}");
            }
        }

        [Route("api/login/manager")]
        [HttpPost]
        public async Task<ActionResult<UserEntity>> LoginManager(UserEntity user)
        {
            UserEntity person = null;

            person = await _repo.GetUserWithID(user.UserID);

            if (person != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "{\"Error\" : \"Failed to find this member.\"}");
            }
        }
    }
}
