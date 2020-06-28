using GymMovesWebAPI.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IClassRepository {
        public Task<bool> addClass(GymClasses gymClass);
        public Task<GymClasses> getClassById(int classId);
        public Task<GymClasses[]> getGymClasses(int gymId);
        public Task<GymClasses[]> getUserClasses(string username);
        public Task<GymClasses[]> getInstructorClasses(string username);
    }
}
