using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparRetail.Core.Email;

namespace SparRetail.Components.MailMan
{
    public interface IMailManWorker
    {
        void SendMail(MailInfo mailInfo);
    }
}
