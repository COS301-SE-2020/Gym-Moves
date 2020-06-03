using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class ClassTimesEntity {
        public int classTimeID { get; set; }
        public string day { get; set; }
        public string time { get; set; }
    }
}
