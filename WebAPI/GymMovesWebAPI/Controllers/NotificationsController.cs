/*
File Name:
    NotificationsController.cs

Author:
    Tia

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
03/07/2020    | Danel          | Added changing notification settings
--------------------------------------------------------------------------------
04/07/2020    | Danel          | Added getting notification settings
--------------------------------------------------------------------------------
06/07/2020    | Longji         | Use the newly created mailer class to send email
--------------------------------------------------------------------------------
12/07/2020    | Danel          | Added getting announcements
--------------------------------------------------------------------------------


Functional Description:
    This file implements the controller that will handle all notification
    activities.

List of Classes:
    - NotificationsController

 */

using System;
using Microsoft.AspNetCore.Mvc;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.MailerProgram;
using System.Linq;
using System.Collections.Generic;

namespace GymMovesWebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller {
        private IGymRepository gymRepository;
        private INotificationSettingsRepository notificationSettingsRepository;
        private INotificationsRepository notificationsRepository;
        private readonly IMailer mailer;

        string emailReceiver = "u18059903@tuks.co.za";

        public NotificationsController(INotificationSettingsRepository notificationSettingsRep, INotificationsRepository notificationsRep,
            IGymRepository gymRepo, IMailer mail) { 
            
            notificationSettingsRepository = notificationSettingsRep;
            notificationsRepository = notificationsRep;
            gymRepository = gymRepo;
            mailer = mail;
        }
        

        private async Task<bool> addAnnouncement(NotificationRequest req) {
            DateTime dateTime = convertToDate(req);
            Notifications notif = new Notifications() { Body = req.body, GymIdForeignKey = req.gymId, Heading = req.heading, Date = dateTime };

            return await notificationsRepository.addNotification(notif);
        }

        private DateTime convertToDate(NotificationRequest req) {
            DateTime dateTime = new DateTime(int.Parse(req.announcementYear), int.Parse(req.announcementMonth), int.Parse(req.announcementDay));
            return dateTime;
        }


        [HttpPost("sendNotification")]
        public async Task<ActionResult<NotificationResponse>> sendEmail(NotificationRequest req) {

            bool sent = await addAnnouncement(req);
            if (sent) {

                string today = DateTime.Today.ToString("d");
                string announcementDate = convertToDate(req).ToString("d");

                if (today == announcementDate) {

                    Users[] Members = await gymRepository.getMembers(req.gymId);

                    string from = "tiamangena@gmail.com"; //From address   
                    await mailer.sendEmail(from, "Gym Moves Notifications", req.heading, req.body, emailReceiver);
                    
                    foreach (Users user in Members) {

                        NotificationSettings settings = await notificationSettingsRepository.getSettingsOfUser
                            (user.Username);

                        if (settings.Email) { 
                            await mailer.sendEmail(from, "Gym Moves Notifications", req.heading, req.body, user.Email);
                        }
                    }

                    return Ok(new NotificationResponse() { message = "Notification added and sent successfully." });
                }else
                    return Ok(new NotificationResponse() { message = "Notification added and will be sent on " + announcementDate});

            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new NotificationResponse() { message = "There was an error storing the announcement. Please try again later" });            
        }
        
        /*
       Method Name:
          changeNotificationSetting
       Purpose:
          This method handles the changing of the notification settings.
       */
        [HttpPost("changeNotificationSettings")]
        public async Task<ActionResult> changeNotificationSetting(ChangeNotificationsSettingsRequest request) {

            bool changed = await notificationSettingsRepository.changeSetting(request.username, request.email, request.push);

            if (changed) {

                return Ok("Changed your settings successfully.");
            }
            else {

                return StatusCode(500, "We were unable to save your settings right now.");
            }

        }

        /*
      Method Name:
         getNotificationSettings
      Purpose:
         This method handles the getting of the notification settings.
      */
        [HttpGet("getNotificationSettings")]
        public async Task<ActionResult<GetNotificationSettingsResponse>> getNotificationSettings(string username) {

            NotificationSettings settings = await notificationSettingsRepository.getSettingsOfUser(username);

            if (settings != null) {

                GetNotificationSettingsResponse response = new GetNotificationSettingsResponse();

                response.email = settings.Email;
                response.push = settings.PushNotifications;

                return Ok(response);
            }
            else {
                return StatusCode(500, "We are unable to get your notification settings right now.");
            }

        }

        /*
         Method Name:
            getAllAnnouncements
         Purpose:
            This method handles the getting of the notification settings.
         */
        [HttpGet("getAllAnnouncements")]
        public async Task<ActionResult<Notifications[]>> getAllAnnouncements(int gymID) {

            Notifications[] notifications = await notificationsRepository.getGymNotifications(gymID);

            List<Notifications> announcementsThatHaveBeenMade = new List<Notifications>();

            foreach (Notifications notif in notifications)
            {
                DateTime now = DateTime.Now;
                DateTime stored = new DateTime(notif.Date.Year, notif.Date.Month, notif.Date.Day);

                if(stored.Subtract(now).TotalDays <= 0)
                {
                    announcementsThatHaveBeenMade.Add(notif);
                }
            }

            if (notifications == null || notifications.Length == 0) {
                return NotFound("There are no announcements.");
            }
            else {
                return Ok(announcementsThatHaveBeenMade);
            }
        }
    }
}
