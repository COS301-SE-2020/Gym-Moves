using GymMovesWebAPI.Data.Models.ResponseModels;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GymClassRequest {
        public string Username { get; set; }
        public GymClassResponse NewClass { get; set; }
    }

    public class GymClassRemoveRequest {
        public string Username { get; set; }
        public int ClassId { get; set; }
    }

    public class RegisterUserForClassRequest
    {
        public string Username { get; set; }
        public int ClassId { get; set; }
    }

    /* Class name:
           CancelAndDeleteClassRequest
       
       Purpose:
           To handle the incoming cancel and deletion request.
     */

    public class CancelAndDeleteClassRequest {
        public string username { get; set; }
        public int classId { get; set; }
    }
}
