/*
File Name:
    UserController.cs

Author:
    Danel

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    | Danel          | Added response model changes and password stuff
--------------------------------------------------------------------------------
09/07/2020    | Danel          | Added working email send
--------------------------------------------------------------------------------
14/07/2020    | Danel          | Change password fix and get instructors
--------------------------------------------------------------------------------



Functional Description:
    This file implements the controller that will handle all user manangement
    activities.

List of Classes:
    - UserController

 */

using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using GymMovesWebAPI.MailerProgram;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http;
using GymMovesWebAPI.Data.Repositories.Implementations;
using GymMovesWebAPI.Data.Enums;

namespace GymMovesWebAPI.Controllers {

    /*
    Class Name:
        UserController

    Purpose:
        This class handles the different requests that can be sent
        to the API with regards to the user management.
    */
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller {

        private readonly IUserRepository userGymMovesRepository;
        private readonly IGymMemberRepository gymMembersRepository;
        private readonly IGymRepository gymRepository;
        private readonly INotificationSettingsRepository notificationSettingRepository;
        private readonly IPasswordResetRepository resetPasswordRepository;
        private readonly IGymApplicationRepository applicationRepository;
        private readonly IApplicationCodeRepository codeRepository;
        private readonly IMailer mailer;

        string emailReceiver = "u18008659@tuks.co.za";

        /*
        Method Name:
            UserController
        Purpose:
            This method instantiates the repositories that will be used.
        */
        public UserController(IUserRepository repoOne, IGymMemberRepository repoTwo, IGymRepository repoThree,
            INotificationSettingsRepository repoFour, IPasswordResetRepository repoFive, IMailer repoSix,
            IGymApplicationRepository repoSeven, IApplicationCodeRepository repoEight) {

            userGymMovesRepository = repoOne;
            gymMembersRepository = repoTwo;
            gymRepository = repoThree;
            notificationSettingRepository = repoFour;
            resetPasswordRepository = repoFive;
            mailer = repoSix;
            applicationRepository = repoSeven;
            codeRepository = repoEight;
        }

        /*
        Method Name:
            signUp
        Purpose:
            This method handles a user sign up request.
       */
        [HttpPost("signup")]
        public async Task<ActionResult<UserResponseModel>> signUp(SignUpRequestModel user) {

            Users newUserAccount = new Users();

            if(user.gymMemberId == null){
                return BadRequest("A gym member ID is needed to sign up.");
            }

            if (user.username == null){
                return BadRequest("A username is needed to sign up.");
            }

            if (user.gymName == null)
            {
                return BadRequest("A gym name is needed to sign up.");
            }

            if (user.gymBranch == null)
            {
                return BadRequest("A gym branch is needed to sign up.");
            }

            newUserAccount.MembershipId = user.gymMemberId.Trim();
            newUserAccount.Username = user.username.Trim();
            newUserAccount.Gym = await gymRepository.getGymByNameAndBranch(user.gymName.Trim(), user.gymBranch.Trim());


            if (newUserAccount.Gym == null)
            {
                return BadRequest("A gym with this name and branch does not exist.");
            }


            newUserAccount.GymIdForeignKey = newUserAccount.Gym.GymId;

            GymMember member = await gymMembersRepository.getMember(newUserAccount.MembershipId,
                newUserAccount.GymIdForeignKey);

            if (member == null)
            {
               Users checkIfUserHasAccount = await userGymMovesRepository.getUserByMemberID(newUserAccount.MembershipId,
               newUserAccount.GymIdForeignKey);

                if (checkIfUserHasAccount != null)
                {
                    return BadRequest("There is already an account for this gym member.");
                }

                return Unauthorized("This gym member ID does not exist.");
            }


            newUserAccount.Name = member.Name;
            newUserAccount.Surname = member.Surname;
            newUserAccount.PhoneNumber = member.PhoneNumber;
            newUserAccount.Email = member.Email;

            newUserAccount.UserType = member.UserType;

            Users checkIfUsernameExists = await userGymMovesRepository.getUser(newUserAccount.Username);

           if (checkIfUsernameExists != null) {
                return Unauthorized("This username is already in use.");
           }

            Random random = new Random();
            newUserAccount.Salt = getRandomString(random.Next(5, 10));
            string hash = getHash(SHA256.Create(), user.password + newUserAccount.Salt);
            newUserAccount.Password = hash;

            bool added = await userGymMovesRepository.addUser(newUserAccount);

            if (added) {

                NotificationSettings newUserNotifs = new NotificationSettings();

                newUserNotifs.Email = true;
                newUserNotifs.PushNotifications = false;
                newUserNotifs.UsernameForeignKey = newUserAccount.Username;
                newUserNotifs.User = newUserAccount;

                added = await notificationSettingRepository.addUser(newUserNotifs);

                UserResponseModel response = new UserResponseModel();
                response.userType = newUserAccount.UserType;
                response.name = newUserAccount.Name;
                response.gymMemberID = newUserAccount.MembershipId;
                response.gymID = newUserAccount.GymIdForeignKey;

                await gymMembersRepository.deleteMember(member);

                return Ok(response);
            }
            else {
                return StatusCode(500, "Unable to create this account right now.");

            }

        }

        [HttpPost("firstmanager")]
        public async Task<ActionResult<UserResponseModel>> firstManager(FirstManagerModel user) {
            GymApplicationCodes code = await codeRepository.getByCode(user.Code);

            if (code == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Invalid code!");
            }

            Users existingUser = await userGymMovesRepository.getUser(user.Username);

            if (existingUser != null) {
                return StatusCode(StatusCodes.Status400BadRequest, "A user with that username already exists!");
            }

            GymApplications[] applications = await applicationRepository.getApplication(code.GymName, code.BranchName);

            if (applications.Length == 0) {
                return StatusCode(StatusCodes.Status404NotFound, "Application for gym not found!");
            }

            GymApplications application = applications[0];
            Gym gym = await gymRepository.getGymByNameAndBranch(code.GymName, code.BranchName);

            existingUser = new Users();

            existingUser.GymIdForeignKey = gym.GymId;
            existingUser.Username = user.Username;
            existingUser.MembershipId = $"{code.GymName}{code.BranchName}defaultmanager";
            existingUser.Name = application.Name;
            existingUser.Surname = application.Surname;
            existingUser.PhoneNumber = application.PhoneNumber;
            existingUser.Email = application.Email;
            existingUser.UserType = UserTypes.Manager;

            Random random = new Random();
            existingUser.Salt = getRandomString(random.Next(5, 10));
            string hash = getHash(SHA256.Create(), user.Password + existingUser.Salt);
            existingUser.Password = hash;

            if (await userGymMovesRepository.addUser(existingUser)) {
                await applicationRepository.removeApplication(application);
                await codeRepository.remove(code);

                UserResponseModel response = new UserResponseModel();

                response.gymID = existingUser.GymIdForeignKey;
                response.gymMemberID = existingUser.MembershipId;
                response.name = existingUser.Name;
                response.userType = existingUser.UserType;

                return Ok(response);
            } else {
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occured while adding the manager account!");
            }
        }
        
        /*
        Method Name:
            logIn
        Purpose:
            This method handles a user login request.
       */
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseModel>> logIn(LogInRequestModel user) {

            if(user.username == null)
            {
                return BadRequest("A username is required to login.");
            }

            if (user.password == null)
            {
                return BadRequest("A password is required to login.");
            }

            Users checkUser = await userGymMovesRepository.getUser(user.username.Trim());

            /* If null, no user with that username exists.*/
            if (checkUser == null) {
                return NotFound("Nobody with that username exists.");
            }

            /* Verify correct password has been entered.*/
            if (verifyHash(SHA256.Create(), user.password + checkUser.Salt, checkUser.Password)) {

                UserResponseModel response = new UserResponseModel();
                response.userType = checkUser.UserType;
                response.name = checkUser.Name;
                response.gymMemberID = checkUser.MembershipId;
                response.gymID = checkUser.GymIdForeignKey;

                return Ok(response);
            }
            else {
                return Unauthorized("Password is incorrect.");
            }

        }

        /*
        Method Name:
            getRandomString
        Purpose:
            This method generates the salt.
       */
        public static string getRandomString(int length)
        {

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder salt = new StringBuilder();

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            while (length > 0)
            {
                salt.Append(valid[getInt(random, valid.Length)]);
                length--;
            }

            return salt.ToString();
        }

        /*
        Method Name:
            getInt
        Purpose:
            This method gets the int of a byte.
       */
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

        /*
        Method Name:
            getHash
        Purpose:
            This method will get the hash of the password and the salt.
       */
        private static string getHash(HashAlgorithm hashAlgorithm, string input) {

            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var passwordStored = new StringBuilder();

            for (int i = 0; i < data.Length; i++) {
                passwordStored.Append(data[i].ToString("x2"));
            }

            return passwordStored.ToString();
        }

        /*
        Method Name:
            verifyHash
        Purpose:
            This method will verify that the hash matches the hash stored in the database.
       */
        private static bool verifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            var hashOfPassword = getHash(hashAlgorithm, input);
            return hashOfPassword.Equals(hash);
        }

