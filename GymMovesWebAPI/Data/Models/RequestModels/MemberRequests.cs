using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class AddMemberRequests {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Records { get; set; }
    }
}
