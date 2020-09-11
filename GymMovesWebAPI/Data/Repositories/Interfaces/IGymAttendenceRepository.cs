using GymMovesWebAPI.Data.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymAttendenceRepository {
        public Task<bool> addAttendence(GymAttendenceRecord record);
        public Task<GymAttendenceRecord> getAttendenceRecord(int gymId, DateTime date);
        public Task<bool> updateAttendence(GymAttendenceRecord record);
    }
}
