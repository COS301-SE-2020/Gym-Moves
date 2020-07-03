using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IUserRepository {
        public Task<bool> addUser(Users user);
        public Task<Users> getUser(string username);
    }
}
