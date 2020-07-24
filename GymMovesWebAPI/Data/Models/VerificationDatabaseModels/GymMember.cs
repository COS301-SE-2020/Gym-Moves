using GymMovesWebAPI.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Data.Models.VerificationDatabaseModels {
    public class GymMember {
        [Required]
        public string MembershipId { get; set; }
        [Required]
        public int GymId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public UserTypes UserType { get; set; }
    }
}
