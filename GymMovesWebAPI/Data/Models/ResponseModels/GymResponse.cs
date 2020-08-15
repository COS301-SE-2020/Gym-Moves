using GymMovesWebAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.ResponseModels {
    public class getMembersResponse {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MembershipId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserTypes UserType { get; set; }
    }
}
