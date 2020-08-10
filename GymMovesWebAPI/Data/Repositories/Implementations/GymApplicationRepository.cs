using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class GymApplicationRepository : IGymApplicationRepository {
        private readonly MainDatabaseContext context;

        public GymApplicationRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addApplication(GymApplications application) {
            context.Add(application);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<GymApplications> getApplication(string gymName, string gymBranch) {
            IQueryable<GymApplications> query = context.GymApplications;
            query = query.Where(p => p.GymName == gymName)
                .Where(p => p.BranchName == gymBranch);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> removeApplication(GymApplications application) {
            context.Remove(application);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
