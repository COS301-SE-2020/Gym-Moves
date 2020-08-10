using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class SupportStaffRepository : ISupportStaffRepository {
        private readonly MainDatabaseContext context;

        public SupportStaffRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addStaff(SupportUsers user) {
            context.Add(user);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<SupportUsers> getStaff(string username) {
            IQueryable<SupportUsers> query = context.SupportStaff;
            query = query.Where(p => p.Username == username);

            return await query.FirstOrDefaultAsync();
        }
    }
}
