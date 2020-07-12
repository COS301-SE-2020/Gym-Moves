using GymMovesWebAPI.Data.Models.ResponseModels;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GymClassRequest {
        /* Who made the request */
        public string Username { get; set; }
        public GymClassResponse NewClass { get; set; }
    }

    public class RegisterUserForClassRequest
    {
        public string Username { get; set; }
        public int ClassId { get; set; }
    }

    public class CancelAndDeleteClassRequest {
        public string username { get; set; }
        public int classId { get; set; }
    }
}
