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
--------------------------------------------------------------------------------


Functional Description:
    This file implements the controller that will handle all user manangement
    activities.

List of Classes:
    - SignUpRequestModel
    - LogInRequestModel
    - ForgotPasswordRequestModel
    - ChangePasswordRequestModel

 */

namespace GymMovesWebAPI.Data.Models.RequestModels{

    /*
    Class Name:
        SignUpRequestModel

    Purpose:
        This class handles the incoming JSON structure
        of a sign up request.
    */
    public class SignUpRequestModel
    {
        public string username { get; set; }
        public string gymMemberId { get; set; }
        public string password { get; set; }
        public string gymName { get; set; }
        public string gymBranch { get; set; }
    }

    /*
    Class Name:
        LogInRequestModel

    Purpose:
        This class handles the incoming JSON structure
        of a login request.
    */
    public class LogInRequestModel {
        public string username { get; set; }
        public string password { get; set; }
    }

    /*
    Class Name:
        ForgotPasswordRequestModel

    Purpose:
        This class handles the incoming JSON structure
        of a forget password request.
    */
    public class ForgotPasswordRequestModel
    {
        public string username { get; set; }
        public string code { get; set; }
        public string password { get; set; }
    }

    /*
    Class Name:
        ChangePasswordRequestModel

    Purpose:
        This class handles the incoming JSON structure
        of a change password.
    */
    public class ChangePasswordRequestModel
    {
        public string username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    /*
Class Name:
    GymSignUpRequestModel

Purpose:
    This class handles the incoming JSON structure
    of a gym requesting to sign up to use the app.
*/
    public class GymSignUpRequestModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string memberid { get; set; }
        public string gymName { get; set; }
        public string gymBranch { get; set; }
        public string code { get; set; }
        public string password { get; set; }


    }
}