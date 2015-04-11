using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SparRetail.Components.MailMan;
using SparRetail.Core.Email;
using SparRetail.Core.Messaging;

namespace SparRetail.PlugIns.MailMan.ConsoleHost
{
    public class MailManPlugIn : SubscriberPlugin
    {
        protected readonly IMailManWorker mailManWorker;


        public MailManPlugIn(IMailManWorker mailManWorker, ExchangeSettings exchangeSettings, HostSettings hostSettings, QueueSettings queueSettings)
            : base(exchangeSettings, hostSettings, queueSettings)
        {
            this.mailManWorker = mailManWorker;
        }

        protected override ProcessMessageResult ProcessMessage(string message, IDictionary<string, object> headers)
        {
            try
            {
                mailManWorker.SendMail(JsonConvert.DeserializeObject<MailInfo>(message));
                    return ProcessMessageResult.Success;
            }
            catch (JsonReaderException jsonEx)
            {
                Logger.Error(jsonEx);
                return ProcessMessageResult.Failed;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ProcessMessageResult.Retry;
            }
        }
    }
}
