using System.ComponentModel.DataAnnotations;

namespace GymMoves_WebAPI.Data.EntityModels {
    public enum UserType {
        Member, Staff, Manager, Instructor
    }

    public class UserEntity {
        public string phoneNumber { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string password { get; set; }
        public UserType userType { get; set; }
        public GymEntity registeredGym { get; set; }
    }
}
