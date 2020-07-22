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
04/07/2020    | Danel          |  Added new response model
--------------------------------------------------------------------------------


Functional Description:
    This file contains the classes that will be used to respond to a notification
    request.

List of Classes:
    - NotificationResponse
    - GetNotificationSettingsResponse

 */

using System;
using System.Collections.Generic;
using System.Text;

namespace GymMovesWebAPI.Data.Models.ResponseModels
{
    public class NotificationResponse {
        public string message { get; set; }
    }

    public class GetNotificationSettingsResponse
    {
        public bool email { get; set; }
        public bool push { get; set; }
    }
}
