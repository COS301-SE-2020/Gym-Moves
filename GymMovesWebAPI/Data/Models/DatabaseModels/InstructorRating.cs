using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class InstructorRating {
        [Key]
        public string InstructorUsernameForeignKey { get; set; }
        [ForeignKey("InstructorUsernameForeignKey")]
        public Users Instructor { get; set; }

        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
    }
}
