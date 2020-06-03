using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class GymEntity {
        public int gymId { get; set; }
        public string gymName { get; set; }
    }
}
