using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassEntity {
        public int classID { get; set; }
        public ClassTypeEntity classType { get; set; }
        public UserEntity instructorId { get; set; }
        public GymEntity atGym { get; set; }
        public ClassTimesEntity classTime { get; set; }
        public int maxCapacity { get; set; }
        public int registeredCount { get; set; }
    }
}