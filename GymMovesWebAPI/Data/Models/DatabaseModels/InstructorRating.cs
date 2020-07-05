/*
File Name:
    InstructorRating.cs

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
    - InstructorRating
*/


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class InstructorRating {
        [Key]
        public string InstructorUsernameForeignKey { get; set; }
        [ForeignKey("InstructorUsernameForeignKey")]
        public Users Instructor { get; set; }

        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
    }
}
