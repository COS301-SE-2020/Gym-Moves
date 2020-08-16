using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GymApplicationsRequest {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GymName { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Extra { get; set; }
    }

    public class GetAllApplicationsRequest {
        public string Username { get; set; }
    }

    public class GetAnApplicationRequest { 
        public string Username { get; set; }
        public string GymName { get; set; }
        public string BranchName { get; set; }
    }

    public class SetApplicationState
    {
        public string status { get; set; }

        public string GymName { get; set; }
        public string BranchName { get; set; }
    }
}
