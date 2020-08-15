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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Mappers {
    public static class UserMappers {
        public static getMembersResponse[] UserToMemberResponse(Users[] models) {
            getMembersResponse[] responses = new getMembersResponse[models.Length];
            
            for (int i = 0; i < models.Length; i++) {
                responses[i] = new getMembersResponse();

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
    }
}
