using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.Repositories.Implementation {
    public class GymRepository : GymRepositoryInterface {
        private readonly MainDatabaseContext _context;

        public GymRepository(MainDatabaseContext context) {
            _context = context;
        }
        
        public async Task<bool> Add(GymEntity entity) {
            _context.Add(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Remove(GymEntity entity) {
            _context.Remove(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<GymEntity> FindById(int id) {
            IQueryable<GymEntity> query = _context.Gyms;
            query = query.Where(g => g.GymID == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
