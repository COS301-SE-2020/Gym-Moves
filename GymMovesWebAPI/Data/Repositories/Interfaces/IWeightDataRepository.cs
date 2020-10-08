using GymMovesWebAPI.Data.Models.DatabaseModels;
using Microsoft.VisualBasic;
using System;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IWeightDataRepository {
        public Task<bool> addWeight(WeightData weight);
        public Task<WeightData[]> getWeight(string username);
        public Task<WeightData> getWeightOnDay(string username, DateTime date);
        public Task<bool> updateWeight(WeightData weight);
    }
}
