using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class InstructorEntity {
        [Key]
        public string InstructorID { get; set; }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public int RegisteredGymFK { get; set; }
        
        [ForeignKey("RegisteredGymFK")]
        public GymEntity RegisteredGym { get; set; }

        public ICollection<ClassEntity> InstructedClasses { get; set; }
    }
}
