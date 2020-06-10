using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymMoves_WebAPI.Data.Repositories.Implementation;
using GymMoves_WebAPI.Data.Models;

namespace GymMoves_WebAPI.Controllers
{
    [ApiController]
    public class UserManagementController : Controller
    {

        private readonly UserRepositoryInterface user_repo;

        public UserManagementController(UserRepositoryInterface repo)
        {
            user_repo = repo;
        }


        [Route("api/signup/user")]
        [HttpPost]
        public async Task<ActionResult<ReturnObjectUM>> SignUpUser(UserEntity user)
        {
            bool added = false;
            
            added = await user_repo.Add(user);

            ReturnObjectUM returnMessage = new ReturnObjectUM();

            if (added)
            {
                returnMessage.status = "successful";
                returnMessage.user = "member";
                returnMessage.message = "N/A";

                return Ok(returnMessage);
            }
            else
            {
                returnMessage.status = "unsuccessful";
                returnMessage.message = "This person is not a member of the gym!";
                returnMessage.user = "N/A";

                return Unauthorized(returnMessage);
            }
        }

        [Route("api/signup/instructor")]
        [HttpPost]
        public async Task<ActionResult<ReturnObjectUM>> SignUp(InstructorEntity user)
        {
            bool added = false;

            added = await user_repo.AddInstructor(user);

            ReturnObjectUM returnMessage = new ReturnObjectUM();

            if (added)
            {
                returnMessage.status = "successful";
                returnMessage.user = "instructor";
                returnMessage.message = "N/A";

                return Ok(returnMessage);
            }
            else
            {
                returnMessage.status = "unsuccessful";
                returnMessage.message = "This person is not a member of the gym!";
                returnMessage.user = "N/A";

                return Unauthorized(returnMessage);
            }
        }

        [Route("api/signup/manager")]
        [HttpPost]
        public async Task<ActionResult<ReturnObjectUM>> SignUpManager(UserEntity user)
        {
            bool added = false;

            added = await user_repo.Add(user);

            ReturnObjectUM returnMessage = new ReturnObjectUM();

            if (added)
            {
                returnMessage.status = "successful";
                returnMessage.user = "manager";
                returnMessage.message = "N/A";

                return Ok(returnMessage);
            }
            else
            {
                returnMessage.status = "unsuccessful";
                returnMessage.message = "This person is not a member of the gym!";
                returnMessage.user = "N/A";

                return Unauthorized(returnMessage);
            }
        }



        [Route("api/login/user")]
        [HttpPost]
        public async Task<ActionResult<ReturnObjectUM>> LogInUser(UserEntity user)
        {
            UserEntity person = null;

            person = await user_repo.GetUserWithID(user.UserID);

            ReturnObjectUM returnMessage = new ReturnObjectUM();

            if (person != null && person.Password == user.Password)
            {
                returnMessage.status = "successful";
                returnMessage.user = "member";
                returnMessage.message = "N/A";

                return Ok(returnMessage);
            }
            else
            {
                returnMessage.status = "unsuccessful";
                returnMessage.message = "This person is not a member of the gym!";
                returnMessage.user = "N/A";

                return Unauthorized(returnMessage);
            }
        }

        [Route("api/login/instructor")]
        [HttpPost]
        public async Task<ActionResult<ReturnObjectUM>> LogInInstructor(InstructorEntity user)
        {
            InstructorEntity person = null;

            person = await user_repo.GetInstructorWithID(user.InstructorID);

            ReturnObjectUM returnMessage = new ReturnObjectUM();

            if (person != null && person.Password == user.Password)
            {
                returnMessage.status = "successful";
                returnMessage.user = "instructor";
                returnMessage.message = "N/A";

                return Ok(returnMessage);
            }
            else
            {
                returnMessage.status = "unsuccessful";
                returnMessage.message = "This person is not a member of the gym!";
                returnMessage.user = "N/A";

                return Unauthorized(returnMessage);
            }
        }

        [Route("api/login/manager")]
        [HttpPost]
        public async Task<ActionResult<UserEntity>> LoginManager(UserEntity user)
        {
            UserEntity person = null;

            person = await user_repo.GetUserWithID(user.UserID);

            ReturnObjectUM returnMessage = new ReturnObjectUM();

            if (person != null && person.Password == user.Password)
            {
                returnMessage.status = "successful";
                returnMessage.user = "member";
                returnMessage.message = "N/A";

                return Ok(returnMessage);
            }
            else
            {
                returnMessage.status = "unsuccessful";
                returnMessage.message = "This person is not a member of the gym!";
                returnMessage.user = "N/A";

                return Unauthorized(returnMessage);
            }
        }
    }
}
