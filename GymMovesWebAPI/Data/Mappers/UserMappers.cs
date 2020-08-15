/*
File Name:
    UserMappers.cs

Author:
    Longji

Date Created:
    15/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    This file implements a mapper for gym classes.

List of Classes:
    - UserMappers

 */


using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Mappers {
    public static class UserMappers {
        public static GetMembersResponse[] UserToMemberResponse(Users[] models) {
            GetMembersResponse[] responses = new GetMembersResponse[models.Length];
            
            for (int i = 0; i < models.Length; i++) {
                responses[i] = new GetMembersResponse();

                responses[i].Username = models[i].Username;
                responses[i].Name = models[i].Name;
                responses[i].Surname = models[i].Surname;
                responses[i].MembershipId = models[i].MembershipId;
                responses[i].Email = models[i].Email;
                responses[i].PhoneNumber = models[i].PhoneNumber;
                responses[i].UserType = models[i].UserType;
            }

            return responses;
        }

        public static GetMembersResponse[] MemberToMemberResponse(GymMember[] models) {
            GetMembersResponse[] responses = new GetMembersResponse[models.Length];

            for (int i = 0; i < models.Length; i++) {
                responses[i] = new GetMembersResponse();

                responses[i].Username = null;
                responses[i].Name = models[i].Name;
                responses[i].Surname = models[i].Surname;
                responses[i].MembershipId = models[i].MembershipId;
                responses[i].Email = models[i].Email;
                responses[i].PhoneNumber = models[i].PhoneNumber;
                responses[i].UserType = models[i].UserType;
            }

            return responses;
        }
    }
}
