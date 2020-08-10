using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class GymApplications {
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
        public string Extra { get; set; }
    }
}
