using GymMovesWebAPI.Data.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymAttendenceRepository {
        public Task<bool> addAttendence(GymAttendanceRecord record);
        public Task<GymAttendanceRecord> getAttendenceRecord(int gymId, string time, int day, int month, int year);
        public Task<bool> updateAttendence(GymAttendanceRecord record);
        public Task<GymAttendanceRecord[]> GetAttendanceRecords(int gymId);
    }
}
