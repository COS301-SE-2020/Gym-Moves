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
    NotificationRequest

 */

using System;


namespace GymMovesWebAPI.Data.Models.RequestModels
{
    public class NotificationRequest
    {
        public int gymId { get; set; }
        public String heading { get; set; }

        public String body { get; set; }

        public String announcementDay { get; set; }

        public String announcementMonth { get; set; }

        public String announcementYear { get; set; }
    }

    /*
    Class Name:
        NotificationsSettingsRequest

    Purpose:
        This class is the structure of the incoming change setting request.
    */
    public class NotificationsSettingsRequest
    {
        public bool sms { get; set; }
        public bool push { get; set; }
        public bool email { get; set; }
        public string username { get; set; }
    }
}
