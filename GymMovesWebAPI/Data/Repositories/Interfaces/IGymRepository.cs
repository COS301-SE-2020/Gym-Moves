using GymMovesWebAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymRepository {
        public Task<bool> addGym(Gym gym);
        public Task<Gym> getGymById(int gymId);
        public Task<Gym> getGymByNameAndBranch(String name, String branch);
    }
}
