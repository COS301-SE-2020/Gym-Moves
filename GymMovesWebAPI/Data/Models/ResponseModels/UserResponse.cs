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


Functional Description:
    This file implements structure of what the objects being returned will
    look like.

List of Classes:
    - UserSignUpResponseModel
    - UserLogInResponseModel

 */

using GymMovesWebAPI.Data.Enums;

namespace GymMovesWebAPI.Data.Models.ResponseModels {
    /*
    Class Name:
        UserSignUpResponseModel

    Purpose:
        This class defines the structure of the sign up
        responses.
    */
    public class UserSignUpResponseModel {
        public bool usernameValid { get; set; }
        public bool gymMemberIdValid { get; set; }
        public UserTypes userType { get; set; }
        public string name { get; set; }
        public string gymMemberID { get; set; }
        public int gymID { get; set; }
    }

    /*
    Class Name:
        UserLogInResponseModel

    Purpose:
         This class defines the structure of the log in
        responses.
    */
    public class UserLogInResponseModel {
        public bool usernameValid { get; set; }
        public bool passwordValid { get; set; }
        public UserTypes userType { get; set; }
        public string name { get; set; }
        public string gymMemberID { get; set; }
        public int gymID { get; set; }
    }

   
}
