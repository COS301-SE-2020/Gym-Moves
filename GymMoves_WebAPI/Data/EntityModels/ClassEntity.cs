using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassEntity {
        [Key]
        public int ClassID { get; set; }

        public int ClassTypeFK { get; set; }
        [ForeignKey("ClassTypeFK")]
        public ClassTypeEntity ClassType { get; set; }

        public string InstructorIDFK { get; set; }
        [ForeignKey("InstructorIDFK")]
        public InstructorEntity Instructor { get; set; }

        public int AtGymFK { get; set; }
        [ForeignKey("AtGymFK")]
        public GymEntity AtGym { get; set; }

        public int ClassTimeFK { get; set; }
        [ForeignKey("ClassTimeFK")]
        public ClassTimesEntity ClassTime { get; set; }

        public ICollection<UserEntity> Students { get; set; }

        public int maxCapacity { get; set; }
        public int registeredCount { get; set; }
    }
}