﻿/*
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
28/06/2020      Longji          Added addGym, getGymById and getByNameAndBranch functions implementations
02/07/2020      Longji          Added getAllGyms function implementation

Functional Description:
    

List of Classes:
    - GymRepository
*/

using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class GymRepository : IGymRepository {
        private readonly MainDatabaseContext context;

        public GymRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addGym(Gym gym) {
            context.Add(gym);
            return (await context.SaveChangesAsync()) > 0; 
        }

        public async Task<Gym[]> getAllGyms() {
            return await context.Gyms.ToArrayAsync();            
        }

        public async Task<Gym> getGymById(int gymId) {
            IQueryable<Gym> query = context.Gyms;
            query = query.Where(p => p.GymId == gymId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Gym> getGymByNameAndBranch(string name, string branch)
        {
            IQueryable<Gym> query = context.Gyms;
            query = query.Where(p => p.GymBranch == branch);

            if(query != null)
                query = query.Where(p => p.GymName == name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Users[]> getMembers(int gymID)
        {
            IQueryable<Users> query = context.Users;
            query = query.Where(p => p.GymIdForeignKey == gymID);
            if (query != null)
                query = query.Where(p => p.UserType == UserTypes.Member);

            return await query.ToArrayAsync();
        }

    }
}
