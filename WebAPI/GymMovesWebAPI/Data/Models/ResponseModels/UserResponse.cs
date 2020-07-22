/*
File Name:
    UserResponse.cs

Author:
    Danel

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    |    Danel       |    Added more response fields
--------------------------------------------------------------------------------
14/07/2020    |    Danel       |    Changed response fields
--------------------------------------------------------------------------------


Functional Description:
    This file implements structure of what the objects being returned will
    look like.

List of Classes:
    - UserResponseModel
    - InstructorResponseModel

 */

using GymMovesWebAPI.Data.Enums;

namespace GymMovesWebAPI.Data.Models.ResponseModels {

    /*
    Class Name:
        UserResponseModel

    Purpose:
        This class defines the structure of the sign up
        responses.
    */
    public class UserResponseModel {
        public UserTypes userType { get; set; }
        public string name { get; set; }
        public string gymMemberID { get; set; }
        public int gymID { get; set; }
    }

    /*
   Class Name:
       InstructorResponseModel

   Purpose:
       This class defines the structure of the get all
       instructors responses.
   */
    public class InstructorResponseModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
    }
}
