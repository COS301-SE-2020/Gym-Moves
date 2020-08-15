using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GetMembersRequest {
        [Required]
        public string Username { get; set; }
        [Required]
        public int GymId { get; set; }
    }
}
