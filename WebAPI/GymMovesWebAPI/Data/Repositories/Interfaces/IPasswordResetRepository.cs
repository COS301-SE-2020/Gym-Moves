using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.Data.Models.DatabaseModels;

namespace GymMovesWebAPI.Data.Repositories.Interfaces
{
    /*
   Interface Name:
       IPasswordResetRepository

   Purpose:
       This class implements the interface of the password rest
       repo.
   */
    public interface IPasswordResetRepository
    {
        public Task<bool> addUser(PasswordReset user);
        public Task<PasswordReset> getUser(string username);
        public Task<bool> deleteUser(PasswordReset user);
    }
}
