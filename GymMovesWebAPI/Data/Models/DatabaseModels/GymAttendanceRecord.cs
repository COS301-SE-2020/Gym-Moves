using System;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class GymAttendanceRecord {
        public int GymId { get; set; }
        public string Time { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
    }
}