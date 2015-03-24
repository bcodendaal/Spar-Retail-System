using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Email
{
    public class Mailer : IMailer
    {
        public void SendMail(string body)
        {
            MailMessage message = new MailMessage("me@email.com", "pieter.roodt@gmail.com");
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential("pieter.roodt", "lolnoob101");
            message.Subject = "test mail";
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.Body = body;
            client.Send(message);
            Trace.WriteLine(body);
        }
    }
}
