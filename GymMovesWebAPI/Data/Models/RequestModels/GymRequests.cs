using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class getMembersRequest {
        public string Username { get; set; }
        public int GymId { get; set; }
    }
}
