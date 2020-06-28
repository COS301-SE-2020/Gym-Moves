using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public class SupportUsers {
        [Key]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}