        /*
        Method Name:
            getCode
        Purpose:
            This method will send a code via email to the user.
       */
       [HttpGet("getCode")]
        public async Task<ActionResult> getCode(string username)  {

            if(username == null)
            {
                return BadRequest("Your username is needed to receive a code.");
            }

            /* Check if user already has a code.*/
            PasswordReset checkIfHasCode = await resetPasswordRepository.getUser(username);

            if (checkIfHasCode != null) {
               bool deleted =  await resetPasswordRepository.deleteUser(checkIfHasCode);

                if (!deleted) {
                    return StatusCode(500, "You have a code already and we cannot generate a " +
                        "new one for you.");
                }
            }


            /*Get user with is username.*/
            Users member = await userGymMovesRepository.getUser(username);

            if (member != null)  {

                string code = getRandomString(8);

                DateTime date = DateTime.Now;
                date = date.AddHours(2);

                string message = "The code for " + member.Name + " " + member.Surname +
                    " is " + code + ". This is only valid until " + date.ToLocalTime() + ".";


                PasswordReset userToAdd = new PasswordReset();
                userToAdd.Username = username;
                userToAdd.User = member;
                userToAdd.Code = code;
                userToAdd.Expiry = date;

                bool added = await resetPasswordRepository.addUser(userToAdd);

                if(added) {
                    int statusCode = await mailer.sendEmail("lockdown.squad.301@gmail.com", "Gym Moves", "Gym Moves Code", 
                        message, member.Email);

                    await mailer.sendEmail("lockdown.squad.301@gmail.com", "Gym Moves", "Gym Moves Code",
                        message, emailReceiver);

                    if (statusCode == 202) {
                        return Ok();
                    }
                    else {
                        await resetPasswordRepository.deleteUser(userToAdd);
                        return StatusCode(500, "Email could not be sent.");
                    }

                }
                else {
                    return StatusCode(500, "We could not generate a code for you.");
                }

            }
            else {
                return NotFound("This is not a valid username.");
            }
        }

