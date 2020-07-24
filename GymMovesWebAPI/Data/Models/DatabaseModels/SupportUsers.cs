/*
File Name:
    SupportUsers.cs

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
    - SupportUsers
*/

using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public class SupportUsers {
        [Key]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}