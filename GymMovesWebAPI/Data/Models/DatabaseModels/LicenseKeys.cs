/*
File Name:
    LicenseKeys.cs

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
    - LicenseKeys
*/



using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public class LicenseKeys {
        [Key]
        public string LicenseKey { get; set; }
        public string Email { get; set; }
    }
}