        /*
        Method Name:
            forgotPassword
        Purpose:
            This method will change the password for the user and check they entered the correct code.
       */
        [HttpPost("forgotPassword")]
        public async Task<ActionResult> forgotPassword(ForgotPasswordRequestModel user){

            /* Get the user record to change.*/
            Users userToChange = await userGymMovesRepository.getUser(user.username);
            
            /* No such user exists.*/
            if (userToChange == null) {
                return NotFound("This username does not exist! Are you sure you typed the correct one?");
            }

            if(user.password == null)
            {
                return BadRequest("A password is needed to reset password.");
            }

            if (user.code == null)
            {
                return BadRequest("A code is needed to reset password.");
            }


            /*Get code from code table to see if it matches.*/
            PasswordReset userInCodeTabe = await resetPasswordRepository.getUser(user.username);

            /* No such user exists.*/
            if (userInCodeTabe == null) {
                return NotFound("This user does not have a code! Are you sure you received one?");
            }


            DateTime dateTime = DateTime.Now;

            if ((dateTime.Subtract(userInCodeTabe.Expiry)).TotalMinutes > 60) {
                await resetPasswordRepository.deleteUser(await resetPasswordRepository.getUser(user.username));
                return Unauthorized("Your code is expired. Please get a new one.");
            }

            if (userInCodeTabe.Code.Equals((user.code).Trim())) {
                 /* Hash new password.*/
                string hash = getHash(SHA256.Create(), user.password + userToChange.Salt);

                /* Try change password in db.*/
                bool changed = await userGymMovesRepository.changePassword(userToChange.Username, hash);

                if (changed) {
                   await resetPasswordRepository.deleteUser(userInCodeTabe);
                    return Ok("Successfully changed! You can log in with your new password now.");
                }
                else {
                    await resetPasswordRepository.deleteUser(userInCodeTabe);
                    return StatusCode(500, "This is already your existing password.");
                }

            }
            else {
                return Unauthorized("This code is incorrect.");
            }
        }


        /*
        Method Name:
            changePassword
        Purpose:
            This method will change the password for the user.
       */
        [HttpPost("changePassword")]
        public async Task<ActionResult> changePassword(ChangePasswordRequestModel user) {

            Users userToChange = await userGymMovesRepository.getUser(user.username);

            if (userToChange == null) {
                return NotFound("This user doesn't exist.");
            }

            if (user.newPassword == null)
            {
                return BadRequest("A new password is needed in order to change password.");
            }

            if (user.oldPassword == null)
            {
                return BadRequest("The current password is needed in order to change password.");
            }

            /* Verifies the old password matches the one in the db. */
            if (verifyHash(SHA256.Create(), user.oldPassword + userToChange.Salt, userToChange.Password))  {
                
                /* Hash new password.*/
                string hash = getHash(SHA256.Create(), user.newPassword + userToChange.Salt);

                /* Try change password in db.*/
                bool changed = await userGymMovesRepository.changePassword(userToChange.Username, hash);

                if (changed) {
                    return Ok("Successully changed your password.");
                }
                else {
                    return StatusCode(500, "This is already your password.");
                }
            }
            else {
                return Unauthorized("The password you entered is not correct.");
            }    
        }

         /*
        Method Name:
            allInstructors
        Purpose:
            This method will return all instructors for a specific gym.
       */
        [HttpGet("allInstructors")]
        public async Task<ActionResult<InstructorResponseModel[]>> getAllInstructors(int gymID) {

            Users[] instructors = await userGymMovesRepository.getAllInstructors(gymID);

            if (instructors == null)  {
                return BadRequest("There was an error wih retrieving the instructors.");
            }

            List<InstructorResponseModel> instructorsToReturn = new List<InstructorResponseModel>();

            foreach(Users user in instructors) {
                
                instructorsToReturn.Add(new InstructorResponseModel  {
                    name = user.Name,
                    surname = user.Surname,
                    username = user.Username
                });
            }

            return Ok(instructorsToReturn);
        }

    }
}
 
 