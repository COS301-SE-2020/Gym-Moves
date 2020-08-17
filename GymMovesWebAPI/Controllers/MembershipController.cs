/*
File Name:
    MembershipController.cs

Author:
    Longji

Date Created:
    04/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
04/08/2020    |  Longji        |  Add function to take csv file
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - MembershipController
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EllipticCurve.Utils;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase {
        private readonly IUserRepository userRepository;
        private readonly IGymMemberRepository memberRepository;

        public MembershipController(IUserRepository ur, IGymMemberRepository gmr) {
            userRepository = ur;
            memberRepository = gmr;
        }

        [HttpPost("addmember")]
        public async Task<ActionResult<AddMemberResponse>> addMembers(AddMemberRequests request) {
            Users user = await userRepository.getUser(request.Username);

            if (user == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Manager does not exist!");
            }

            if (user.UserType != UserTypes.Manager) {
                return StatusCode(StatusCodes.Status401Unauthorized, "Only managers can add members to the gym!");
            }

            string[] lines = request.Records.Split("\n");
            AddMemberResponse response = new AddMemberResponse();
            int gymId = user.GymIdForeignKey;

            response.Success = 0;
            response.Duplicates = 0;
            response.Failure = 0;

            if (!verifyCorrectFields(lines)) {
                return StatusCode(StatusCodes.Status400BadRequest, "CSV format is incorrect!");
            }

            for (int i = 0; i < lines.Length; i++) {
                /* Membership ID, Name, Surname, Email, PhoneNumber, User Type */
                string[] split = lines[i].Split(",");

                /* Check if member exists in users database */
                Users existingUser = await userRepository.getUserByMemberID(split[0].Trim(), gymId);
                if (existingUser != null) {
                    response.Duplicates++;
                    continue;
                }

                /* Check if member exists in verification database */
                GymMember existingMember = await memberRepository.getMember(split[0].Trim(), gymId);
                if (existingMember != null) {
                    response.Duplicates++;
                    continue;
                }

                GymMember newMember = new GymMember();

                newMember.MembershipId = split[0].Trim();
                newMember.GymId = gymId;
                newMember.Name = split[1].Trim();
                newMember.Surname = split[2].Trim();
                newMember.Email = split[3].Trim();
                newMember.PhoneNumber = split[4].Trim();

                int userType = int.Parse(split[5].Trim());

                switch (userType) {
                    case 0:
                        newMember.UserType = UserTypes.Member;
                        break;
                    case 1:
                        newMember.UserType = UserTypes.Instructor;
                        break;
                    case 2:
                        newMember.UserType = UserTypes.Manager;
                        break;
                }

                if (await memberRepository.addMember(newMember)) {
                    response.Success++;
                } else {
                    response.Failure++;
                }
            }

            return Ok(response);
        }

        private bool verifyCorrectFields(string[] lines) {
            for (int i = 0; i < lines.Length; i++) {
                if (lines[0].Split(",").Length != 6) {
                    return false;
                }
            }

            return true;
        }
    }
}
