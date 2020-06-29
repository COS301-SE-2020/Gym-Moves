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


Functional Description:
    This file implements the controller that will handle all user manangement
    activities.

List of Classes:
    - UserController

 */

using GymMovesWebAPI.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.ArgumentModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;

using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using GymMovesWebAPI.Data.Models.DatabaseModels;

namespace GymMovesWebAPI.Controllers{


    /*
    Class Name:
        UserController

    Purpose:
        This class handles the different requests that can be sent
        to the API with regards to the user management.
    */
    [ApiController]
    public class UserController : Controller{

        private readonly IUserRepository userGymMovesRepository;
        private readonly IGymMemberRepository userRepository;
        private readonly IGymRepository gymRepository;
        private readonly INotificationSettingsRepository notificationSettingRepository;

        /*
        Method Name:
            UserController
        Purpose:
            This method instantiates the repositories that will be used.
        */
        public UserController(IUserRepository repoOne, IGymMemberRepository repoTwo, IGymRepository repoThree,
            INotificationSettingsRepository repoFour){
            
            userGymMovesRepository = repoOne;
            userRepository = repoTwo;
            gymRepository = repoThree;
            notificationSettingRepository = repoFour;
        }

        /*
        Method Name:
            signUp
        Purpose:
            This method handles a user sign up request.
       */
        [Route("api/signup")]
        [HttpPost]
        public async Task<ActionResult<UserSignUpResponseModel>> signUp(SignInUserModel user){

            /* Create return message to send back.*/
            UserSignUpResponseModel returnMessage = new UserSignUpResponseModel();

            /* New user account being made.*/
            Users newUserAccount = new Users();

            /* Set the new accounts member ID and username. */
            newUserAccount.MembershipId = user.gymMemberId.Trim();
            newUserAccount.Username = user.username.Trim();

            string[] gymArray = user.gym.Split(",");

            if(gymArray.Length == 2)
            {

                /* Set the users gym and gym ID. */
                newUserAccount.Gym = await gymRepository.getGymByNameAndBranch(gymArray[0].Trim(), gymArray[1].Trim());
                newUserAccount.GymIdForeignKey = newUserAccount.Gym.GymId;

                /* Get the member from the user repo that has the entered member ID and gym. */
                GymMember member = await userRepository.getMember(newUserAccount.MembershipId, newUserAccount.GymIdForeignKey);

                /* If member null, such person does not exist. */
                if (member == null)
                {
                    returnMessage.usernameValid = true;
                    returnMessage.gymMemberIdValid = false;
                    returnMessage.userType = 0;

                    return Unauthorized(returnMessage);
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
                if (checkIfUsernameExists != null)
                {
                    returnMessage.usernameValid = false;
                    returnMessage.gymMemberIdValid = true;
                    returnMessage.userType = 0;

                    return Unauthorized(returnMessage);
                }

                /* random will generate different salt lengths of between 5 and 10. */
                Random random = new Random();
                newUserAccount.Salt = getRandomString(random.Next(5, 10));

                string hash = getHash(SHA256.Create(), user.password + newUserAccount.Salt);

                /* Set new accounts password as the hash. */
                newUserAccount.Password = hash;

                /* Check if new account added. */
                bool added = await userGymMovesRepository.addUser(newUserAccount);

                if (added)
                {

                    NotificationSettings newUserNotifs = new NotificationSettings();

                    newUserNotifs.Email = false;
                    newUserNotifs.PushNotifications = true;
                    newUserNotifs.Sms = false;
                    newUserNotifs.UsernameForeignKey = newUserAccount.Username;
                    newUserNotifs.User = newUserAccount;

                    added = await notificationSettingRepository.addUser(newUserNotifs);

                    returnMessage.usernameValid = true;
                    returnMessage.gymMemberIdValid = true;
                    returnMessage.userType = newUserAccount.UserType;

                    return Ok(returnMessage);
                }

            }

            returnMessage.usernameValid = false;
            returnMessage.gymMemberIdValid = false;
            returnMessage.userType = 0;

            return Unauthorized(returnMessage);
            
        }

        /*
        Method Name:
            logIn
        Purpose:
            This method handles a user login request.
       */
        [Route("api/login")]
        [HttpPost]
        public async Task<ActionResult<UserLogInResponseModel>> logIn(LogInUserModel user){

            /* Check if user exists with this username. */
            Users checkUser = await userGymMovesRepository.getUser(user.username.Trim());

            /* Response for login. */
            UserLogInResponseModel response = new UserLogInResponseModel();

            /* If null, no user with that username exists.*/
            if (checkUser == null){
                
                response.usernameValid = false;
                response.passwordValid = true;
                response.userType = 0;

                return Unauthorized(response);
            }

            /* Verify correct password has been entered.*/
            if (verifyHash(SHA256.Create(), user.password + checkUser.Salt, checkUser.Password)){
                
                response.usernameValid = true;
                response.passwordValid = true;
                response.userType = checkUser.UserType;

                return Ok(response);
            }
            else{
                response.usernameValid = true;
                response.passwordValid = false;
                response.userType = 0;

                return Unauthorized(response);
            }

        }

        /*
        Method Name:
            getRandomString
        Purpose:
            This method gets the salt.
       */
        public static string getRandomString(int length){

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder salt = new StringBuilder();

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            while (length > 0){
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
        public static int getInt(RNGCryptoServiceProvider random, int max){
            byte[] byteChar = new byte[4];
            int value;

            do{
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
        private static string getHash(HashAlgorithm hashAlgorithm, string input){

            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var passwordStored = new StringBuilder();

            for (int i = 0; i < data.Length; i++){
                passwordStored.Append(data[i].ToString("x2"));
            }

            return passwordStored.ToString();
        }

        /*
        Method Name:
            verifyHash
        Purpose:
            This method will verify that the hash matches the hash stored in the datbase.
       */
        private static bool verifyHash(HashAlgorithm hashAlgorithm, string input, string hash){
            
            var hashOfPassword = getHash(hashAlgorithm, input);
            return hashOfPassword.Equals(hash);
        }
    }
}