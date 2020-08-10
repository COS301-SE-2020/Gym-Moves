using GymMovesWebAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface ISupportStaffRepository {
        public Task<bool> addStaff(SupportUsers user);
        public Task<SupportUsers> getStaff(string username);
    }
}
