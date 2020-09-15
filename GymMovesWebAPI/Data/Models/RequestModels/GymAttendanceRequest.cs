using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GymAttendenceAddModel {
        [Required]
        public int gymid { get; set; }
        [Required]
        public string time { get; set; }
        [Required]
        public int day { get; set; }
        [Required]
        public int month { get; set; }
        [Required]
        public int year { get; set; }
    }
}
