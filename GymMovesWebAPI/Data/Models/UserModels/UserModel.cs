/*
File Name:
    UserModel.cs

Author:
    Danel

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    This file implements the controller that will handle all user manangement
    activities.

List of Classes:
    - SignInUserModel
    - LogInUserModel

 */

namespace GymMovesWebAPI.Data.Models.ArgumentModels {

    /*
    Class Name:
        SignInUserModel

    Purpose:
        This class handles the incoming JSON structure
        of a sign up request.
    */
    public class SignInUserModel {
        public string username { get; set; }
        public string gymMemberId { get; set; }
        public string password { get; set; }
        public string gym { get; set; }
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
}
