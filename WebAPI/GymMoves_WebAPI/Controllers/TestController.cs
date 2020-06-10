using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymMoves_WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {
        private readonly UserRepositoryInterface _repo;

        public TestController(UserRepositoryInterface repo) {
            _repo = repo;
        }

        [HttpPost("user/add")]
        public async Task<ActionResult<UserEntity>> AddUser(UserEntity user) {
            bool status = await _repo.Add(user);
            UserEntity addedEntity = null;

            if (status) {
                addedEntity = await _repo.GetUserWithID(user.UserID);
            }

            if (addedEntity != null) {
                return Ok(addedEntity);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add");
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserEntity>> GetUser(string id) {
            UserEntity user = await _repo.GetUserWithID(id);

            if (user != null) {
                return Ok(user);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to find");
            }
        }

        [HttpPost("user/remove")]
        public async Task<ActionResult<UserEntity>> RemoveUser(UserEntity entity) {
            bool status = await _repo.RemoveUser(entity);

            if (status) {
                return Ok(entity);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to remove");
            }
        }

        [HttpPost("instructor/add")]
        public async Task<ActionResult<InstructorEntity>> AddInstructor(InstructorEntity entity) {
            bool status = await _repo.AddInstructor(entity);

            InstructorEntity instructor = await _repo.GetInstructorWithID(entity.InstructorID);

            if (status) {
                return Ok(instructor);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Add unsuccessful");
            }
        }

        [HttpGet("instructor")]
        public async Task<ActionResult<InstructorEntity>> GetInstructor(string id) {
            InstructorEntity instructor = await _repo.GetInstructorWithID(id);

            if (instructor != null) {
                return Ok(instructor);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to find");
            }
        }

        [HttpPost("instructor/remove")]
        public async Task<ActionResult<InstructorEntity>> RemoveInstructor(InstructorEntity entity) {
            bool status = await _repo.RemoveInstructor(entity);

            if (status) {
                return Ok(entity);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to remove");
            }
        }
    }
}
