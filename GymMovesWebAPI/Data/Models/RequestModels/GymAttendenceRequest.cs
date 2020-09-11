using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GymAttendenceAddModel {
        public string username { get; set; }
        public int gymid { get; set; }
        public string time { get; set; }
        public string day { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
}
