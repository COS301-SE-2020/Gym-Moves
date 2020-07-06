using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMovesWebAPI.MailerProgram;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GymMovesWebAPI.Controllers {
    public class TestModel {
        public string tt { get; set; }
        public int nn { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {
        private readonly IMailer mailer;

        public TestController(IMailer mail) {
            mailer = mail;
        }

        [HttpPost("poof")]
        public ActionResult<TestModel> poof(TestModel t) {
            Console.WriteLine("Request...");
            return Ok(t);
        }

        [HttpGet("send")]
        public async Task<ActionResult> sendMail(string to) {
            return Ok(await mailer.sendEmail("derpyissunny@gmail.com", "Longji Kang", "Notification", "This is a notification email\nPlease abc",to));
        }
    }
}
