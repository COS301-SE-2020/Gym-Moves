using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class ClassRatingRequest {
        public string username { get; set; }
        public int classId { get; set; }
        public int rating { get; set; }
    }
}
