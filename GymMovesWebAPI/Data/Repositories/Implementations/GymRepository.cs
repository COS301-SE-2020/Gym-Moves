﻿using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
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

        public async Task<Gym> getGymById(int gymId) {
            IQueryable<Gym> query = context.Gyms;
            query = query.Where(p => p.GymId == gymId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Gym> getGymByNameAndBranch(string name, string branch)
        {
            IQueryable<Gym> query = context.Gyms;
            query = query.Where(p => p.GymBranch == branch);
            query = query.Where(p => p.GymName == name);

            return await query.FirstOrDefaultAsync();
        }
    }
}