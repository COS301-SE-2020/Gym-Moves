/*
File Name:
    ClassRegister.cs

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
    - ClassRegister
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class ClassRegister {
        public string StudentUsernameForeignKey { get; set; }
        [ForeignKey("StudentUsernameForeignKey")]
        public Users Student { get; set; }

        public int ClassIdForeignKey { get; set; }
        [ForeignKey("ClassIdForeignKey")]
        public GymClasses Class { get; set; }
    }
}
