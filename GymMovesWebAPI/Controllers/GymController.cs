/*
File Name:
    GymController.cs

Author:
    Longji

Date Created:
    03/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    |  Longji        |  Create return all gyms function
--------------------------------------------------------------------------------
28/07/2020    |  Raeesa        |  Create function for any gym to register to app
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - GymController
*/

using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Mappers;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.GymModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using GymMovesWebAPI.Data.Repositories.Implementations;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymRepository gymRepository;
        private readonly IGymMemberRepository gymMemberRepository;
        private readonly IUserRepository userGymMovesRepository;
        private readonly INotificationSettingsRepository notificationSettingRepository;
        private readonly ILicenseKeysRepository license;
        public GymController(IGymRepository gr, IGymMemberRepository gm, IUserRepository user, INotificationSettingsRepository n, ILicenseKeysRepository l)
        {
            gymRepository = gr;
            gymMemberRepository = gm;
            userGymMovesRepository = user;
            notificationSettingRepository = n;
            license = l;
        }

        /*
        Method Name:
            listAllGyms
        Purpose:
            This function returns all the gyms that are registered in the gym
         */
        [HttpGet("getall")]
        public async Task<ActionResult<GymModel>> listAllGyms()
        {
            GymModel[] results = GymMapper.mapToGymModel(await gymRepository.getAllGyms());
            return Ok(results);
        }
        /*
         Method Name:
            registerGym
         Purpose:
             This method will verify that the gym is a valid gym and that the licence code is correct.
             It will then register the gym is everything matches.
        */
        [HttpPost("register")]
        public async Task<ActionResult<GymModel>> registerGym(GymSignUpRequestModel newGym)
        {
            Gym newgym = new Gym();
            newgym.GymName = newGym.gymName.Trim();
            newgym.GymBranch = newGym.gymBranch.Trim();

            //validate code 
            LicenseKeys licenseKeys = await license.getKey(newGym.code);
            if (licenseKeys == null)
            {
                return Unauthorized("This code is invalid.");
            }

            Gym checkifgymexists = await gymRepository.getGymByNameAndBranch(newgym.GymName, newgym.GymBranch);
            if (checkifgymexists != null)
            {
                return Unauthorized("This gym is already registered.");
            }

            Users checkIfUsernameExists = await userGymMovesRepository.getUser(newGym.username);

            if (checkIfUsernameExists != null)
            {
                return Unauthorized("This username is already in use.");
            }


            if (licenseKeys.LicenseKey != newGym.code || licenseKeys.Email != newGym.email)
            {
                return Unauthorized("This is not the code registered to your email.");
            }

            else
            {
                bool creategym = await gymRepository.addGym(newgym);
                Gym gymsid = await gymRepository.getGymByNameAndBranch(newgym.GymName.Trim(), newgym.GymBranch.Trim());


                    GymMember checkifmemberexists = await gymMemberRepository.getMember(newGym.memberid, gymsid.GymId);
                    if (checkifmemberexists != null)
                    {
                        return Unauthorized("This gym member is already registered.");
                    }

                    GymMember newmember = new GymMember();
                    newmember.MembershipId = newGym.memberid.Trim();
                    newmember.Name = newGym.name.Trim();
                    newmember.Surname = newGym.surname.Trim();
                    newmember.Email = newGym.email.Trim();
                    newmember.PhoneNumber = newGym.number.Trim();
                    newmember.UserType = UserTypes.Manager;
                    newmember.GymId = gymsid.GymId;
                  
                    bool member = await gymMemberRepository.addMember(newmember);

                    Users newUserAccount = new Users();
                    newUserAccount.MembershipId = newGym.memberid.Trim();
                    newUserAccount.Username = newGym.username.Trim();
                    newUserAccount.Gym = await gymRepository.getGymByNameAndBranch(newGym.gymName.Trim(), newGym.gymBranch.Trim());
                    newUserAccount.GymIdForeignKey = newUserAccount.Gym.GymId;

                    GymMember Member = await gymMemberRepository.getMember(newmember.MembershipId,
                        newUserAccount.GymIdForeignKey);


                    if (Member == null)
                    {
                        return Unauthorized("This gym member ID does not exist.");
                    }

                    Users checkIfUserHasAccount = await userGymMovesRepository.getUserByMemberID(Member.MembershipId,
                        Member.GymId);

                    if (checkIfUserHasAccount != null)
                    {
                        return BadRequest("There is already an account for this gym member.");
                    }

                    newUserAccount.Name = Member.Name;
                    newUserAccount.Surname = Member.Surname;
                    newUserAccount.PhoneNumber = Member.PhoneNumber;
                    newUserAccount.Email = Member.Email;

                    newUserAccount.UserType = Member.UserType;

                   

                    Random random = new Random();
                    newUserAccount.Salt = getRandomString(random.Next(5, 10));
                    string hash = getHash(SHA256.Create(), newGym.password + newUserAccount.Salt);
                    newUserAccount.Password = hash;

                    bool added = await userGymMovesRepository.addUser(newUserAccount);

                    if (added)
                    {

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

                        return Ok(response);
                    }
                    else
                    {
                        return StatusCode(500, "Unable to create this account right now.");

                    }

               
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
    }
}
