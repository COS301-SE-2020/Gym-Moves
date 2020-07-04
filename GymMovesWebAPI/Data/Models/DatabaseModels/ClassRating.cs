/*
File Name:
    ClassRating.cs

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
    - ClassRating
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class ClassRating {
        [Key]
        public int ClassIdForeignKey { get; set; }
        [ForeignKey("ClassIdForeignKey")]
        public GymClasses Class { get; set; }

        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
    }
}
