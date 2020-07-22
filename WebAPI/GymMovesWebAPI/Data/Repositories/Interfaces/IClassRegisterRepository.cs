using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IClassRegisterRepository {
        public Task<bool> addRegister(ClassRegister register);
        public Task<bool> removeRegister(ClassRegister register);
        public Task<bool> removeRegisters(ClassRegister[] registers);
        public Task<ClassRegister[]> getUserRegisters(string username, bool includeClass = false);
        public Task<ClassRegister[]> getClassRegisters(int classId);
        public Task<ClassRegister> getSpecificUserAndClass(string username, int classId);
    }
}
