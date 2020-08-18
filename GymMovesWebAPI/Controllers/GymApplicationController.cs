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
15/08/20      | Raeesa         | Added accept applications controller
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - GymApplicationController
*/

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.MailerProgram;
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
        private readonly IApplicationCodeRepository codeRepository;
        private readonly IMailer mailer;

        public GymApplicationController(IGymApplicationRepository gar, IGymRepository gr, ISupportStaffRepository ssr, IApplicationCodeRepository acr, IMailer mail) {
            applicationRepository = gar;
            gymRepository = gr;
            staffRepository = ssr;
            codeRepository = acr;
            mailer = mail;
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

        [HttpPost("AcceptApplications")]
        public async Task<ActionResult<GymApplications[]>> AcceptApplications(SetApplicationState request)
        {
            GymApplications[] application = await applicationRepository.getApplication(request.GymName, request.BranchName);
            if (request.status == "Approve")
            {
                //GymApplications[] application = await applicationRepository.getApplication(request.GymName, request.BranchName);
                application[0].Status = ApplicationStatus.Approved;
                Gym newgym = new Gym();
                newgym.GymName = request.GymName.Trim();
                newgym.GymBranch = request.BranchName.Trim();
                bool creategym = await gymRepository.addGym(newgym);
                bool updateStatus = await applicationRepository.updateApplication(application[0]);

                /* Generate Code For Sign Up */
                string generated = getRandomString(10);

                GymApplicationCodes code = await codeRepository.getByCode(generated);

                while (code != null) {
                    generated = getRandomString(10);
                    code = await codeRepository.getByCode(generated);
                }

                code = new GymApplicationCodes();

                code.BranchName = newgym.GymBranch;
                code.GymName = newgym.GymName;
                code.Code = generated;

                if (!await codeRepository.add(code)) {
                    /* Remove gym from database and do not change application status */
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unable to store generated code!");
                }

                await mailer.sendEmail("lockdown.squad.301@gmail.com", "Gym Moves", "Gym Application Approval", $"Congratulations! Your gym approval has been approved! Use the following code {generated}\nAt ", "kanglongjidev@gmail.com");
            }


            if (request.status == "Reject")
            {
               // GymApplications[] application = await applicationRepository.getApplication(request.GymName, request.BranchName);
                application[0].Status = ApplicationStatus.Rejected;
                bool updateStatus = await applicationRepository.updateApplication(application[0]);
            }

            return Ok(application);

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
        public static string getRandomString(int length) {

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder salt = new StringBuilder();

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            while (length > 0) {
                salt.Append(valid[getInt(random, valid.Length)]);
                length--;
            }

            return salt.ToString();
        }

        public static int getInt(RNGCryptoServiceProvider random, int max) {
            byte[] byteChar = new byte[4];
            int value;

            do {
                random.GetBytes(byteChar);
                value = BitConverter.ToInt32(byteChar, 0) & Int32.MaxValue;
            }
            while (value >= max * (Int32.MaxValue / max));

            return value % max;
        }
    }
}
