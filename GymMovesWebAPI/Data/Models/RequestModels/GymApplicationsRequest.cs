using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GymApplicationsRequest {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string GymName { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Extra { get; set; }
    }

    public class GetAllApplicationsRequest {
        [Required]
        public string Username { get; set; }
    }

    public class GetAnApplicationRequest {
        [Required]
        public string Username { get; set; }
        [Required]
        public string GymName { get; set; }
        [Required]
        public string BranchName { get; set; }
    }

    public class SetApplicationState
    {
        [Required]
        public string status { get; set; }
        [Required]
        public string GymName { get; set; }
        [Required]
        public string BranchName { get; set; }
    }
}
