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

using System.Threading.Tasks;

namespace GymMovesWebAPI.MailerProgram {
    public interface IMailer {
        public Task<int> sendEmail(string fromEmail, string fromName, string subject, string content, string to, bool isHtml = false);
        public Task<int> sendEmail(string fromEmail, string fromName, string subject, string content, string[] to, bool isHtml = false);
    }
}