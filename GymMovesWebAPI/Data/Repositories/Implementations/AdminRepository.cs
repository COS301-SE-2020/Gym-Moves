using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly MainDatabaseContext context;

        public AdminRepository(MainDatabaseContext context)
        {
            this.context = context;
        }

        public async Task<bool> addAdmin(SupportUsers user)
        {
            context.Add(user);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<SupportUsers> getAdmin(string username)
        {
            IQueryable<SupportUsers> query = context.SupportStaff;
            query = query.Where(p => p.Username == username);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> changePassword(string username, string password)
        {

            var user = context.SupportStaff.First(a => a.Username == username);

            user.Password = password;


            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
