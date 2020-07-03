using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using GymMovesWebAPI.Data.Models.NotificationModels;
using Microsoft.AspNetCore.Mvc;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Implementations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GymMovesWebAPI.Controllers
{
    [ApiController]
    class NotificationsController : Controller {
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
        public void sendGmailEmail(MailMessage message)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("tiamangena@gmail.com", "xxuwyymlscsuiwhg");
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

        public async Task<bool> addAnnouncement(NotificationRequest req) {
            DateTime dateTime = new DateTime(int.Parse(req.announcementYear), int.Parse(req.announcementMonth), int.Parse(req.announcementDay));
            Notifications notif = new Notifications() { Body = req.body, GymIdForeignKey = req.gymId, Heading = req.heading, Date = dateTime };

           return await notificationsRepository.addNotification(notif);
        }


        [Route("api/sendNotification")]
        [HttpPost]
        public async Task<ActionResult<NotificationResponse>> sendEmail(NotificationRequest req)
        {
   
            Users[] Members = await gymRepository.getMembers(req.gymId);

            foreach(Users user in Members) {

                if (user.NotificationSetting.Email) { 
                    string from = "tiamangena@gmail.com"; //From address    
                    MailMessage message = new MailMessage(from, user.Email);

                    message.Subject = req.heading;
                    message.Body = req.body;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    sendGmailEmail(message);
                }
            }

            bool sent = await addAnnouncement(req);

            if (sent)
                return Ok(new NotificationResponse() { message = "Notification added and sent successfully." });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new NotificationResponse() { message = "There was an error storing the announcement. Please try again later" });
            
            
        }
    }
}
