/*
File Name:
    PasswordReset.cs

Author:
    Longji

Date Created:
    02/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
02/07/2020    |  Longji        |  Added all fields and data annotations needed 
              |                |  by the database table that this data model 
              |                |  represents
--------------------------------------------------------------------------------


Functional Description:
    

List of Classes:
    - PasswordReset
*/

using GymMovesWebAPI.Data.Models.DatabaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class PasswordReset {
        [Key]
        public string Username { get; set; }
        [ForeignKey("Username")]
        public Users User { get; set; }
        [StringLength(8)]
        public string Code { get; set; }
        public DateTime Expiry { get; set; } 
    }
}
