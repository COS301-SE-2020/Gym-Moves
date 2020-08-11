/*
File Name:
    GymApplicationController.cs

Author:
    Longji

Date Created:
    10/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
10/08/2020    |  Longji        | Created the controller
--------------------------------------------------------------------------------


Functional Description:
    

List of Classes:
    - GymApplicationController
*/

using System.Threading.Tasks;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GymApplicationController : ControllerBase {
        private readonly IGymApplicationRepository applicationRepository;
        private readonly IGymRepository gymRepository;
        private readonly ISupportStaffRepository staffRepository;

        public GymApplicationController(IGymApplicationRepository gar, IGymRepository gr, ISupportStaffRepository ssr) {
            applicationRepository = gar;
            gymRepository = gr;
            staffRepository = ssr;
        }

        [HttpPost("add")]
        public async Task<ActionResult<GymApplications>> addApplication(GymApplicationsRequest application) {
            Gym gym = await gymRepository.getGymByNameAndBranch(application.GymName, application.BranchName);

            if (gym != null) {
                return StatusCode(StatusCodes.Status400BadRequest, $"Gym with name {application.GymName} and branch {application.BranchName} is already taken!");
            }

            if ((await applicationRepository.getApplication(application.GymName, application.BranchName)).Length != 0) {
                return StatusCode(StatusCodes.Status400BadRequest, "An application for this gym is already in progress!");
            }

            GymApplications existingApplication = new GymApplications();

            existingApplication.Name = application.Name;
            existingApplication.Surname = application.Surname;
            existingApplication.Email = application.Email;
            existingApplication.PhoneNumber = application.PhoneNumber;
            existingApplication.GymName = application.GymName;
            existingApplication.BranchName = application.BranchName;
            existingApplication.Address = application.Address;
            existingApplication.Extra = application.Extra;
            existingApplication.Status = ApplicationStatus.Pending;

            if (await applicationRepository.addApplication(existingApplication)) {
                return Ok(existingApplication);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "We were unable to add your application to the database!");
            }
        }

        [HttpPost("GetAllApplications")]
        public async Task<ActionResult<GymApplications[]>> getAllApplications(GetAllApplicationsRequest request) {
            if (request.Username == "") {
                return StatusCode(StatusCodes.Status400BadRequest, "Staff username cannot be empty!");
            }
            
            SupportUsers staff = await staffRepository.getStaff(request.Username);

            if (staff == null) {
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid staff member!");
            }

            GymApplications[] applications = await applicationRepository.getAllApplications();

            return Ok(applications);
        }

        [HttpPost("GetApplication")]
        public async Task<ActionResult<GymApplications[]>> getApplication(GetAnApplicationRequest request) {
            if (request.Username == "") {
                return StatusCode(StatusCodes.Status400BadRequest, "Staff username cannot be empty!");
            }

            if (request.GymName == "") {
                return StatusCode(StatusCodes.Status400BadRequest, "Gym name cannot be empty!");
            }

            SupportUsers staff = await staffRepository.getStaff(request.Username);

            if (staff == null) {
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid staff member!");
            }

            GymApplications[] application = await applicationRepository.getApplication(request.GymName, request.BranchName);

            return Ok(application);
        }

        /* TODO: Create API function to update the state of each application */
        /* Find way to minimize  */
    }
}
