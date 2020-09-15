/*
File Name:
    NotificationRequest.cs

Author:
    Tia Mangena

Date Created:
    01/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    |  Danel         |    Added a request model
--------------------------------------------------------------------------------

Functional Description:
    This file contains the classes that will be used to access the data sent in a 
    notification related post request.

List of Classes:
    - NotificationRequest
    - ChangeNotificationsSettingsRequest

 */

using System.ComponentModel.DataAnnotations;

namespace GymMovesWebAPI.Data.Models.RequestModels
{
    public class NotificationRequest
    {
        [Required]
        public int gymId { get; set; }
        [Required]
        public string heading { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public string announcementDay { get; set; }
        [Required]
        public string announcementMonth { get; set; }
        [Required]
        public string announcementYear { get; set; }
    }

    /*
    Class Name:
        NotificationsSettingsRequest

    Purpose:
        This class is the structure of the incoming change setting request.
    */
    public class ChangeNotificationsSettingsRequest
    {
        [Required]
        public bool push { get; set; }
        [Required]
        public bool email { get; set; }
        [Required]
        public string username { get; set; }
    }

}
