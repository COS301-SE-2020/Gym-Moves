using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.ResponseModels {
    public class AddMemberResponse {
        public int Success { get; set; }
        public int Duplicates { get; set; }
        public int Failure { get; set; }
    }
}
