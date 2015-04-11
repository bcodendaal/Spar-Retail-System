using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Email
{
    public interface IMailer
    {
        void QueueEmail(string body, string subject, string recipient, string from);
        void SendMail(string body, string subject, string recipient, string from);
    }
}
