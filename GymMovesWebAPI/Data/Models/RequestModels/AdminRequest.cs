using GymMovesWebAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels
{
    public class add {       
        public string username { get; set; }
        public SupportUsers user { get; set; }
    }

    public class AdminSignupRequest
    {
        public string username { get; set; }
        public string tempPassword { get; set; }
        public string password { get; set; }
    }

    public class AdminLoginRequest
    {
        string username { get; set; }
        string password { get; set; }
    }


    public class GetCodeAdminRequest
    {
        string username { get; set; }

    }

    public class ResetPasswordAdminRequest
    {
        string username { get; set; }
        string code { get; set; }
        string password { get; set; }

    }

    public class ChangePasswordAdminRequest
    {
        string username { get; set; }
        string oldPassword { get; set; }
        string newPassword { get; set; }

    }
}
