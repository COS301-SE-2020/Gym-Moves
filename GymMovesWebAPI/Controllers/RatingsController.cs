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

using System.Threading.Tasks;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase {
        private readonly IClassRepository classRepository;
        private readonly IUserRepository userRepository;
        private readonly IClassRatingRepository classRatingRepository;
        private readonly IInstructorRatingRepository instructorRatingRepository;
        private readonly IGymRepository gymRepository;

        public RatingsController(IClassRepository cr, IUserRepository ur, IClassRatingRepository crr, IInstructorRatingRepository irr, IGymRepository gr) {
            classRepository = cr;
            userRepository = ur;
            classRatingRepository = crr;
            instructorRatingRepository = irr;
            gymRepository = gr;
        }

        [HttpGet("gym")]
        public async Task<ActionResult<AllClassRatingResponse[]>> getGymClassRatings(int gymid) {
            Gym gym = await gymRepository.getGymById(gymid);

            if (gym == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Specified gym does not exist!");
            }

            GymClasses[] classes = await classRepository.getGymClasses(gymid);
            AllClassRatingResponse[] responses = new AllClassRatingResponse[classes.Length];

            for (int i = 0; i < classes.Length; i++) {
                ClassRating rating = await classRatingRepository.getRating(classes[i].ClassId);

                if (rating == null) {
                    rating = new ClassRating();

                    rating.ClassIdForeignKey = classes[i].ClassId;
                    rating.RatingCount = 0;
                    rating.RatingSum = 0;
                }

                responses[i] = new AllClassRatingResponse();

                responses[i].Name = classes[i].Name;
                responses[i].Instructor = classes[i].InstructorUsername;
                responses[i].Day = classes[i].Day;
                responses[i].Time = classes[i].StartTime;
                responses[i].RatingSum = rating.RatingSum;
                responses[i].RatingCount = rating.RatingCount;
            }

            return Ok(responses);
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
