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

        public async Task<Users> getUserByMemberID(string memberID, int gymID){
            
            IQueryable<Users> query = context.Users;
            query = query.Where(p => p.MembershipId == memberID);

            if (query != null) { 
                query = query.Where(p => p.GymIdForeignKey == gymID);
                return await query.FirstOrDefaultAsync();
            }

            return null;
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
}
