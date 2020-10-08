using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class WeightDataRepositroy : IWeightDataRepository {
        private readonly MainDatabaseContext context;
        
        public WeightDataRepositroy(MainDatabaseContext context) {
            this.context = context;
        }
        
        public async Task<bool> addWeight(WeightData weight) {
            context.Add(weight);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<WeightData[]> getWeight(string username) {
            IQueryable<WeightData> query = context.WeightData;

            query = query.Where(p => p.Username == username);

            return await query.ToArrayAsync();
        }

        public async Task<WeightData> getWeightOnDay(string username, DateTime date) {
            IQueryable<WeightData> query = context.WeightData;

            query = query.Where(p => p.Username == username);
            query = query.Where(p => p.Date == date);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> updateWeight(WeightData weight) {
            context.Update(weight);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
