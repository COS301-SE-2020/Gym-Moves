using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int classID { get; set; }
        public string className { get; set; }
        public int atGym { get; set; }
        public int classCapacity { get; set; }
        public int currentStudentCount { get; set; }
        public string classDays { get; set; }
        public string classTimes { get; set; }
        public int classType { get; set; }
        public int classInstructor { get; set; }
    }
}