using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMoves_WebAPI.Data.EntityModels {
    public enum UserType {
        Member, Staff, Manager
    }

    public class UserEntity {   
        //Gym membership number
        [Key]
        public string UserID { get; set; }
        
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        
        public int RegisteredGymFK { get; set; }
        [ForeignKey("RegisteredGymFK")]
        public GymEntity RegisteredGym { get; set; }
    
        public ICollection<ClassEntity> Classes { get; set; }
    }
}
