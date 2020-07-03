/*
File Name:
    GymController.cs

Author:
    Longji

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
28/06/2020      Longji          Added addGym, getGymById and getByNameAndBranch functions definitions
02/07/2020      Longji          Added getAllGyms function definition


Functional Description:
    

List of Classes:
    - IGymRepository
*/

using GymMovesWebAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymRepository {
        public Task<bool> addGym(Gym gym);
        public Task<Gym[]> getAllGyms();
        public Task<Gym> getGymById(int gymId);
        public Task<Gym> getGymByNameAndBranch(String name, String branch);
    }
}
