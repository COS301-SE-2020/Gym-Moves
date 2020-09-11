using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.ResponseModels {
    public class GetGymAttendanceResponse {
        public string Time { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int Count { get; set; }
    }
}
