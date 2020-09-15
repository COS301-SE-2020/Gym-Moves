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
using Microsoft.AspNetCore.Http;
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

        public GymController(IGymRepository gr, IGymMemberRepository gm, IUserRepository user, INotificationSettingsRepository n)
        {
            gymRepository = gr;
            gymMemberRepository = gm;
            userGymMovesRepository = user;
            notificationSettingRepository = n;
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

        [HttpPost("registeredMembers")]
        public async Task<ActionResult<GetMembersResponse[]>> getRegisteredMembers(GetMembersRequest request)
        {
            if (request.Username == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Username cannot be empty!");
            }

            Users user = await userGymMovesRepository.getUser(request.Username);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User does not exist!");
            }

            if (user.UserType != UserTypes.Manager)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User is not a manager!");
            }

            if (user.GymIdForeignKey != request.GymId)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Managers can only see the registered users of their own gym!");
            }

            Users[] registeredUsers = await userGymMovesRepository.getAllUsers(request.GymId);

            if (registeredUsers.Length > 0)
            {
                GetMembersResponse[] responses = UserMappers.UserToMemberResponse(registeredUsers);
                return Ok(responses);
            }
            else
            {
                return Ok(registeredUsers);
            }
        }

        [HttpPost("unregisteredMembers")]
        public async Task<ActionResult<GetMembersResponse>> getUnregisteredMembers(GetMembersRequest request)
        {

            if (request.Username == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Username cannot be empty!");
            }

            Users user = await userGymMovesRepository.getUser(request.Username);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User does not exist!");
            }

            if (user.UserType != UserTypes.Manager)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User is not a manager!");
            }

            if (user.GymIdForeignKey != request.GymId)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Managers can only see the unregistered users of their own gym!");
            }

            GymMember[] unregisteredUsers = await gymMemberRepository.getGymMembers(request.GymId);

            if (unregisteredUsers.Length > 0)
            {
                GetMembersResponse[] responses = UserMappers.MemberToMemberResponse(unregisteredUsers);
                return Ok(responses);
            }
            else
            {
                return Ok(unregisteredUsers);
            }
        }



        [HttpPost("AllMembers")]
        public async Task<ActionResult<GetMembersResponse>> getAllMembers(GetMembersRequest request)
        {

            if (request.Username == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Username cannot be empty!");
            }


            Users user = await userGymMovesRepository.getUser(request.Username);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User does not exist!");
            }

            if (user.UserType != UserTypes.Manager)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User is not a manager!");
            }

            if (user.GymIdForeignKey != request.GymId)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Managers can only see the unregistered users of their own gym!");
            }

            GymMember[] unregisteredUsers = await gymMemberRepository.getGymMembers(request.GymId);
            Users[] registeredUsers = await userGymMovesRepository.getAllUsers(request.GymId);

            if (unregisteredUsers.Length > 0 || registeredUsers.Length > 0)
            {
                GetMembersResponse[] responses = UserMappers.MemberToMemberResponse(unregisteredUsers);
                GetMembersResponse[] responses2 = UserMappers.UserToMemberResponse(registeredUsers);

                var all = new GetMembersResponse[responses.Length + responses2.Length];
                responses.CopyTo(all, 0);
                responses2.CopyTo(all, responses.Length);
                return Ok(all);
            }
            else
            {
                return Ok(unregisteredUsers);
            }
        }

    }
}
