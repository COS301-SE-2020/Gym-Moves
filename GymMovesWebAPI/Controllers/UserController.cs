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


namespace GymMovesWebAPI.Controllers {

    /*
    Class Name:
        UserController

    Purpose:
        This class handles the different requests that can be sent
        to the API with regards to the user management.
    */
    [Route("api/user/[controller]")]
    [ApiController]
    public class UserController : Controller {

        private readonly IUserRepository userGymMovesRepository;
        private readonly IGymMemberRepository gymMembersRepository;
        private readonly IGymRepository gymRepository;
        private readonly INotificationSettingsRepository notificationSettingRepository;
        private readonly IPasswordResetRepository resetPasswordRepository;
        private readonly IMailer mailer;

        /*
        Method Name:
            UserController
        Purpose:
            This method instantiates the repositories that will be used.
        */
        public UserController(IUserRepository repoOne, IGymMemberRepository repoTwo, IGymRepository repoThree,
            INotificationSettingsRepository repoFour, IPasswordResetRepository repoFive, IMailer repoSix) {

            userGymMovesRepository = repoOne;
            gymMembersRepository = repoTwo;
            gymRepository = repoThree;
            notificationSettingRepository = repoFour;
            resetPasswordRepository = repoFive;
            mailer = repoSix;
        }

        /*
        Method Name:
            signUp
        Purpose:
            This method handles a user sign up request.
       */
        [HttpPost("signup")]
        public async Task<ActionResult<SignUpResponseModel>> signUp(SignUpRequestModel user) {

            SignUpResponseModel response = new SignUpResponseModel();

            Users newUserAccount = new Users();

            newUserAccount.MembershipId = user.gymMemberId.Trim();
            newUserAccount.Username = user.username.Trim();
            newUserAccount.Gym = await gymRepository.getGymByNameAndBranch(user.gymName.Trim(), user.gymBranch.Trim());
            newUserAccount.GymIdForeignKey = newUserAccount.Gym.GymId;

          
            GymMember member = await gymMembersRepository.getMember(newUserAccount.MembershipId, 
                newUserAccount.GymIdForeignKey);

            /* If member null, such person does not exist. */
            if (member == null) {
                response.usernameValid = true;
                response.gymMemberIdValid = false;
                response.userType = 0;

                response.name = "";
                response.gymMemberID = "";
                response.gymID = -1;

                return Unauthorized(response);
            }

            /* Get the member from the user repo that has the entered member ID and gym. */
            Users checkIfUserHasAccount = await userGymMovesRepository.getUserByMemberID(member.MembershipId, 
                member.GymId);

            /* If null, person has account.*/
            if(checkIfUserHasAccount != null) {
                response.usernameValid = false;
                response.gymMemberIdValid = false;
                response.userType = 0;

                response.name = "";
                response.gymMemberID = "";
                response.gymID = -1;

                return BadRequest(response);
            }


            newUserAccount.Name = member.Name;
            newUserAccount.Surname = member.Surname;
            newUserAccount.PhoneNumber = member.PhoneNumber;
            newUserAccount.Email = member.Email;

            /* Since person exists, get their user type. */
            newUserAccount.UserType = member.UserType;

            /* Need to check if username unique. */
            Users checkIfUsernameExists = await userGymMovesRepository.getUser(newUserAccount.Username);

            /* If not null, it means username already exists.*/
            if (checkIfUsernameExists != null) {
                response.usernameValid = false;
                response.gymMemberIdValid = true;
                response.userType = 0;

                response.name = "";
                response.gymMemberID = "";
                response.gymID = -1;

                return Unauthorized(response);
            }

            /* random will generate different salt lengths of between 5 and 10. */
            Random random = new Random();
            newUserAccount.Salt = getRandomString(random.Next(5, 10));

            string hash = getHash(SHA256.Create(), user.password + newUserAccount.Salt);

            /* Set new accounts password as the hash. */
            newUserAccount.Password = hash;

            /* Check if new account added. */
            bool added = await userGymMovesRepository.addUser(newUserAccount);

            if (added) {

                NotificationSettings newUserNotifs = new NotificationSettings();

                newUserNotifs.Email = false;
                newUserNotifs.PushNotifications = true;
                newUserNotifs.UsernameForeignKey = newUserAccount.Username;
                newUserNotifs.User = newUserAccount;

                added = await notificationSettingRepository.addUser(newUserNotifs);

                response.usernameValid = true;
                response.gymMemberIdValid = true;
                response.userType = newUserAccount.UserType;

                response.name = newUserAccount.Name;
                response.gymMemberID = newUserAccount.MembershipId;
                 response.gymID = newUserAccount.GymIdForeignKey;

                return Ok(response);
            }
            else {

                response.usernameValid = false;
                response.gymMemberIdValid = false;
                response.userType = 0;

                response.name = "";
                response.gymMemberID = "";
                response.gymID = -1;

                return StatusCode(500, response);

            }

        }

