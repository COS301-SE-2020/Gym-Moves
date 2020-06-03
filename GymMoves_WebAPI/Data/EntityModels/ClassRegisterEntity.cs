using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassRegisterEntity {
        public int registerID { get; set; }
        public ClassEntity classId { get; set; }
        public UserEntity studentId { get; set; }
    }
}
