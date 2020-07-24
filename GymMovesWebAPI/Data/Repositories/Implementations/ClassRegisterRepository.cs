using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Models.DatabaseModels;
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

            IQueryable<GymClasses> query = context.Classes;
            query = query.Where(p => p.ClassId == register.ClassIdForeignKey);

            GymClasses classToUpdate = query.FirstOrDefault();
            classToUpdate.CurrentStudents++;

            context.Update(classToUpdate);

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> removeRegister(ClassRegister register) {
            context.Remove(register);

            IQueryable<GymClasses> query = context.Classes;
            query = query.Where(p => p.ClassId == register.ClassIdForeignKey);

            GymClasses classToUpdate = query.FirstOrDefault();
            classToUpdate.CurrentStudents--;

            context.Update(classToUpdate);

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> removeRegisters(ClassRegister[] registers) {
            if (registers.Length == 0) {
                return true;
            }

            context.Remove(registers);

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<ClassRegister[]> getClassRegisters(int classId) {
            IQueryable<ClassRegister> query = context.ClassRegisters;
            query = query.Where(p => p.ClassIdForeignKey == classId);

            return await query.ToArrayAsync();
        }

        public async Task<ClassRegister[]> getUserRegisters(string username, bool includeClass = false) {
            IQueryable<ClassRegister> query = context.ClassRegisters;
            query = query.Where(p => p.StudentUsernameForeignKey == username);

            if (includeClass) {
                query = query.Include(p => p.Class);
            }

            return await query.ToArrayAsync();
        }

        public async Task<ClassRegister> getSpecificUserAndClass(string username, int classId) {
            IQueryable<ClassRegister> query = context.ClassRegisters;
            query = query.Where(p => p.StudentUsernameForeignKey == username);
            query = query.Where(p => p.ClassIdForeignKey == classId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
