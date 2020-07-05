﻿/*
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
04/07/2020    | Danel          | Added working email send
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
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using MailKit.Security;

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
        public async Task<ActionResult<SignUpResponseModel>> signUp(SignUpRequestModel user) {

            /* Create return message to send back.*/
            SignUpResponseModel response = new SignUpResponseModel();

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
        [Route("api/login")]
        [HttpPost]
        public async Task<ActionResult<LogInResponseModel>> logIn(LogInRequestModel user) {

            /* Response for login. */
            LogInResponseModel response = new LogInResponseModel();

            /* Check if user exists with this username. */
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
        [Route("api/getcode")]
        [HttpPost]
        public async Task<ActionResult> getCode(GetCodeRequestModel user)  {

            /* Check if user already has a code.*/
            PasswordReset checkIfHasCode = await resetPasswordRepository.getUser(user.username);

            if (checkIfHasCode != null) {
               bool deleted =  await resetPasswordRepository.deleteUser(checkIfHasCode);

                if (!deleted) {
                    return StatusCode(500);
                }
            }


            /*Get user with is username.*/
            Users member = await userGymMovesRepository.getUser(user.username);

            if (member != null)  {

                var certificate = new X509Certificate2("", "", X509KeyStorageFlags.Exportable);
                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer
                    ("lockdownsquad@skilled-nation-282411.iam.gserviceaccount.com")
                { Scopes = new[] { "https://mail.google.com/" }, User = "lockdown.squad.301@gmail.com" }.FromCertificate(certificate));

                bool result = await credential.RequestAccessTokenAsync(new CancellationToken());


                MailboxAddress from = new MailboxAddress("Gym Moves", "lockdown.squad.301@gmail.com");
                MailboxAddress to = new MailboxAddress("User", "u18008659@tuks.co.za");
                // MailboxAddress to = new MailboxAddress("User", user.Email);

                MimeMessage message = new MimeMessage();
                message.From.Add(from);
                message.To.Add(to);
                message.Subject = "Gym Moves Code";

                DateTime date = DateTime.Now;
                date = date.AddHours(2);

                string code = getRandomString(8);
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "The code for " + member.Name + " " + member.Surname +
                    " is " + code + ". This is only valid until " + date.ToLocalTime() + ".";

                message.Body = bodyBuilder.ToMessageBody();


                PasswordReset userToAdd = new PasswordReset();
                userToAdd.Username = user.username;
                userToAdd.User = member;
                userToAdd.Code = code;
                userToAdd.Expiry = date;

                bool added = await resetPasswordRepository.addUser(userToAdd);

                if (added) {

                    SmtpClient client = new SmtpClient();
                    var connection = new SaslMechanismOAuth2("lockdown.squad.301@gmail.com", credential.Token.AccessToken);
                    client.Authenticate(connection);
                    client.Send(message);
                    client.DisconnectAsync(true);
                    client.Dispose();

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

        /*
        Method Name:
            forgotPassword
        Purpose:
            This method will change the password for the user and check they entered the correct code.
       */
        [Route("api/forgotpassword")]
        [HttpPost]
        public async Task<ActionResult<ForgotPasswordResponseModel>> forgotPassword(ForgotPasswordRequestModel user){

            ForgotPasswordResponseModel response = new ForgotPasswordResponseModel();

            /* Get the user record to change.*/
            Users userToChange = await userGymMovesRepository.getUser(user.username);
            
            /* No such user exists.*/
            if (userToChange == null) {
                response.message = "This username does not exist! Are you sure you typed the correct one?";
                return Unauthorized(response);
            }


            /*Get code from code table to see if it matches.*/
            PasswordReset userInCodeTabe = await resetPasswordRepository.getUser(user.username);

            /* No such user exists.*/
            if (userInCodeTabe == null) {
                response.message = "This user does not have a code! Are you sure you received one?";
                return Unauthorized(response);
            }


            DateTime dateTime = DateTime.Now;

            if ((dateTime.Subtract(userInCodeTabe.Expiry)).TotalMinutes > 60) {
                await resetPasswordRepository.deleteUser(await resetPasswordRepository.getUser(user.username));
                response.message = "Oh no! Your code is expired.";
                return Unauthorized(response);
            }

            if (userInCodeTabe.Code.Equals((user.code).Trim())) {
                 /* Hash new password.*/
                string hash = getHash(SHA256.Create(), user.password + userToChange.Salt);

                /* Try change password in db.*/
                bool changed = await userGymMovesRepository.changePassword(userToChange.Username, hash);

                if (changed) {
                    response.message = "Successfully changed! You can log in with your new password now.";
                    await resetPasswordRepository.deleteUser(userInCodeTabe);
                    return Ok(response);
                }
                else {
                    response.message = "Something went wrong on our side. Please try again.";
                    await resetPasswordRepository.deleteUser(userInCodeTabe);
                    return StatusCode(500, response);
                }

            }
            else {
                response.message = "This code is incorrect.";
                return Unauthorized(response);
            }
        }


        /*
        Method Name:
            changePassword
        Purpose:
            This method will change the password for the user.
       */
        [Route("api/changepassword")]
        [HttpPost]
        public async Task<ActionResult<ChangePasswordResponseModel>> changePassword(ChangePasswordRequestModel user) {

            ChangePasswordResponseModel response = new ChangePasswordResponseModel();

            /* Get the user record to change.*/
            Users userToChange = await userGymMovesRepository.getUser(user.username);

            /* If null, user does not exit.*/
            if (userToChange == null) {
                response.message = "This user doesn't exist.";
                return Unauthorized(response);
            }

            /* Verifies the old password matches the one in the db. */
            if(verifyHash(SHA256.Create(), user.oldPassword + userToChange.Salt, userToChange.Password))  {
                
                /* Hash new password.*/
                string hash = getHash(SHA256.Create(), user.newPassword + userToChange.Salt);

                /* Try change password in db.*/
                bool changed = await userGymMovesRepository.changePassword(userToChange.Username, hash);

                if (changed) {
                    response.message = "Successully changed your password.";
                    return Ok(response);
                }
                else {
                    response.message = "Unable to change your password now, please try again later.";
                    return StatusCode(500, response);
                }
            }
            else {
                response.message = "The password you entered is not correct.";
                return Unauthorized(response);
            }    
        }
    }
}