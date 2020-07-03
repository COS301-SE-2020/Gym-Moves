/*
File Name:
    UserRepository.cs

Author:
    Longji

Date Created:
    26/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    | Danel          | Added chnage password functionality and added
              |                | an implementation.
--------------------------------------------------------------------------------


Functional Description:
    This file implements the controller that will handle all user manangement
    activities.

List of Classes:
    - UserController

 */

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
        public async Task<bool> changePassword(string username, string password) {

            var user = context.Users.First(a => a.Username == username);

            user.Password = password;


            return (await context.SaveChangesAsync()) > 0;
        }
    }

    /*
    Class Name:
        PasswordResetRepository

    Purpose:
        This class implements the password rest
        repo.
    */
    public class PasswordResetRepository : IPasswordResetRepository {
        
        private readonly MainDatabaseContext context;

        /*
        Method Name:
            changePassword
        Purpose:
            This changes the password of the user.
        */
        public PasswordResetRepository(MainDatabaseContext context) {
           
            this.context = context;
        }
        /*
        Method Name:
            addUser
        Purpose:
            This adds a user to the reset password table.
        */
        public async Task<bool> addUser(PasswordReset user) {
           
            context.Add(user);
            return (await context.SaveChangesAsync()) > 0;
        }

        /*
        Method Name:
            getUser
        Purpose:
            This gets a user from the reset password table.
        */
        public async Task<PasswordReset> getUser(string username) {
           
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
        public async Task<bool> deleteUser(string username) {
            
            context.Remove(getUser(username));
            return (await context.SaveChangesAsync()) > 0;
        }


    }
}
