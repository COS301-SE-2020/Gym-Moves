using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public class GymClasses {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

        public int GymIdForeignKey { get; set; }
        [ForeignKey("GymIdGymIdForeignKey")]
        public Gym Gym { get; set; }
        
        public string InstructorUsername { get; set; }
        [ForeignKey("InstructorUsername")]
        public Users Instructor { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentStudents { get; set; }
        public bool Cancelled { get; set; }


        public ClassRating ClassRating { get; set; }

        public ICollection<ClassRegister> Registers { get; set; }
    }
}
