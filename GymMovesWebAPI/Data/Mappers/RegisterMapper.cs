/*
File Name:
    RegisterMapper.cs

Author:
    Longji

Date Created:
    02/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    This file implemets the mappers for class registers.
    

List of Classes:
    - RegisterMapper
*/

using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
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
