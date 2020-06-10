using GymMoves_WebAPI.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.Repositories {
    public interface GymRepositoryInterface {
        public Task<bool> Add(GymEntity entity);
        public Task<bool> Remove(GymEntity entity);

        public Task<GymEntity> FindById(int id);
    }
}
