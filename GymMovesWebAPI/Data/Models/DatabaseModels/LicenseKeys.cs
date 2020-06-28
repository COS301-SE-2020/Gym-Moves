using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public class LicenseKeys {
        [Key]
        public string LicenseKey { get; set; }
        public string Email { get; set; }
    }
}
