/*
File Name:
    RatingsController.cs

Author:
    Longji

Date Created:
    30/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
30/07/2020    |  Longji        |  Created function to rate classes
--------------------------------------------------------------------------------
04/08/2020    |  Longji        |  Created function to rate instructors
--------------------------------------------------------------------------------
05/08/2020    |  Longji        |  Added functions to get rating for a specified
              |                |  instructor or class
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - RatingsController
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase {
        private readonly IClassRepository classRepository;
        private readonly IUserRepository userRepository;
        private readonly IClassRatingRepository classRatingRepository;
        private readonly IInstructorRatingRepository instructorRatingRepository;

        public RatingsController(IClassRepository cr, IUserRepository ur, IClassRatingRepository crr, IInstructorRatingRepository irr) {
            classRepository = cr;
            userRepository = ur;
            classRatingRepository = crr;
            instructorRatingRepository = irr;
        }

        [HttpPost("class")]
        public async Task<ActionResult<ClassRatingResponse>> rateClass(ClassRatingRequest request) {
            /* Validate User Exists */
            Users user = await userRepository.getUser(request.username);

            if (user == null) {
                return StatusCode(StatusCodes.Status401Unauthorized, "User specified is not a valid user!");
            }

            /* Validate Class Exists */
            GymClasses gymClass = await classRepository.getClassById(request.classId);

            if (gymClass == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Specified class does not exist!");
            }

            /* Validate From Same Gym */
            if (gymClass.GymIdForeignKey != user.GymIdForeignKey) {
                return StatusCode(StatusCodes.Status401Unauthorized, "User rating the class is not from the same gym as the class!");
            }

            /* Validate Not Instructor Of Class */
            if (gymClass.InstructorUsername == user.Username) {
                return StatusCode(StatusCodes.Status401Unauthorized, "Instructors cannot rate their own classes!");
            }

            /* Validate Rating Out Of 5 */
            if (request.rating > 5 || request.rating < 0) {
                return StatusCode(StatusCodes.Status400BadRequest, "Rating provided is out of allowed range!");
            }

            /* Check if existing rating record exists */
            ClassRating rating = await classRatingRepository.getRating(request.classId);

            if (rating == null) {
                rating = new ClassRating();

                rating.ClassIdForeignKey = request.classId;
                rating.RatingCount = 0;
                rating.RatingCount = 0;

                if (!(await classRatingRepository.addRating(rating))) {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong creating the rating record for the class!");
                }
            }

            rating.RatingCount++;
            rating.RatingSum += request.rating;

            if (!(await classRatingRepository.rateClass(rating))) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong adding your rating to the database!");
            }

            ClassRatingResponse response = new ClassRatingResponse();
            response.classId = rating.ClassIdForeignKey;
            response.ratingCount = rating.RatingCount;
            response.ratingSum = rating.RatingSum;

            return Ok(response);
        }

        [HttpPost("instructor")]
        public async Task<ActionResult<InstructorRatingResponse>> rateInstructor(InstructorRatingRequest request) {
            if (request.username == request.instructor) {
                return StatusCode(StatusCodes.Status403Forbidden, "Instructors cannot rate themselves!");
            }

            Users user = await userRepository.getUser(request.username);

            if (user == null) {
                return StatusCode(StatusCodes.Status401Unauthorized, "User attempting to make the rating does not exist!");
            }

            Users instructor = await userRepository.getUser(request.instructor);

            if (instructor == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Instructor not found!");
            }

            if (instructor.UserType != UserTypes.Instructor) {
                return StatusCode(StatusCodes.Status403Forbidden, "The user you're trying to rate is not an instructor!");
            }

            if (user.GymIdForeignKey != instructor.GymIdForeignKey) {
                return StatusCode(StatusCodes.Status401Unauthorized, "You cannot rate an instructor from another gym!");
            }

            if (request.rating > 5 || request.rating < 0) {
                return StatusCode(StatusCodes.Status400BadRequest, "Rating provided is out of allowed range!");
            }

            InstructorRating rating = await instructorRatingRepository.getRating(instructor.Username);

            if (rating == null) {
                rating = new InstructorRating();
                rating.InstructorUsernameForeignKey = instructor.Username;
                rating.RatingCount = 0;
                rating.RatingSum = 0;

                if (!(await instructorRatingRepository.addRating(rating))) {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong creating the rating record for the instructor!");
                }
            }

            rating.RatingCount++;
            rating.RatingSum += request.rating;

            if (await instructorRatingRepository.rateClass(rating)) {
                InstructorRatingResponse response = new InstructorRatingResponse();

                response.instructor = rating.InstructorUsernameForeignKey;
                response.ratingCount = rating.RatingCount;
                response.ratingSum = rating.RatingSum;

                return Ok(response);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong adding your rating to the database!");
            }
        }

        [HttpGet("class")]
        public async Task<ActionResult<ClassRatingResponse>> getClassRating(int classid) {
            GymClasses gymClass = await classRepository.getClassById(classid);

            if (gymClass == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Specified class does not exist!");
            }

            ClassRating rating = await classRatingRepository.getRating(classid);
            ClassRatingResponse response = new ClassRatingResponse();

            if (rating == null) {
                response.classId = classid;
                response.ratingCount = 0;
                response.ratingSum = 0;
            } else {
                response.classId = rating.ClassIdForeignKey;
                response.ratingCount = rating.RatingCount;
                response.ratingSum = rating.RatingSum;
            }

            return Ok(response);
        }

        [HttpGet("instructor")]
        public async Task<ActionResult<InstructorRatingResponse>> getInstructorRating(string instructor) {
            Users instructorUser = await userRepository.getUser(instructor);

            if (instructorUser == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Instructor not found!");
            }

            if (instructorUser.UserType != UserTypes.Instructor) {
                return StatusCode(StatusCodes.Status403Forbidden, "User specified is not an instructor!");
            }

            InstructorRating rating = await instructorRatingRepository.getRating(instructor);
            InstructorRatingResponse response = new InstructorRatingResponse();

            if (rating == null) {
                response.instructor = instructor;
                response.ratingCount = 0;
                response.ratingSum = 0;
            } else {
                response.instructor = rating.InstructorUsernameForeignKey;
                response.ratingCount = rating.RatingCount;
                response.ratingSum = rating.RatingSum;
            }

            return Ok(response);
        }
    }
}
