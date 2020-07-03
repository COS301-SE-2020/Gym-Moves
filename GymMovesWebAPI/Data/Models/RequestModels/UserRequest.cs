/*
File Name:
    UserRequest.cs

Author:
    Danel

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    | Danel          |    Added more request models


Functional Description:
    This file implements the controller that will handle all user manangement
    activities.

List of Classes:
    - SignUpUserModel
    - LogInUserModel
    - GetCodeUserModel
    - ForgotPasswordUserModel
    - ChangePasswordUserModel

 */

namespace GymMovesWebAPI.Data.Models.ResponseModels {

    /*
    Class Name:
        SignInUserModel

    Purpose:
        This class handles the incoming JSON structure
        of a sign up request.
    */
    public class SignUpUserModel {
        public string username { get; set; }
        public string gymMemberId { get; set; }
        public string password { get; set; }
        public string gymName { get; set; }
        public string gymBranch { get; set; }
    }

    /*
    Class Name:
        LogInUserModel

    Purpose:
        This class handles the incoming JSON structure
        of a login request.
    */
    public class LogInUserModel {
        public string username { get; set; }
        public string password { get; set; }
    }

    /*
    Class Name:
        GetCodeUserModel

    Purpose:
        This class handles the incoming JSON structure
        of a get code request.
    */
    public class GetCodeUserModel
    {
        public string username { get; set; }
    }

    /*
    Class Name:
        ForgotPasswordUserModel

    Purpose:
        This class handles the incoming JSON structure
        of a forget password request.
    */
    public class ForgotPasswordUserModel
    {
        public string username { get; set; }
        public string code { get; set; }
        public string password { get; set; }
    }

    /*
    Class Name:
        ChangePasswordUserModel

    Purpose:
        This class handles the incoming JSON structure
        of a change password.
    */
    public class ChangePasswordUserModel
    {
        public string username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
