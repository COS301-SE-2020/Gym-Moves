/*
File Name:
    ClassesController.cs

Author:
    Longji

Date Created:
    29/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
29/06/2020      Longji          Created the initial class file with functions that
                                allow adding of new classes, listing classes by the
                                user, instructor or the gym. Also added function to
                                signup users to classes.
--------------------------------------------------------------------------------

Functional Description:
    - The purpose of the classes contained is to implement the api interface that the
      mobile application would query to get data from the database or to add a new
      class to a gym. This class is also used to register a pre-existing user to a
      pre-existing class at the gym.
      

List of Classes:
    - ClassesController
*/

using System.Threading.Tasks;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Mappers;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase {
        private readonly IUserRepository userRepository;
        private readonly IClassRepository classRepository;
        private readonly IClassRegisterRepository registerRepository;
        private readonly IGymRepository gymRepository;

        public ClassesController(IUserRepository uR, IClassRepository cR, IClassRegisterRepository cRR, IGymRepository gR) {
            userRepository = uR;
            classRepository = cR;
            registerRepository = cRR;
            gymRepository = gR;
        }

        [HttpPost("add")]
        public async Task<ActionResult<bool>> addClass(GymClassRequest newClass) {
            Users personAdding = await userRepository.getUser(newClass.Username);

            if (personAdding == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The user requesting for the class to be added could not be found!");
            }

            if (personAdding.UserType == UserTypes.Manager) {
                if (personAdding.GymIdForeignKey == newClass.NewClass.GymId) {
                    GymClasses newClassModel = ClassMappers.reducedClassToClassModel(newClass.NewClass);

                    if (await classRepository.getInstructorClassAtSpecificDateTime(newClassModel.InstructorUsername, newClassModel.Day, newClassModel.StartTime) == null) {
                        if (await classRepository.addClass(newClassModel)) {
                            return Ok(true);
                        } else 
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred attmepting to the new class to the database!");
                        }
                    } else 
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "Designated instructor already has a class starting at the given start time!");
                    }
                } else 
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Managers can only add new classes for the gym they're assigned to!");
                }
            } else 
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Only managers can add new classes!");
            }
        }

        [HttpGet("gymlist")]
        public async Task<ActionResult<GymClassResponse[]>> getByGym(int gymId) {
            if (await gymRepository.getGymById(gymId) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The specified gym was not found!");
            }

            GymClasses[] allClasses = await classRepository.getGymClasses(gymId);

            if (allClasses.Length == 0) {
                return Ok(new GymClassResponse[0]);
            }

            GymClassResponse[] allClassesReduced = ClassMappers.classModelToReducedModel(allClasses);

            return Ok(allClassesReduced);
        }

        [HttpGet("userlist")]
        public async Task<ActionResult<GymClassResponse[]>> getByUser(string username) {
            if (await userRepository.getUser(username) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The specified user was not found!");
            }

            ClassRegister[] registerList = await registerRepository.getUserRegisters(username, true);

            if (registerList.Length == 0) {
                return Ok(new GymClassResponse[0]);
            }

            GymClassResponse[] classesReduced = new GymClassResponse[registerList.Length];

            for (int i = 0; i < registerList.Length; i++) {
                classesReduced[i] = ClassMappers.classModelToReducedModel(registerList[i].Class);
            }

            return Ok(classesReduced);
        }

        [HttpGet("instructorlist")]
        public async Task<ActionResult<GymClassResponse[]>> getByInstructor(string username) {
            Users verifyUser = await userRepository.getUser(username);

            if (verifyUser == null || verifyUser.UserType != UserTypes.Instructor) {
                return StatusCode(StatusCodes.Status404NotFound, "The specified instructor was not found!");
            }

            GymClasses[] classes = await classRepository.getInstructorClasses(username);

            if (classes.Length == 0) {
                return Ok(new GymClassResponse[0]);
            }

            GymClassResponse[] reducedClasses = ClassMappers.classModelToReducedModel(classes);

            return Ok(reducedClasses);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<bool>> signUpUserToClass(RegisterUserForClassRequest register) {
            if (await classRepository.getClassById(register.ClassId) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The class the user wants to signup for does not exist!");
            }

            if (await userRepository.getUser(register.Username) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The user does not exist!");
            }

            ClassRegister existingRegister = await registerRepository.getSpecificUserAndClass(register.Username, register.ClassId);

            if (existingRegister == null) {
                GymClasses targetClass = await classRepository.getClassById(register.ClassId);

                if (targetClass.CurrentStudents < targetClass.MaxCapacity) {
                    if (await registerRepository.addRegister(RegisterMapper.registerUserForClassToClassRegister(register))) {
                        return Ok(true);
                    } else 
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while trying to register the user for the class!");
                    }
                } else 
                {
                    return Ok(false);
                }
            } else 
            {
                return Ok(false);
            }
        }
    }
}
