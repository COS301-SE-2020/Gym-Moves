using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class LicenseKeysRepository : ILicenseKeysRepository {
        private readonly MainDatabaseContext context;

        public LicenseKeysRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addKey(LicenseKeys key) {
            context.Add(key);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<LicenseKeys> getKey(string key) {
            IQueryable<LicenseKeys> query = context.Licenses;
            query = query.Where(p => p.LicenseKey == key);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> removeKey(LicenseKeys key) {
            context.Remove(key);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
