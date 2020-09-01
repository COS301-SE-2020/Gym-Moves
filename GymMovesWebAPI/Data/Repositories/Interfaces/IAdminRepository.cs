using GymMovesWebAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces
{
    public interface IAdminRepository {
        public Task<bool> addAdmin(SupportUsers user);
        public Task<SupportUsers> getAdmin(string username);
        public Task<bool> changePassword(string username, string password);
    }
}
