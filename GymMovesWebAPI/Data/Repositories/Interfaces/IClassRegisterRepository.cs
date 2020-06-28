using GymMovesWebAPI.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IClassRegisterRepository {
        public Task<bool> addRegister(ClassRegister register);
        public Task<ClassRegister[]> getUserRegisters(string username);
        public Task<ClassRegister[]> getClassRegisters(int classId);
    }
}
