using GymMovesWebAPI.Data.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymAttendenceRepository {
        public Task<bool> addAttendence(GymAttendenceRecord record);
        public Task<GymAttendenceRecord> getAttendenceRecord(int gymId, string time, string day, string month, string year);
        public Task<bool> updateAttendence(GymAttendenceRecord record);
    }
}
