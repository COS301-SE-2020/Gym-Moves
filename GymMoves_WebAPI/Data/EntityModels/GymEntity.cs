using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymMoves_WebAPI.Data.EntityModels {
    public class GymEntity {
        [Key]
        public int GymID { get; set; }
        public string GymName { get; set; }

        public ICollection<UserEntity> Users { get; set; }
    }
}
