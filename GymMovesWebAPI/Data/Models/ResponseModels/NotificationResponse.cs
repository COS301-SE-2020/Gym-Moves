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
    This file contains the classes that will be used to respond to a notification
    request.

List of Classes:
    NotificationResponse

 */

using System;
using System.Collections.Generic;
using System.Text;

namespace GymMovesWebAPI.Data.Models.ResponseModels
{
    class NotificationResponse
    {
        public String message { get; set; }
    }
}
