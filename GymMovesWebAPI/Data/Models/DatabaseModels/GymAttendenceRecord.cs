using System;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class GymAttendenceRecord {
        public int GymId { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int Count { get; set; }
    }
}