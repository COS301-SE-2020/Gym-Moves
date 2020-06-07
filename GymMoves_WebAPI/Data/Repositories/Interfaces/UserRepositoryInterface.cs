using GymMoves_WebAPI.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.Repositories {
    public interface UserRepositoryInterface {
        //Returns true if and only if the add operation is successful
        public Task<bool> Add(UserEntity entity);
        public Task<bool> RemoveUser(UserEntity entity);

        public Task<bool> AddInstructor(InstructorEntity entity);
        public Task<bool> RemoveInstructor(InstructorEntity entity);

        public Task<UserEntity> GetUserWithID(string id);
        public Task<InstructorEntity> GetInstructorWithID(string id);

        public Task<ClassEntity[]> GetUserClasses(string id);
    }
}
