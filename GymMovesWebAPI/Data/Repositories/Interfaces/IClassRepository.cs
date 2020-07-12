using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IClassRepository {
        public Task<bool> addClass(GymClasses gymClass);
        public Task<GymClasses> getClassById(int classId);
        public Task<GymClasses[]> getGymClasses(int gymId);
        public Task<GymClasses[]> getUserClasses(string username);
        public Task<GymClasses[]> getInstructorClasses(string username);
        public Task<GymClasses> getInstructorClassAtSpecificDateTime(string instructor, string day, string time);
        public Task<bool> instructorCancelClass(int classId);
        public Task<bool> managerDeleteClass(GymClasses classToDelete);
    }
}
