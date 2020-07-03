using GymMovesWebAPI.Data.Models.ArgumentModels;
using GymMovesWebAPI.Models.DatabaseModels;

namespace GymMovesWebAPI.Data.Mappers {
    public static class RegisterMapper {
        public static ClassRegister registerUserForClassToClassRegister(RegisterUserForClassRequest source) {
            ClassRegister target = new ClassRegister();

            target.ClassIdForeignKey = source.ClassId;
            target.StudentUsernameForeignKey = source.Username;

            return target;
        }
    }
}
