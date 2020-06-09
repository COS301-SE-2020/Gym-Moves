using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GymMoves_WebAPI.Data.Repositories.Implementation {
    public class ClassRepository : ClassRepositoryInterface {
        private readonly MainDatabaseContext _context;

        public ClassRepository(MainDatabaseContext context) {
            _context = context;
        }

        public async Task<bool> Add(ClassEntity entity) {
            _context.Add(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Remove(ClassEntity entity) {
            _context.Remove(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<ClassEntity> FindByID(int id) {
            IQueryable<ClassEntity> query = _context.Classes
                .Include(u => u.AtGym);
            query = query.Where(c => c.ClassID == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ClassEntity> FindByClass(ClassEntity entity) {
            IQueryable<ClassEntity> query = _context.Classes
                .Include(c => c.ClassTime)
                .Include(c => c.ClassType);
            query = query.Where(c => c.ClassID == entity.ClassID);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ClassEntity[]> FindByClassType(ClassTypeEntity type) {
            IQueryable<ClassEntity> query = _context.Classes;
            query = query.Where(c => c.ClassTypeFK == type.ClassTypeID);

            return await query.ToArrayAsync();
        }

        public async Task<ClassEntity[]> FindByClassTime(ClassTimesEntity time) {
            IQueryable<ClassEntity> query = _context.Classes;
            query = query.Where(c => c.ClassTimeFK == time.ClassTimeID);

            return await query.ToArrayAsync();
        }

        public async Task<ClassEntity[]> FindByGym(GymEntity gym) {
            IQueryable<ClassEntity> query = _context.Classes
                .Include(c => c.ClassTime)
                .Include(c => c.ClassType);
            query = query.Where(c => c.AtGymFK == gym.GymID);

            return await query.ToArrayAsync();
        }

        public Task<ClassEntity[]> FindByHasSpace(GymEntity gym) {
            throw new NotImplementedException();
        }

        public Task<ClassEntity[]> FindByFull(GymEntity gym) {
            throw new NotImplementedException();
        }

        public Task<ClassEntity[]> FindByInstructor(InstructorEntity instructor) {
            throw new NotImplementedException();
        }

        public async Task<ClassEntity[]> FindByUser(UserEntity user) {
            IQueryable<UserEntity> query = _context.Users;
            query = query.Where(u => u.UserID == user.UserID);

            UserEntity dbUser = await query.FirstOrDefaultAsync();

            if (dbUser != null) {
                ClassEntity[] userClasses = dbUser.Classes.ToArray();

                return userClasses;
            } else {
                return null;
            }
        }

        public async Task<ClassEntity> RegisterUserForClass(UserEntity user, ClassEntity rClass) {
            IQueryable<ClassEntity> classQuery = _context.Classes;
            IQueryable<UserEntity> userQuery = _context.Users;

            classQuery = classQuery.Where(c => c.ClassID == rClass.ClassID);
            userQuery = userQuery.Where(u => u.UserID == user.UserID);

            ClassEntity foundClass = await classQuery.FirstOrDefaultAsync();
            UserEntity foundUser = await userQuery.FirstOrDefaultAsync();

            if (foundClass == null) {
                return null;
            }

            if (foundUser == null) {
                return null;
            }

            foundClass.Students.Add(foundUser);
            foundUser.Classes.Add(foundClass);

            return foundClass;
        }
    }
}
