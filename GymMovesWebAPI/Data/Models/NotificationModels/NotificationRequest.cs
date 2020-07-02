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


Functional Description:
    This file contains the classes that will be used to access the data sent in a nootification post request.

List of Classes:
    NotificationRequest

 */

using System;
using System.Collections.Generic;
using System.Text;

namespace GymMovesWebAPI.Data.Models.NotificationModels
{
    class NotificationRequest
    {
        public int gymId { get; set; }
        public String heading { get; set; }

        public String body { get; set; }

        public String announcementDay { get; set; }

        public String announcementMonth { get; set; }

        public String announcementYear { get; set; }
    }
}
