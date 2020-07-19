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

    public class EditGymClassRequest {
        public string EditorUsername { get; set; }
        public int ClassId { get; set; }
        public string InstructorUsername { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int MaxCapacity { get; set; }
        public bool Cancelled { get; set; }
    }

    /* Class name:
           CancelClassRequest
       
       Purpose:
           To handle the incoming cancel request.
     */

    public class CancelClassRequest {
        public string username { get; set; }
        public int classId { get; set; }
    }
}
