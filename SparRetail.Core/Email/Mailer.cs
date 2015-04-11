using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Core.Messaging;

namespace SparRetail.Core.Email
{
    public class Mailer : IMailer
    {
        protected readonly IMessageProducer messageProducer;

        public Mailer(IMessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        public void QueueEmail(string body, string subject, string recipient, string from)
        {
            messageProducer.PublishMessage(MessageConstants.MailExchangeKey, new List<object> { new MailInfo { Body = body, From = from, Recipient = recipient, Subject = subject } }, "#", null);
        }

        public void SendMail(string body, string subject, string recipient, string from)
        {
            var message = new MailMessage(from, recipient);
            var client = new SmtpClient
            {
                Port = 587,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential("pieter.roodt", "lolnoob101")
            };
            message.Subject = subject;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = body;
            client.Send(message);
            Trace.WriteLine(body);
        }
    }
}
