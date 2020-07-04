/*
File Name:
    NotificationSettings.cs

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
    - NotificationSettings
*/

using GymMovesWebAPI.Models.DatabaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class NotificationSettings {
        [Key]
        public string UsernameForeignKey { get; set; }
        [ForeignKey("UsernameForeignKey")]
        public Users User { get; set; }

        public bool Email { get; set; }
        public bool Sms { get; set; }
        public bool PushNotifications { get; set; }
    }
}
