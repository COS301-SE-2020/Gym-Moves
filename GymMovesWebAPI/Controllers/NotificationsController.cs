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


Functional Description:
    This file implements the controller that will handle all notification
    activities.

List of Classes:
    - NotificationsController

 */

using System;
using System.Text;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using GymMovesWebAPI.Data.Models.DatabaseModels;

namespace GymMovesWebAPI.Controllers {
    [ApiController]
    public class NotificationsController : Controller {
        private IUserRepository userRepository;
        private IGymRepository gymRepository;
        private INotificationSettingsRepository notificationSettingsRepository;
        private INotificationsRepository notificationsRepository;

        public NotificationsController(IUserRepository userRep, INotificationSettingsRepository notificationSettingsRep, INotificationsRepository notificationsRep,
            IGymRepository gymRepo)
        {

            userRepository = userRep;
            notificationSettingsRepository = notificationSettingsRep;
            notificationsRepository = notificationsRep;
            gymRepository = gymRepo;


        }
        
        private void sendGmailEmail(MailMessage message)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("tiamangena@gmail.com", "");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> addAnnouncement(NotificationRequest req) {
            DateTime dateTime = convertToDate(req);
                Notifications notif = new Notifications() { Body = req.body, GymIdForeignKey = req.gymId, Heading = req.heading, Date = dateTime };

           return await notificationsRepository.addNotification(notif);
        }

        private DateTime convertToDate(NotificationRequest req)
        {
            DateTime dateTime = new DateTime(int.Parse(req.announcementYear), int.Parse(req.announcementMonth), int.Parse(req.announcementDay));
            return dateTime;
        }


        [Route("api/sendNotification")]
        [HttpPost]
        public async Task<ActionResult<NotificationResponse>> sendEmail(NotificationRequest req)
        {

            bool sent = await addAnnouncement(req);
            if (sent)
            {

                string today = DateTime.Today.ToString("d");
                string announcementDate = convertToDate(req).ToString("d");

                if (today == announcementDate)
                {

                    Users[] Members = await gymRepository.getMembers(req.gymId);

                    foreach (Users user in Members)
                    {

                        if (user.NotificationSetting.Email)
                        {
                            string from = "tiamangena@gmail.com"; //From address    
                            MailMessage message = new MailMessage(from, user.Email);

                            message.Subject = req.heading;
                            message.Body = req.body;
                            message.BodyEncoding = Encoding.UTF8;
                            message.IsBodyHtml = true;
                            sendGmailEmail(message);
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
        [Route("api/changenotification")]
        [HttpPost]
        public async Task<ActionResult> changeNotificationSetting(NotificationsSettingsRequest request) {

            bool changed = await notificationSettingsRepository.changeSetting(request.username, request.email, request.sms, request.push);

            if (changed) {

                return Ok();
            }
            else {

                return StatusCode(500);
            }

        }

        /*
      Method Name:
         getNotificationSettings
      Purpose:
         This method handles the getting of the notification settings.
      */
        [Route("api/getnotifications")]
        [HttpPost]
        public async Task<ActionResult<GetNotificationResponse>> getNotificationSettings(GetNotificationRequest request) {

            GetNotificationResponse response = new GetNotificationResponse();

            NotificationSettings settings = await notificationSettingsRepository.getSettingsOfUser(request.username);

            if (settings != null) {

                response.email = settings.Email;
                response.sms = settings.Sms;
                response.push = settings.PushNotifications;

                return Ok(response);
            }
            else {

                return StatusCode(500, response);
            }

        }
    }
}
