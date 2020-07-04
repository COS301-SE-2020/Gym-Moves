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
    - SignUpResponseModel
    - LogInResponseModel
    - ForgotPasswordResponseModel
    - ChangePasswordResponseModel

 */

using GymMovesWebAPI.Data.Enums;

namespace GymMovesWebAPI.Data.Models.ResponseModels {
    /*
    Class Name:
        SignUpResponseModel

    Purpose:
        This class defines the structure of the sign up
        responses.
    */
    public class SignUpResponseModel
    {
        public bool usernameValid { get; set; }
        public bool gymMemberIdValid { get; set; }
        public UserTypes userType { get; set; }
        public string name { get; set; }
        public string gymMemberID { get; set; }
        public int gymID { get; set; }
    }

    /*
    Class Name:
       LogInResponseModel

    Purpose:
         This class defines the structure of the log in
        responses.
    */
    public class LogInResponseModel {
        public bool usernameValid { get; set; }
        public bool passwordValid { get; set; }
        public UserTypes userType { get; set; }
        public string name { get; set; }
        public string gymMemberID { get; set; }
        public int gymID { get; set; }
    }

    /*
   Class Name:
      ForgotPasswordResponseModel

   Purpose:
        This class defines the structure of the forgot password
       responses.
   */
    public class ForgotPasswordResponseModel {
        public string message { get; set; }

    }

    /*
   Class Name:
       ChangePasswordResponseModel

   Purpose:
        This class defines the structure of the change 
        password responses.
   */
    public class ChangePasswordResponseModel
    {
        public string message { get; set; }

    }


}
