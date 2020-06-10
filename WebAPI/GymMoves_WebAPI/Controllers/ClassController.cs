using System.Threading.Tasks;
using GymMoves_WebAPI.Data.EntityModels;
using GymMoves_WebAPI.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymMoves_WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller {
        private readonly UserRepositoryInterface _userRepo;
        private readonly ClassRepositoryInterface _classRepo;
        private readonly GymRepositoryInterface _gymRepo;

        public ClassController(UserRepositoryInterface userRepo, ClassRepositoryInterface classRepo, GymRepositoryInterface gymRepo) {
            _userRepo = userRepo;
            _classRepo = classRepo;
            _gymRepo = gymRepo;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ClassEntity>> AddClass(ClassEntity entity) {
            ClassEntity existing = await _classRepo.FindByClass(entity);

            if (existing != null) {
                return NotFound("Class already exists!");
            } else {
                bool status = await _classRepo.Add(entity);

                if (status) {
                    existing = await _classRepo.FindByClass(entity);

                    return Ok(existing);
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while attempting to add a new class entity to the database!");
                }
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<ClassEntity>> SignUpForClass(string userid, int classid) {
            UserEntity userEntity = await _userRepo.GetUserWithID(userid);
            ClassEntity classEntity = await _classRepo.FindByID(classid);

            if (userEntity == null) {
                return NotFound("User does not exist");
            }

            if (classEntity == null) {
                return NotFound("Class does not exist");
            }

            ClassEntity changed = await _classRepo.RegisterUserForClass(userEntity, classEntity);

            if (changed != null) {
                return Ok(changed);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to register user to class");
            }
        }

        [HttpGet("findByUser")]
        public async Task<ActionResult<ClassEntity[]>> GetClassByUser(string userid) {
            UserEntity user = await _userRepo.GetUserWithID(userid);

            if (user == null) {
                return Unauthorized("User with id " + userid + " does not exist!");
            } else {
                return Ok(_userRepo.GetUserClasses(userid));
            }
        }

        [HttpGet("findById")]
        public async Task<ActionResult<ClassEntity>> GetClassByClassID(int classid) {
            ClassEntity foundClass = await _classRepo.FindByID(classid);

            if (foundClass != null) {
                return Ok(foundClass);
            } else {
                return NotFound("Class not found!");
            }
        }

        [HttpGet("findByGym")]
        public async Task<ActionResult<ClassEntity[]>> GetClassByGym(int gymid) {
            GymEntity gym = await _gymRepo.FindById(gymid);

            if (gym == null) {
                return NotFound("Gym not found!");
            }

            ClassEntity[] foundClass = await _classRepo.FindByGym(gym);

            return Ok(foundClass);
        }
    }
}
