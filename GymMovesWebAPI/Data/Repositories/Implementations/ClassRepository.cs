using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;


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

        /* TODO: Check if given time is in the start end range */
        public async Task<GymClasses> getInstructorClassAtSpecificDateTime(string instructor, string day, string time) {
            IQueryable<GymClasses> query = context.Classes;
            query = query.Where(p => p.InstructorUsername == instructor);
            query = query.Where(p => p.Day == day);
            query = query.Where(p => p.StartTime == time);

            return await query.FirstOrDefaultAsync();
        }


        public async Task<bool> instructorCancelClass(int classId) {
            var classToChange = context.Classes.First(a => a.ClassId == classId);
            classToChange.Cancelled = !classToChange.Cancelled;

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> managerDeleteClass(GymClasses classToDelete) {
            context.Remove(classToDelete);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
