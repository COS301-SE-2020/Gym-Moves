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
29/06/2020    | Longji         | Created the initial class file with functions that
              |                | allow adding of new classes, listing classes by the
              |                | user, instructor or the gym. Also added function to
              |                | signup users to classes.
--------------------------------------------------------------------------------
03/07/2020    | Longji         | Removed function used for adding new users for test
              |                | purposes.
--------------------------------------------------------------------------------
08/07/2020    | Longji         | Added functions to remove a class and a function to
                               | deregister a user from the class.
--------------------------------------------------------------------------------
11/07/2020    | Longji         | Added function to get a specific class
--------------------------------------------------------------------------------
18/07/2020    | Danel          | Instructors can cancel classes
--------------------------------------------------------------------------------
15/07/2020    | Longji         | Added API call to get a specific user, class combination.
--------------------------------------------------------------------------------
18/07/2020    | Longji         | Added API call to edit a class.
              |                | Fixed mistakes in the remove classes api function.
--------------------------------------------------------------------------------


Functional Description:
    - The purpose of the classes contained is to implement the api interface that the
      mobile application would query to get data from the database or to add a new
      class to a gym. This class is also used to register a pre-existing user to a
      pre-existing class at the gym.
      

List of Classes:
    - ClassesController
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Mappers;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Implementations;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.MailerProgram;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IClassRepository classRepository;
        private readonly IClassRegisterRepository registerRepository;
        private readonly IGymRepository gymRepository;
        private readonly IClassAttendanceRepository classAttendanceRepository;
        private readonly IMailer mailer;

        string emailReceiver = "u18008659@tuks.co.za";

        public ClassesController(IUserRepository uR, IClassRepository cR, IClassRegisterRepository cRR,
            IGymRepository gR, IClassAttendanceRepository cAR, IMailer mail)
        {
            userRepository = uR;
            classRepository = cR;
            registerRepository = cRR;
            gymRepository = gR;
            classAttendanceRepository = cAR;
            mailer = mail;
        }

        /*
        Method Name:
            addClass
        Purpose:
            This function handles requests made to add a new class to the gym. The
            function will verify the user attempting to add the class is a manager
            as well as verify that the user attempting to add the class does exist.

            The function also verifies that the person attempting to add the class
            belongs to the gym that class is meant to be added to. It then checks
            that the instructor assigned to the class does not have a class starting
            at the same time.
         */
        [HttpPost("add")]
        public async Task<ActionResult<bool>> addClass(GymClassRequest newClass) {
            
            Users personAdding = await userRepository.getUser(newClass.Username);

            if (personAdding == null) {
                
                return StatusCode(StatusCodes.Status404NotFound, "The user requesting for " +
                    "the class to be added could not be found!");
            }

            if (personAdding.UserType == UserTypes.Manager) {

                if (personAdding.GymIdForeignKey == newClass.NewClass.GymId) {

                    GymClasses newClassModel = ClassMappers.reducedClassToClassModel(newClass.NewClass);

                    if (await classRepository.getInstructorClassAtSpecificDateTime(newClassModel.InstructorUsername,
                        newClassModel.Day, newClassModel.StartTime) == null) {

                        if (await classRepository.addClass(newClassModel)) { 

                            return Ok(true);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred attempting " +
                                "to add the new class to the database!");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "Designated instructor already has a class " +
                            "starting at the given start time!");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Managers can only add new classes for the gym " +
                        "they're assigned to!");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Only managers can add new classes!");
            }
        }

        /*
        Method Name:
            getByGym
        Purpose:
            This function handles requests made to list all the classes that are
            hosted in a specific gym. It also verifies that the specified gym
            exists in the database before attempting to find classes registered
            to that particular gym.
        */
        [HttpGet("gymlist")]
        public async Task<ActionResult<GymClassResponse[]>> getByGym(int gymId)
        {
            if (await gymRepository.getGymById(gymId) == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The specified gym was not found!");
            }

            GymClasses[] allClasses = await classRepository.getGymClasses(gymId);

            if (allClasses.Length == 0)
            {
                return Ok(new GymClassResponse[0]);
            }

            GymClassResponse[] allClassesReduced = ClassMappers.classModelToReducedModel(allClasses);

            return Ok(allClassesReduced);
        }


        /*
        Method Name:
            getByUser
        Purpose:
            This function handles the api request for getting all classes a specific
            user is signed up for. It also verifies thhat the target user actually
            exists in the database. 
        */
        [HttpGet("userlist")]
        public async Task<ActionResult<GymClassResponse[]>> getByUser(string username)
        {
            if (await userRepository.getUser(username) == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The specified user was not found!");
            }

            ClassRegister[] registerList = await registerRepository.getUserRegisters(username, true);

            if (registerList.Length == 0)
            {
                return Ok(new GymClassResponse[0]);
            }

            GymClassResponse[] classesReduced = new GymClassResponse[registerList.Length];

            for (int i = 0; i < registerList.Length; i++)
            {
                classesReduced[i] = ClassMappers.classModelToReducedModel(registerList[i].Class);
            }

            return Ok(classesReduced);
        }

        /*
        Method Name:
            getByInstructor
        Purpose:
            This function handles the api request for getting all classes that an
            instructor is instructing. It verifies that the instructor exists in
            the database before fetching the classes.
        */
        [HttpGet("instructorlist")]
        public async Task<ActionResult<GymClassResponse[]>> getByInstructor(string username)
        {
            Users verifyUser = await userRepository.getUser(username);

            if (verifyUser == null || verifyUser.UserType != UserTypes.Instructor)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The specified instructor was not found!");
            }

            GymClasses[] classes = await classRepository.getInstructorClasses(username);

            if (classes.Length == 0)
            {
                return Ok(new GymClassResponse[0]);
            }

            GymClassResponse[] reducedClasses = ClassMappers.classModelToReducedModel(classes);

            return Ok(reducedClasses);
        }

        /*
        Method Name:
            signUpUserToClass
        Purpose:
            This function handles the api request handles registering a user to a
            specific class. It verifies that the target class as well as the target
            user actually it exists. It also checks that the class is not at capacity
            before registering the user.
            
            If the user is already signed 
        */
        [HttpPost("signup")]
        public async Task<ActionResult<bool>> signUpUserToClass(RegisterUserForClassRequest register)
        {
            if (await classRepository.getClassById(register.ClassId) == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The class the user wants to signup for does not exist!");
            }

            if (await userRepository.getUser(register.Username) == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The user does not exist!");
            }

            ClassRegister existingRegister = await registerRepository.getSpecificUserAndClass(register.Username, register.ClassId);

            if (existingRegister == null)
            {
                GymClasses targetClass = await classRepository.getClassById(register.ClassId);

                if (targetClass.CurrentStudents < targetClass.MaxCapacity)
                {
                    if (await registerRepository.addRegister(RegisterMapper.registerUserForClassToClassRegister(register)))
                    {
                       
                        DayOfWeek today = DateTime.Today.DayOfWeek;

                        DateTime date;

                        if(today.ToString().ToLower() == targetClass.Day.ToLower())
                        {
                            date = DateTime.Today.Date;
                        }
                        else
                        {
                            DayOfWeek dayOfClass;

                            switch (targetClass.Day.ToLower())
                            {
                                case "monday":
                                    dayOfClass = DayOfWeek.Monday;
                                    break;
                                case "tuesday":
                                    dayOfClass = DayOfWeek.Tuesday;
                                    break;
                                case "wednesday":
                                    dayOfClass = DayOfWeek.Wednesday;
                                    break;
                                case "thursday":
                                    dayOfClass = DayOfWeek.Thursday;
                                    break;
                                case "friday":
                                    dayOfClass = DayOfWeek.Friday;
                                    break;
                                case "saturday":
                                    dayOfClass = DayOfWeek.Saturday;
                                    break;
                                default:
                                    dayOfClass = DayOfWeek.Sunday;
                                    break;
                            }

                            int difference = days[(int)today, (int)dayOfClass];

                            date = DateTime.Today.AddDays(difference).Date;
                        }

                        ClassAttendance classToChange = await classAttendanceRepository.getClassInstance(register.ClassId, date);

                        if(classToChange == null) {
                             ClassAttendance newClass = new ClassAttendance();
                            newClass.Date = date;
                            newClass.ClassId = targetClass.ClassId;
                            newClass.Capacity = targetClass.MaxCapacity;
                            newClass.Class = targetClass;
                            newClass.NumberOfStudents = targetClass.CurrentStudents;

                            await classAttendanceRepository.addNewClassInstance(newClass);
                        }
                        else {
                            await classAttendanceRepository.editClassAttendance(classToChange.ClassId, classToChange.Date, 1);
                        }
                       

                        return Ok(true);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while trying to register the user for the class!");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Class max capacity reached!");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User is already signed up for the specified class");
            }
        }

        [HttpPost("cancel")]
        public async Task<ActionResult> cancelClass(CancelClassRequest classRequest) {
            
            GymClasses classToChange = await classRepository.getClassById(classRequest.classId);

            if (classToChange == null) {

                return NotFound("This class ID does not exist.");
            }

            if (classToChange.InstructorUsername != classRequest.username) {
               
                return Unauthorized("This instructor does not teach this class.");
            }

            string content = "";
            string returnMessage = "";
            bool oldWay = classToChange.Cancelled;

            if (oldWay) {

                returnMessage = "This class has been resumed.";
            }
            else {
                content = "We are sad to say, a class you signed up for has been cancelled!\n" +
               classToChange.Name + " that was supposed to happen on " + classToChange.Day +
               " at " + classToChange.StartTime + ".";

                returnMessage = "This class has been cancelled. Remember to resume it when you can.";
            }

            bool changed = await classRepository.instructorCancelClass(classRequest.classId);


            if (changed && (oldWay == false)) {
                await mailer.sendEmail("lockdown.squad.301@gmail.com", "Gym Moves", "Class Cancelled", content, emailReceiver);
                
                //Causes error
                /*foreach(ClassRegister user in classToChange.Registers) {
                    
                    await mailer.sendEmail("lockdown.squad.301@gmail.com", "Gym Moves", "Class Cancelled", content, user.Student.Email);
                }*/

                return Ok(returnMessage);
            }
            else if(changed && (oldWay == true)) {
                return Ok(returnMessage);
            }
            else {
                return StatusCode(500, "We were unable to change the class on our side. Please try again later.");
            }
        }

        [HttpPost("deregister")]
        public async Task<ActionResult<bool>> deregisterUserFromClass(RegisterUserForClassRequest register) 
        {
            if (await classRepository.getClassById(register.ClassId) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The class the user wants to signup for does not exist!");
            }

            if (await userRepository.getUser(register.Username) == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The user does not exist!");
            }

            ClassRegister existingRegister = await registerRepository.getSpecificUserAndClass(register.Username, register.ClassId);

            if (existingRegister == null) {
                return StatusCode(StatusCodes.Status404NotFound, "User is not signed up for the class!");
            }

            if (await registerRepository.removeRegister(existingRegister)) {

                DayOfWeek today = DateTime.Today.DayOfWeek;
                DateTime date;
                GymClasses targetClass = await classRepository.getClassById(register.ClassId);

                if (today.ToString().ToLower() == targetClass.Day.ToLower()) {
                   
                    date = DateTime.Today.Date;
                }
                else {
                   
                    DayOfWeek dayOfClass;

                    switch (targetClass.Day.ToLower()) {
                       
                        case "monday":
                            dayOfClass = DayOfWeek.Monday;
                            break;
                        case "tuesday":
                            dayOfClass = DayOfWeek.Tuesday;
                            break;
                        case "wednesday":
                            dayOfClass = DayOfWeek.Wednesday;
                            break;
                        case "thursday":
                            dayOfClass = DayOfWeek.Thursday;
                            break;
                        case "friday":
                            dayOfClass = DayOfWeek.Friday;
                            break;
                        case "saturday":
                            dayOfClass = DayOfWeek.Saturday;
                            break;
                        default:
                            dayOfClass = DayOfWeek.Sunday;
                            break;
                    }

                    int difference = days[(int)today, (int)dayOfClass];

                    date = DateTime.Today.AddDays(difference).Date;
                }

                ClassAttendance classToChange = await classAttendanceRepository.getClassInstance(register.ClassId, date);

                if (classToChange == null)
                {
                    ClassAttendance newClass = new ClassAttendance();
                    newClass.Date = date;
                    newClass.ClassId = targetClass.ClassId;
                    newClass.Capacity = targetClass.MaxCapacity;
                    newClass.Class = targetClass;
                    newClass.NumberOfStudents = targetClass.CurrentStudents;

                    await classAttendanceRepository.addNewClassInstance(newClass);
                }
                else
                {
                    await classAttendanceRepository.editClassAttendance(classToChange.ClassId, classToChange.Date, -1);
                }


                return Ok(true);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while attempting to remove the register from the database!");
            }
        }

        [HttpPost("remove")]
        public async Task<ActionResult<bool>> removeClass(GymClassRemoveRequest removeClass) {
            Users user = await userRepository.getUser(removeClass.Username);

            if (user == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The user requesting for the class to be removed could not be found!");
            }

            if (user.UserType == UserTypes.Manager) {
                GymClasses targetClass = await classRepository.getClassById(removeClass.ClassId);
                if (targetClass == null) {
                    return StatusCode(StatusCodes.Status404NotFound, "The class being removed was not found!");
                }

                if (targetClass.GymIdForeignKey != user.GymIdForeignKey) {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Managers can only remove class for their own gym!");
                }

                /* Multithread remove and notifying users */

                await classAttendanceRepository.removeClass(targetClass.ClassId);
                
                if (await classRepository.removeClass(targetClass) == true) {
                    return Ok(true);
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong removing the class from the database!");
                }
            }
   
            return StatusCode(StatusCodes.Status401Unauthorized, "Only managers can remove classes!");
        }

        [HttpGet("class")]
        public async Task<ActionResult<GymClassResponse>> getSpecificClass(int classid) {
            GymClasses targetClass = await classRepository.getClassById(classid);

            if (targetClass == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Specified class was not found!");
            }

            GymClassResponse convertedObject = ClassMappers.classModelToReducedModel(targetClass);

            return Ok(convertedObject);
        }

        [HttpGet("userclass")]
        public async Task<ActionResult<bool>> getSpecificUserAndClass(string username, int classid) {
            if (await userRepository.getUser(username) == null) {
                return StatusCode(StatusCodes.Status404NotFound, $"User with username {username} does not exist!");
            }

            if (await classRepository.getClassById(classid) == null) {
                return StatusCode(StatusCodes.Status404NotFound, $"Class with class id {classid} does not exist!");
            }

            if (await registerRepository.getSpecificUserAndClass(username, classid) != null) {
                return Ok(true);
            } else {
                return Ok(false);
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<EditGymClassRequest>> editClass(EditGymClassRequest edit) {
            Users user = await userRepository.getUser(edit.EditorUsername);

            if (user == null) {
                return StatusCode(StatusCodes.Status404NotFound, "User making the edit to the class was not found!");
            }

            if (user.UserType != UserTypes.Manager && user.UserType != UserTypes.Instructor) {
                return StatusCode(StatusCodes.Status401Unauthorized, "User making the edit is not a manager or instructor!");
            }

            GymClasses target = await classRepository.getClassById(edit.ClassId);

            if (target == null) {
                return StatusCode(StatusCodes.Status404NotFound, "The class you wish to edit does not exist!");
            }

            ClassMappers.editRequestToGymClassModel(edit, target);

            target = await classRepository.editClass(target);

            if (target != null) {
                return ClassMappers.classToClassRequestModel(target);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong trying to update the class!");
            }
        }

        [HttpGet("getAttendanceForClass")]
        public async Task<ActionResult<ClassAttendance[]>> getAttendanceForClass(int classId)
        {
            ClassAttendance[] classAttendance = await classAttendanceRepository.getClassAttendance(classId);

             return Ok(classAttendance);

        }

        /*Days and their differences for the other days*/
        int[,] days = new int[,] { 
            { 0, 1, 2, 3, 4, 5, 6 }, 
            { 6, 0, 1, 2, 3, 4, 5 }, 
            { 5, 6, 0, 1, 2, 3, 4 },
            { 4, 5, 6, 0, 1, 2, 3 },
            { 3, 4, 5, 6, 0, 1, 2 },
            { 2, 3, 4, 5, 6, 0, 1 },
            { 1, 2, 3, 4, 5, 6, 0 },
        };
    }
}
