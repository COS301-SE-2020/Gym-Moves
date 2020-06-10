using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassTimesEntity {
        [Key]
        public int ClassTimeID { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
    }
}
