/*
File Name:
    Gym.cs

Author:
    Longji

Date Created:
    02/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - Gym
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class Gym {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GymId { get; set; }
        public string GymName { get; set; }
        public string GymBranch { get; set; }

        public ICollection<Notifications> Notifications { get; set; }
        public ICollection<GymClasses> Classes { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}