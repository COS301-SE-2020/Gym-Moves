using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class UserRepository : IUserRepository {
        private readonly MainDatabaseContext context;

        public UserRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addUser(Users user) {
            context.Add(user);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<Users> getUser(string username) {
            IQueryable<Users> query = context.Users;
            query = query.Where(p => p.Username == username);

            return await query.FirstOrDefaultAsync();
        }

        /*
        Method Name:
            changePassword
        Purpose:
            This changes the password of the user.
        */
        public async Task<bool> changePassword(string username, string password)
        {

            var user = new Users { Username = username };

            user.Password = password;

            context.Entry(user).Property("Password").IsModified = true;

            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
