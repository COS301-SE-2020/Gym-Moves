using GymMovesWebAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface ILicenseKeysRepository {
        public Task<bool> addKey(LicenseKeys key);
        public Task<bool> removeKey(LicenseKeys key);
        public Task<LicenseKeys> getKey(string key);
    }
}
