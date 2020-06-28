using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class ClassRegisterRepository : IClassRegisterRepository {
        private readonly MainDatabaseContext context;

        public ClassRegisterRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addRegister(ClassRegister register) {
            context.Add(register);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<ClassRegister[]> getClassRegisters(int classId) {
            IQueryable<ClassRegister> query = context.ClassRegisters;
            query = query.Where(p => p.ClassIdForeignKey == classId);

            return await query.ToArrayAsync();
        }

        public async Task<ClassRegister[]> getUserRegisters(string username) {
            IQueryable<ClassRegister> query = context.ClassRegisters;
            query = query.Where(p => p.StudentUsernameForeignKey == username);

            return await query.ToArrayAsync();
        }
    }
}
