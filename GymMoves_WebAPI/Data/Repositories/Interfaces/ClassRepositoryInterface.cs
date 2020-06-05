using GymMoves_WebAPI.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.Repositories {
    public interface ClassRepositoryInterface {
        public Task<bool> Add<T>(T entity) where T : class;
        public Task<bool> Remove<T>(T entity) where T : class;

        public Task<ClassEntity[]> FindByClassType(ClassTypeEntity type);
        public Task<ClassEntity[]> FindByClassTime(ClassTimesEntity time);
        public Task<ClassEntity[]> FindByGym(GymEntity gym);
        public Task<ClassEntity[]> FindByHasSpace(GymEntity gym);
        public Task<ClassEntity[]> FindByFull(GymEntity gym);
        public Task<ClassEntity[]> FindByInstructor(InstructorEntity instructor);
        public Task<ClassEntity[]> FindByUser(UserEntity user);
    }
}
