/*
File Name:
   AdminController.cs

Author:
    Danel

Date Created:
    27/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    This file implements the controller that will handle all user manangement
    activities.

List of Classes:
    - AdminController

 */

using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.MailerProgram;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using GymMovesWebAPI.Models.DatabaseModels;

namespace GymMovesWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {

        private readonly IAdminRepository adminRepository;
        private readonly IPasswordResetRepository resetPasswordRepository;
        private readonly IMailer mailer;

        string emailReceiver = "u18008659@tuks.co.za";


        public AdminController(IAdminRepository repoOne, IPasswordResetRepository repoTwo, IMailer repoThree)
        {

            adminRepository = repoOne;
            resetPasswordRepository = repoTwo;
            mailer = repoThree;
        }

        [HttpPost("add")]
        public async Task<ActionResult> add(add user)
        {

            if (user.username == null)
            {
                return BadRequest("A username is needed for the person adding.");
            }

            SupportUsers adder = await adminRepository.getAdmin(user.username);

            if(adder == null)
            {
                return Unauthorized("The person trying to add is not a staff member!");
            }


            if (user.user.Username == null)
            {
                return BadRequest("A username is needed.");
            }

            if (user.user.Name == null)
            {
                return BadRequest("A name is needed.");
            }

            if (user.user.Surname == null)
            {
                return BadRequest("A surname is needed.");
            }

            if (user.user.Email == null)
            {
                return BadRequest("An email is needed.");
            }

            string password = getRandomString(10);

            user.user.Password = getHash(SHA256.Create(), password);

            SupportUsers exists = await adminRepository.getAdmin(user.user.Username);

            if(exists == null)
            {

                string content = "Your username is " + user.user.Username + " and your temporary password you will use to sign in for the first time is " + password + ".";
                await adminRepository.addAdmin(user.user);
                await mailer.sendEmail("lockdown.squad.301@gmail.com", "Gym Moves", "Admin Account", content, emailReceiver);

                return Ok();
            }
            else
            {
                return BadRequest("This username is already in use");
            }

        }


        [HttpPost("signup")]
        public async Task<ActionResult> signUp(AdminSignupRequest user)
        {

            SupportUsers newStaffAccount = new SupportUsers();

            if (user.username == null)
            {
                return BadRequest("A username is needed to make your account.");
            }

            if (user.tempPassword == null)
            {
                return BadRequest("A given password is needed to make your account.");
            }

            if (user.password == null)
            {
                return BadRequest("A new password is needed to create your account.");
            }

            SupportUsers checkUser = await adminRepository.getAdmin(user.username);

            if (checkUser == null)
            {
                return Unauthorized("Nobody with that username exists.");
            }

            if (verifyHash(SHA256.Create(), user.tempPassword, checkUser.Password))
            {
                if (user.tempPassword == user.password)
                {
                    return BadRequest("The password is the same as your given password!");
                }
                else
                {
                    await adminRepository.changePassword(user.username, getHash(SHA256.Create(), user.password));
                    return Ok();
                }
            }
            else
            {
                return Unauthorized("Incorrect given password");
            }

        }


        [HttpPost("login")]
        public async Task<ActionResult> logIn(LogInRequestModel user)
        {

            if (user.username == null)
            {
                return BadRequest("A username is required to login.");
            }

            if (user.password == null)
            {
                return BadRequest("A password is required to login.");
            }

            SupportUsers checkUser = await adminRepository.getAdmin(user.username.Trim());

            /* If null, no user with that username exists.*/
            if (checkUser == null)
            {
                return NotFound("Nobody with that username exists.");
            }

            /* Verify correct password has been entered.*/
            if (verifyHash(SHA256.Create(), user.password, checkUser.Password))
            {
                return Ok();
            }
            else
            {
                return Unauthorized("Password is incorrect.");
            }

        }


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


        private static bool verifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            var hashOfPassword = getHash(hashAlgorithm, input);
            return hashOfPassword.Equals(hash);
        }

    }
}

