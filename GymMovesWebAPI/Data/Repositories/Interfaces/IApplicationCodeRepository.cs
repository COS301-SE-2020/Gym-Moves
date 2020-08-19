using GymMovesWebAPI.Data.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IApplicationCodeRepository {
        public Task<bool> add(GymApplicationCodes application);
        public Task<GymApplicationCodes> getByCode(string code);
        public Task<GymApplicationCodes> getByName(string name, string branch);
        public Task<bool> remove(GymApplicationCodes application);
    }
}
