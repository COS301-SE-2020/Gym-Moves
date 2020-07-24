using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using Microsoft.EntityFrameworkCore;



namespace GymMovesWebAPI.Data.Repositories.Implementations
{
    /*
   Class Name:
       PasswordResetRepository

   Purpose:
       This class implements the password rest
       repo.
   */
    public class PasswordResetRepository : IPasswordResetRepository
    {

        private readonly MainDatabaseContext context;

        /*
        Method Name:
            changePassword
        Purpose:
            This changes the password of the user.
        */
        public PasswordResetRepository(MainDatabaseContext context)
        {

            this.context = context;
        }
        /*
        Method Name:
            addUser
        Purpose:
            This adds a user to the reset password table.
        */
        public async Task<bool> addUser(PasswordReset user)
        {

            context.Add(user);
            return (await context.SaveChangesAsync()) > 0;
        }

        /*
        Method Name:
            getUser
        Purpose:
            This gets a user from the reset password table.
        */
        public async Task<PasswordReset> getUser(string username)
        {

            IQueryable<PasswordReset> query = context.PasswordResets;
            query = query.Where(p => p.Username == username);

            return await query.FirstOrDefaultAsync();
        }

        /*
        Method Name:
            deleteUser
        Purpose:
            This removes a user, as their password changed.
        */
        public async Task<bool> deleteUser(PasswordReset user)
        {
            context.Remove(user);
            return (await context.SaveChangesAsync()) > 0;
        }

    }
}
