using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public class ClassRating {
        [Key]
        public int ClassIdForeignKey { get; set; }
        [ForeignKey("ClassIdForeignKey")]
        public GymClasses Class { get; set; }

        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
    }
}
