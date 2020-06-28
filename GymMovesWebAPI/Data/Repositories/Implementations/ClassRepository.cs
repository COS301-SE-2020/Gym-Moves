using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class ClassRepository : IClassRepository {
        private readonly MainDatabaseContext context;
        private readonly IClassRegisterRepository registerRepository;

        public ClassRepository(MainDatabaseContext context, IClassRegisterRepository registerRepository) {
            this.context = context;
            this.registerRepository = registerRepository;
        }

        public async Task<bool> addClass(GymClasses gymClass) {
            context.Add(gymClass);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<GymClasses> getClassById(int classId) {
            IQueryable<GymClasses> query = context.Classes;
            query = query.Where(p => p.ClassId == classId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<GymClasses[]> getGymClasses(int gymId) {
            IQueryable<GymClasses> query = context.Classes;
            query = query.Where(p => p.GymIdForeignKey == gymId);

            return await query.ToArrayAsync();
        }

        public async Task<GymClasses[]> getInstructorClasses(string username) {
            IQueryable<GymClasses> query = context.Classes;
            query = query.Where(p => p.InstructorUsername == username);

            return await query.ToArrayAsync();
        }

        /* Possible simplification of code by moving registerQuery to RegisterRepository */
        public async Task<GymClasses[]> getUserClasses(string username) {
            ClassRegister[] registerList = await registerRepository.getUserRegisters(username);

            if (registerList.Length == 0) {
                return new GymClasses[0];
            }
            
            GymClasses[] classList = new GymClasses[registerList.Length];

            for (int i = 0; i < registerList.Length; i++) {
                classList[i] = await getClassById(registerList[i].ClassIdForeignKey);
            }

            return classList;
        }
    }
}
