using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class ClassRatingRequest {
        [Required]
        public string username { get; set; }
        [Required]
        public int classId { get; set; }
        [Required]
        public int rating { get; set; }
    }

    public class InstructorRatingRequest {
        [Required]
        public string username { get; set; }
        [Required]
        public string instructor { get; set; }
        [Required]
        public int rating { get; set; }
    }
}
