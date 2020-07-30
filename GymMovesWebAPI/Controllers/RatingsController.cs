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

Functional Description:
    

List of Classes:
    - RatingsController
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public RatingsController(IClassRepository cr, IUserRepository ur, IClassRatingRepository crr) {
            classRepository = cr;
            userRepository = ur;
            classRatingRepository = crr;
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
    }
}
