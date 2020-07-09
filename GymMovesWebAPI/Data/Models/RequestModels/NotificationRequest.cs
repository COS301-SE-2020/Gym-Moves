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
    - NotificationsSettingsRequest
    - GetNotificationRequest

 */

namespace GymMovesWebAPI.Data.Models.RequestModels
{
    public class NotificationRequest
    {
        public int gymId { get; set; }
        public string heading { get; set; }

        public string body { get; set; }

        public string announcementDay { get; set; }

        public string announcementMonth { get; set; }

        public string announcementYear { get; set; }
    }

    /*
    Class Name:
        NotificationsSettingsRequest

    Purpose:
        This class is the structure of the incoming change setting request.
    */
    public class NotificationsSettingsRequest
    {
        public bool push { get; set; }
        public bool email { get; set; }
        public string username { get; set; }
    }

    /*
     Method Name:
        GetNotificationRequest
     Purpose:
        This class is the structure of incoming get settings request.
     */
    public class GetNotificationRequest
    {
        public string username { get; set; }
    }
}
