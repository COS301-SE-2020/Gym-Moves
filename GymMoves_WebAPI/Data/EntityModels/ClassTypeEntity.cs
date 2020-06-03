using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassTypeEntity {
        public int typeID { get; set; }
        public string description { get; set; }
    }
}
