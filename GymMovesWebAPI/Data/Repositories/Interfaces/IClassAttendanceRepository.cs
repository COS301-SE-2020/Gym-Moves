using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GymMovesWebAPI.Data.Models.DatabaseModels;

namespace GymMovesWebAPI.Data.Repositories.Interfaces
{
    public interface IClassAttendanceRepository
    {
        public Task<bool> addNewClassInstance(ClassAttendance classToGet);
        public Task<ClassAttendance> getClassInstance(int classId, DateTime date);
        public Task<ClassAttendance[]> getClassAttendance(int classId);
        public Task<bool> editClassAttendance(int classId, DateTime date, int change);
        public Task<bool> editClassCapacity(int classId, DateTime date, int change);
        public Task<bool> removeClass(int classId);
    }
}
