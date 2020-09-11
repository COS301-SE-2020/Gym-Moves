using System;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class GymAttendenceRecord {
        public int GymId { get; set; }
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
