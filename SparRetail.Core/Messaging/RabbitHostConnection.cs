using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Messaging
{
    public class RabbitHostConnection
    {
        protected HostSettings HostSettings;
        protected ConnectionFactory ConnectionFactory;
        protected IModel Channel;
        protected IConnection Connection;

        public RabbitHostConnection(HostSettings hostSettings)
        {
            HostSettings = hostSettings;

        }

        public void Initialize()
        {
            if (HostSettings == null)
                throw new NullReferenceException("HostSettings cannot be null");

            if (ConnectionFactory == null)
                ConnectionFactory = new ConnectionFactory
                {
                    HostName = HostSettings.Host,
                    Password = HostSettings.Password,
                    UserName = HostSettings.Username,
                    VirtualHost = HostSettings.VHost,
                    Port = HostSettings.Port
                };

            if (Connection == null)
                Connection = ConnectionFactory.CreateConnection();
            if (Channel == null)
                Channel = Connection.CreateModel();
        }

        public void Reconnect()
        {
            Initialize();
        }

        public void DeclareExchange(ExchangeSettings exchangeSettings)
        {
            Channel.ExchangeDeclare(exchangeSettings.ExchangeName, exchangeSettings.ExchangeType, exchangeSettings.Durable, exchangeSettings.AutoDelete, null);
        }

        public void PublishMessage(ExchangeSettings exchangeSettings, List<string> messages, string routingKey, Dictionary<string, object> headers = null)
        {
            if(messages != null)
                messages.ForEach(message=> Channel.BasicPublish(exchangeSettings.ExchangeName, routingKey, null, Encoding.UTF8.GetBytes(message)));
        }
    }
}
