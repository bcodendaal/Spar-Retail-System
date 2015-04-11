using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Core.Email;

namespace SparRetail.Components.MailMan
{
    public class MailManWorker : IMailManWorker
    {
        private readonly IMailer _mailer;

        public MailManWorker(IMailer mailer)
        {
            _mailer = mailer;
        }
    
        public void SendMail(MailInfo mailInfo)
        {
            _mailer.SendMail(mailInfo.Body, mailInfo.Subject, mailInfo.Recipient, mailInfo.From);   
        }
    }
}
