using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public enum UserTypes {
        Member,
        Instructor,
        Manager
    }

    public class Users {
        [Key]
        public string Username { get; set; }

        public int GymIdForeignKey { get; set; }
        [ForeignKey("GymIdForeignKey")]
        public Gym Gym { get; set; }

        public string MembershipId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public UserTypes UserType { get; set; }
        public string Password { get; set; }

        public InstructorRating Rating { get; set; }
        public NotificationSettings NotificationSetting { get; set; }
        public ICollection<ClassRegister> ClassRegisters { get; set; }
    }
}