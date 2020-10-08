using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.ResponseModels {
    public class WeightResponse {
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime Date { get; set; }
    }
}
