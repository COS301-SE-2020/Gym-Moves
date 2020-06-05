using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymMoves_WebAPI.Data.Repositories.Implementation {
    public class UserRepository : UserRepositoryInterface {
        private readonly MainDatabaseContext _context;
        
        public UserRepository(MainDatabaseContext context) {
            _context = context;
        }

        public async Task<bool> Add(UserEntity entity) {
            _context.Add(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> RemoveUser(UserEntity entity) {
            _context.Remove(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> AddInstructor(InstructorEntity entity) {
            _context.Add(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> RemoveInstructor(InstructorEntity entity) {
            _context.Add(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<UserEntity> GetUserWithID(string id) {
            IQueryable<UserEntity> query = _context.Users;
            query = query.Where(u => u.UserID == id);

            UserEntity user = await query.FirstOrDefaultAsync();

            return user;
        }

        public async Task<InstructorEntity> GetInstructorWithID(string id) {
            IQueryable<InstructorEntity> query = _context.Instructors;
            query = query.Where(i => i.InstructorID == id);

            InstructorEntity instructor = await query.FirstOrDefaultAsync();

            return instructor;
        }
    }
}
