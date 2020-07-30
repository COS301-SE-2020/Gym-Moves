using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.ResponseModels {
    public class ClassRatingResponse {
        public int classId { get; set; }
        public int ratingSum { get; set; }
        public int ratingCount { get; set; }
    }
}
