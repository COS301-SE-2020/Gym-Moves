﻿/*
File Name:
    IUserRepository.cs

Author:
    Longji

Date Created:
    26/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    | Danel          | Added more functions and new interface
--------------------------------------------------------------------------------


Functional Description:
    This file implements the interface for user related repositories.

List of Classes:
    - IUserRepository
    - IPasswordResetRepository

 */

using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IUserRepository {
        public Task<bool> addUser(Users user);
        public Task<Users> getUser(string username);
        public Task<Users> getUserByMemberID(string memberID, int gymID);
        public Task<bool> changePassword(string username, string password);
    }
}
