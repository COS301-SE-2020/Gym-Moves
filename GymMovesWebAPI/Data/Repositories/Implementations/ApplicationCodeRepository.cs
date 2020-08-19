using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class ApplicationCodeRepository : IApplicationCodeRepository {
        private readonly MainDatabaseContext context;

        public ApplicationCodeRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> add(GymApplicationCodes application) {
            context.Add(application);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<GymApplicationCodes> getByCode(string code) {
            IQueryable<GymApplicationCodes> query = context.ApplicationCodes;
            query = query.Where(p => p.Code == code);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<GymApplicationCodes> getByName(string name, string branch) {
            IQueryable<GymApplicationCodes> query = context.ApplicationCodes;
            query = query.Where(p => p.GymName == name);
            query = query.Where(p => p.BranchName == branch);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> remove(GymApplicationCodes application) {
            context.Remove(application);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
