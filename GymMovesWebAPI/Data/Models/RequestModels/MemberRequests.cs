using Microsoft.AspNetCore.Http;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class AddMemberRequests {
        public string Username { get; set; }
        public string Records { get; set; }
    }
}
