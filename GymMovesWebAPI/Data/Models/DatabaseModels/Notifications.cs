using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {  
    public class Notifications {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        public int GymIdForeignKey { get; set; }
        [ForeignKey("GymIdForeignKey")]
        public Gym Gym { get; set; }

        public string Heading { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}
