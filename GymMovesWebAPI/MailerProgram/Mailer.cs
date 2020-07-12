/*
File Name:
    Mailer.cs

Author:
    Longji

Date Created:
    06/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:

      

List of Classes:
    - Mailer
*/

using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace GymMovesWebAPI.MailerProgram {
    public class Mailer : IMailer {
        private readonly string API_KEY;

        public Mailer(IConfiguration config) {
           API_KEY = config.GetValue<string>("SendGridAPIKey");
        }

        public async Task<int> sendEmail(string fromEmail, string fromName, string subject, string content, string toEmail, bool isHtml = false) {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toEmail);

            var message = new SendGridMessage();
            message.SetFrom(from);
            message.AddTo(to);
            message.SetSubject(subject);

            if (isHtml) {
                message.AddContent(MimeType.Html, content);
            } 
            else 
            {
                message.AddContent(MimeType.Text, content);
            }

            var response = await client.SendEmailAsync(message);

            return (int)response.StatusCode;
        }

        public async Task<int> sendEmail(string fromEmail, string fromName, string subject, string content, string[] to, bool isHtml = false) {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress(fromEmail, fromName);

            bool flag = false;

            for (int i = 0; i < to.Length; i++) {
                var message = new SendGridMessage();

                message.SetFrom(from);
                message.AddTo(to[i]);

                if (isHtml) {
                    message.AddContent(MimeType.Html, content);
                } else {
                    message.AddContent(MimeType.Text, content);
                }

                var response = await client.SendEmailAsync(message);

                if ((int)response.StatusCode != 202) {
                    flag = true;
                }
            }

            if (!flag) {
                return 202;
            } else {
                return 400;
            }
        }
    }
}
