using Microsoft.VisualBasic;
using System;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class WeightData {
        public string Username { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime Date { get; set; }
    }
}