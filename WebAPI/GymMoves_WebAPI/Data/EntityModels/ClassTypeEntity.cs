using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassTypeEntity {
        [Key]
        public int ClassTypeID { get; set; }
        public string ClassName { get; set; }
    }
}
