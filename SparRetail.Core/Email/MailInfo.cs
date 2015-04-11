using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Email
{
    public class MailInfo
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string From { get; set; }

    }
}
