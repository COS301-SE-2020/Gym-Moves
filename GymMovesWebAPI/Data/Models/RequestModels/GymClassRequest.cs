using GymMovesWebAPI.Data.Models.ResponseModels;
using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Data.Models.RequestModels {
    public class GymClassRequest {
        [Required]
        public string Username { get; set; }
        [Required]
        public GymClassResponse NewClass { get; set; }
    }

    public class GymClassRemoveRequest {
        [Required]
        public string Username { get; set; }
        [Required]
        public int ClassId { get; set; }
    }

    public class RegisterUserForClassRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int ClassId { get; set; }
    }

    public class EditGymClassRequest {
        [Required]
        public string EditorUsername { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public string InstructorUsername { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
        [Required]
        public int MaxCapacity { get; set; }
        [Required]
        public bool Cancelled { get; set; }
    }

    /* Class name:
           CancelClassRequest
       
       Purpose:
           To handle the incoming cancel and deletion request.
     */
    public class CancelClassRequest {
        [Required]
        public string username { get; set; }
        [Required]
        public int classId { get; set; }
    }
}
