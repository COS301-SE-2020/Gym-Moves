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

using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

using MailKit.Net.Smtp;
using MimeKit;

namespace GymMovesWebAPI.Controllers {

    /*
    Class Name:
        UserController

    Purpose:
        This class handles the different requests that can be sent
        to the API with regards to the user management.
    */
    [ApiController]
    public class UserController : Controller {

        private readonly IUserRepository userGymMovesRepository;
        private readonly IGymMemberRepository gymMembersRepository;
        private readonly IGymRepository gymRepository;
        private readonly INotificationSettingsRepository notificationSettingRepository;
        private readonly IPasswordResetRepository resetPasswordRepository;

        /*
        Method Name:
            UserController
        Purpose:
            This method instantiates the repositories that will be used.
        */
        public UserController(IUserRepository repoOne, IGymMemberRepository repoTwo, IGymRepository repoThree,
            INotificationSettingsRepository repoFour, IPasswordResetRepository repoFive) {

            userGymMovesRepository = repoOne;
            gymMembersRepository = repoTwo;
            gymRepository = repoThree;
            notificationSettingRepository = repoFour;
            resetPasswordRepository = repoFive;
        }

        /*
        Method Name:
            signUp
        Purpose:
            This method handles a user sign up request.
       */
        [Route("api/signup")]
        [HttpPost]
        public async Task<ActionResult<UserSignUpResponseModel>> signUp(SignUpUserModel user) {

            /* Create return message to send back.*/
            UserSignUpResponseModel response = new UserSignUpResponseModel();

            /* New user account being made.*/
            Users newUserAccount = new Users();

            /* Set the new accounts member ID and username. */
            newUserAccount.MembershipId = user.gymMemberId.Trim();
            newUserAccount.Username = user.username.Trim();

            /* Set the users gym and gym ID. */
            newUserAccount.Gym = await gymRepository.getGymByNameAndBranch(user.gymName.Trim(), user.gymBranch.Trim());
            newUserAccount.GymIdForeignKey = newUserAccount.Gym.GymId;

            /* Get the member from the user repo that has the entered member ID and gym. */
            GymMember member = await gymMembersRepository.getMember(newUserAccount.MembershipId, newUserAccount.GymIdForeignKey);

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
                newUserNotifs.Sms = false;
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

            response.usernameValid = false;
            response.gymMemberIdValid = false;
            response.userType = 0;

            response.name = "";
            response.gymMemberID = "";
            response.gymID = -1;

            return Unauthorized(response);

        }

        /*
        Method Name:
            logIn
        Purpose:
            This method handles a user login request.
       */
        [Route("api/login")]
        [HttpPost]
        public async Task<ActionResult<UserLogInResponseModel>> logIn(LogInUserModel user) {

            /* Check if user exists with this username. */
            Users checkUser = await userGymMovesRepository.getUser(user.username.Trim());

            /* Response for login. */
            UserLogInResponseModel response = new UserLogInResponseModel();

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
            This method gets the salt.
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
        public static int getInt(RNGCryptoServiceProvider random, int max)
        {
            byte[] byteChar = new byte[4];
            int value;

            do
            {
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
        private static string getHash(HashAlgorithm hashAlgorithm, string input)
        {

            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var passwordStored = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
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
        [Route("api/getcode")]
        [HttpPost]
        public async Task<ActionResult> getCode(GetCodeUserModel user)  {

            Users member = await userGymMovesRepository.getUser(user.username);

            if (member != null)  {
                MailboxAddress from = new MailboxAddress("Self",
                "lockdown.squad.301@gmail.com");

                MailboxAddress to = new MailboxAddress("Self",
               member.Email);

                MimeMessage message = new MimeMessage();
                message.From.Add(from);
                message.To.Add(to);
                message.Subject = "Gym Moves Code";


                string code = getRandomString(8);

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "The code for " + member.Name + " " + member.Surname +
                    " is " + code;

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("lockdown.squad.301@gmail.com", "");

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                client.Dispose();

                PasswordReset userToAdd = new PasswordReset();
                userToAdd.Username = user.username;
                userToAdd.User = member;
                userToAdd.Code = code;

                DateTime date = new DateTime();
                date.AddHours(1);
                userToAdd.Expiry = date;

                bool added = await resetPasswordRepository.addUser(userToAdd);


                return Ok();

            }

            return Unauthorized();
        }

        /*
        Method Name:
            forgotPassword
        Purpose:
            This method will change the password for the user and check they entered the correct code.
       */
        [Route("api/forgotpassword")]
        [HttpPost]
        public async Task<ActionResult<ForgotPasswordUserModel>> forgotPassword(ForgotPasswordUserModel user){

            /* Get the user record to change.*/
            Users userToChange = await userGymMovesRepository.getUser(user.username);

            UserForgotPasswordResponseModel response = new UserForgotPasswordResponseModel();

            /* No such user exists.*/
            if(userToChange == null) {
                response.message = "This username does not exist! Are you sure you typed the correct one?";
                return Unauthorized(response);
            }

            /*Get code from code table to see if it matches.*/
            PasswordReset userCode = await resetPasswordRepository.getUser(user.username);

            DateTime dateTime = new DateTime();

            if ((dateTime.Subtract(userCode.Expiry)).TotalMinutes > 60)
            {
                await resetPasswordRepository.deleteUser(userCode.Username);
                response.message = "Oh no! Your code is expired.";
                return Unauthorized(response);
            }

            if (userCode.Code.Equals(user.code))
            {
                 /* Hash new password.*/
                string hash = getHash(SHA256.Create(), user.password + userToChange.Salt);

                /* Try change password in db.*/
                bool changed = await userGymMovesRepository.changePassword(userToChange.Username, hash);

                if (changed) {
                    response.message = "Successfully changed! You can log in with your new password now.";

                    return Ok(response);
                }
                else {
                    response.message = "Sprry! Something went wrong on our side. Please try again.";

                return Ok(response);
                }

            }

            response.message = "This code is incorrect.";
            return Unauthorized(response);
        }


        /*
        Method Name:
            changePassword
        Purpose:
            This method will change the password for the user.
       */
        [Route("api/changepassword")]
        [HttpPost]
        public async Task<ActionResult> changePassword(ChangePasswordUserModel user) {

            /* Get the user record to change.*/
            Users userToChange = await userGymMovesRepository.getUser(user.username);

            /* If null, user does not exit.*/
            if (userToChange == null) {

                return Unauthorized();
            }

            /* Verifies the old password matches the one in the db. */
            if(verifyHash(SHA256.Create(), user.oldPassword + userToChange.Salt, userToChange.Password))  {
                
                /* Hash new password.*/
                string hash = getHash(SHA256.Create(), user.newPassword + userToChange.Salt);

                /* Try change password in db.*/
                bool changed = await userGymMovesRepository.changePassword(userToChange.Username, hash);

                if (changed) {
                    return Ok();
                }
                else {
                    return StatusCode(500);
                }
            }
            else {
                return Unauthorized();
            }    
        }
    }
}