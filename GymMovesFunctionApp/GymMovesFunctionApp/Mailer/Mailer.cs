using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;

namespace GymMovesFunctionApp.Mailer {
    public static class Mailer {
        private static readonly string API_KEY = Environment.GetEnvironmentVariable("SendGridApi");
        private static readonly string FROM = "lockdown.squad.301@gmail.com";
        private static readonly string NAME = "Gym Moves";
        private static readonly string SUBJECT = "Gym Scheduled Notification";

        public static async System.Threading.Tasks.Task sendEmailAsync(string to, string notificationHeading, string notificationBody, string name, string surname, ILogger log) {
            var client = new SendGridClient(API_KEY);
            var fromMail = new EmailAddress(FROM, NAME);

            string fullName = name + " " + surname;

            var toMail = new EmailAddress(to, fullName);

            var message = new SendGridMessage();
            message.SetFrom(fromMail);
            message.AddTo(toMail);
            message.SetSubject(SUBJECT);

            string content = notificationHeading + "\t" + notificationBody;
            message.AddContent(MimeType.Text, content);

            var response = await client.SendEmailAsync(message);

            if ((int)response.StatusCode == 202) {
                log.LogInformation("[Email]: Successfully sent email");
            } else {
                log.LogInformation("[Email]: Unsuccessfully sent email");
            }
        }
    }
}
