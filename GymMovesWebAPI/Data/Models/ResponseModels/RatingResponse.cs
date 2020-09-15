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

    public class AllClassRatingResponse {
        public string Name { get; set; }
        public string Instructor { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
    }

    public class InstructorRatingResponse {
        public string instructor { get; set; }
        public int ratingSum { get; set; }
        public int ratingCount { get; set; }
    }
}
