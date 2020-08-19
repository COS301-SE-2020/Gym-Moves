using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GymMovesWebAPI.Data.Repositories.Implementations
{
    public class ClassAttendanceRepository : IClassAttendanceRepository
    {

        private readonly MainDatabaseContext context;

        public ClassAttendanceRepository(MainDatabaseContext context)
        {
            this.context = context;
        }

        public async Task<bool> addNewClassInstance(ClassAttendance classToGet)
        {
            context.Add(classToGet);

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<ClassAttendance> getClassInstance(int classId, DateTime date)
        {
            IQueryable<ClassAttendance> query = context.ClassAttendance;
            query = query.Where(p => p.ClassId == classId);

            if(query != null)
            {
                query = query.Where(p => p.Date == date);

            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ClassAttendance[]> getClassAttendance(int classId)
        {
            IQueryable<ClassAttendance> query = context.ClassAttendance;
            query = query.Where(p => p.ClassId == classId);

            return await query.ToArrayAsync();
        }

        public async Task<bool> editClassAttendance(int classId, DateTime date, int change)
        {
            var classToChange = context.ClassAttendance.Where(a => a.ClassId == classId && a.Date == date).FirstOrDefault();

            classToChange.NumberOfStudents += change;

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> editClassCapacity(int classId, DateTime date, int change)
        {
            var classToChange = context.ClassAttendance.Where(a => a.ClassId == classId && a.Date == date).FirstOrDefault();

            classToChange.Capacity = change;

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> removeClass(int classId)
        {
            context.ClassAttendance.RemoveRange(context.ClassAttendance.Where(x => x.ClassId == classId));

            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