        /*
        Method Name:
            logIn
        Purpose:
            This method handles a user login request.
       */
        [HttpPost("login")]
        public async Task<ActionResult<LogInResponseModel>> logIn(LogInRequestModel user) {

            LogInResponseModel response = new LogInResponseModel();

            Users checkUser = await userGymMovesRepository.getUser(user.username.Trim());

            /* If null, no user with that username exists.*/
            if (checkUser == null) {

                response.usernameValid = false;
                response.passwordValid = true;
                response.userType = 0;

                response.name = "";
                response.gymMemberID = "";
                response.gymID = -1;

                return Unauthorized(response);
            }

            /* Verify correct password has been entered.*/
            if (verifyHash(SHA256.Create(), user.password + checkUser.Salt, checkUser.Password)) {

                response.usernameValid = true;
                response.passwordValid = true;
                response.userType = checkUser.UserType;

                response.name = checkUser.Name;
                response.gymMemberID = checkUser.MembershipId;
                response.gymID = checkUser.GymIdForeignKey;

                return Ok(response);
            }
            else {
                response.usernameValid = true;
                response.passwordValid = false;
                response.userType = 0;

                response.name = "";
                response.gymMemberID = "";
                response.gymID = -1;

                return Unauthorized(response);
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
        public async Task<ActionResult<string>> getCode(string username)  {

            /* Check if user already has a code.*/
            PasswordReset checkIfHasCode = await resetPasswordRepository.getUser(username);

            if (checkIfHasCode != null) {
               bool deleted =  await resetPasswordRepository.deleteUser(checkIfHasCode);

                if (!deleted) {
                    return StatusCode(500, "You have a code but we cannot generate a new one for you.");
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
                        message, "u18008659@tuks.co.za");

                    if(statusCode == 202) {
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
                return Unauthorized("This is not a valid username.");
            }
        }

        /*
        Method Name:
            forgotPassword
        Purpose:
            This method will change the password for the user and check they entered the correct code.
       */
        [HttpPost("forgotPassword")]
        public async Task<ActionResult<string>> forgotPassword(ForgotPasswordRequestModel user){

            /* Get the user record to change.*/
            Users userToChange = await userGymMovesRepository.getUser(user.username);
            
            /* No such user exists.*/
            if (userToChange == null) {
                return Unauthorized("This username does not exist! Are you sure you typed the correct one?");
            }


            /*Get code from code table to see if it matches.*/
            PasswordReset userInCodeTabe = await resetPasswordRepository.getUser(user.username);

            /* No such user exists.*/
            if (userInCodeTabe == null) {
                return Unauthorized("This user does not have a code! Are you sure you received one?");
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
                    return StatusCode(500, "We were unable to change your password.");
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
        public async Task<ActionResult<string>> changePassword(ChangePasswordRequestModel user) {

            /* Get the user record to change.*/
            Users userToChange = await userGymMovesRepository.getUser(user.username);

            /* If null, user does not exit.*/
            if (userToChange == null) {
                return Unauthorized("This user doesn't exist.");
            }

            /* Verifies the old password matches the one in the db. */
            if(verifyHash(SHA256.Create(), user.oldPassword + userToChange.Salt, userToChange.Password))  {
                
                /* Hash new password.*/
                string hash = getHash(SHA256.Create(), user.newPassword + userToChange.Salt);

                /* Try change password in db.*/
                bool changed = await userGymMovesRepository.changePassword(userToChange.Username, hash);

                if (changed) {
                    return Ok("Successully changed your password.");
                }
                else {
                    return StatusCode(500, "Unable to change your password now, please try again later.");
                }
            }
            else {
                return Unauthorized("The password you entered is not correct.");
            }    
        }
    }
}