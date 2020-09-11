using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class GymAttendenceRepository : IGymAttendenceRepository {
        private readonly MainDatabaseContext context;

        public GymAttendenceRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addAttendence(GymAttendenceRecord record) {
            context.Add(record);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<GymAttendenceRecord> getAttendenceRecord(int gymId, string time, string day, string month, string year) {
            IQueryable<GymAttendenceRecord> query = context.GymAttendence;

            query = query.Where(p => p.GymId == gymId);
            query = query.Where(p => p.Time == time);
            query = query.Where(p => p.Day == day);
            query = query.Where(p => p.Month == month);
            query = query.Where(p => p.Year == year);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> updateAttendence(GymAttendenceRecord record) {
            context.Update(record);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
