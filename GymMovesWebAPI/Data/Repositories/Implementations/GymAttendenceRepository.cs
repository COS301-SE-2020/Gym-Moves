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

        public async Task<bool> addAttendence(GymAttendanceRecord record) {
            context.Add(record);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<GymAttendanceRecord[]> GetAttendanceRecords(int gymId) {
            IQueryable<GymAttendanceRecord> query = context.GymAttendence;

            query = query.Where(p => p.GymId == gymId);

            return await query.ToArrayAsync();
        }

        public async Task<GymAttendanceRecord> getAttendenceRecord(int gymId, string time, int day, int month, int year) {
            IQueryable<GymAttendanceRecord> query = context.GymAttendence;

            query = query.Where(p => p.GymId == gymId);
            query = query.Where(p => p.Time == time);
            query = query.Where(p => p.Day == day);
            query = query.Where(p => p.Month == month);
            query = query.Where(p => p.Year == year);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> updateAttendence(GymAttendanceRecord record) {
            context.Update(record);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
