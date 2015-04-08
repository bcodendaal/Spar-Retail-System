using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using RabbitMQ.Client.Events;
using SparRetail.Core.Messaging;

namespace SparRetail.PlugIns
{
    public abstract class SubscriberPlugin
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        private bool IsRunning = false;
        private QueueingBasicConsumer consumer;

        protected ExchangeSettings ExchangeSettings;
        protected HostSettings HostSettings;
        protected QueueSettings QueueSettings;

        protected ILog Logger = LogManager.GetLogger("Subscriber");

        public SubscriberPlugin(ExchangeSettings exchangeSettings, HostSettings hostSettings, QueueSettings queueSettings)
        {
            ExchangeSettings = exchangeSettings;
            HostSettings = hostSettings;
            QueueSettings = queueSettings;
            Initialize();
        }

        private void Initialize()
        {
            _factory = new ConnectionFactory
            {
                HostName = HostSettings.Host,
                VirtualHost = HostSettings.VHost,
                UserName = HostSettings.Username,
                Password = HostSettings.Password,
                Port = HostSettings.Port
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            if (ExchangeSettings.AutoCreate)
                _channel.ExchangeDeclare(ExchangeSettings.ExchangeName, ExchangeSettings.ExchangeType, ExchangeSettings.Durable, ExchangeSettings.AutoDelete, null);

            _channel.QueueDeclare(QueueSettings.QueueName, true, false, false, QueueSettings.MessageTTL == 0 ? null : new Dictionary<string, object> { { "x-message-ttl", QueueSettings.MessageTTL }});
            _channel.QueueBind(QueueSettings.QueueName, ExchangeSettings.ExchangeName, QueueSettings.RoutingKey);

            _channel.BasicQos(0, 1, false);
            consumer = new QueueingBasicConsumer(_channel);
            _channel.BasicConsume(QueueSettings.QueueName, false, consumer);

        }

        public void Start()
        {
            IsRunning = true;
            new Thread(Spin).Start();
        }

        protected void Spin()
        {
            while (IsRunning)
            {
                Work();
            }
        }

        private void Work()
        {

            try
            {
                BasicDeliverEventArgs args;
                if (consumer.IsRunning)
                {
                    consumer.Queue.Dequeue(100, out args);
                    if (args != null)
                    {
                        var result = ProcessMessage(Encoding.UTF8.GetString(args.Body), args.BasicProperties.Headers);
                        switch (result)
                        {
                            case ProcessMessageResult.Success:
                                _channel.BasicAck(args.DeliveryTag, false);
                                break;
                            case ProcessMessageResult.Failed:
                                SendToErrorQueue(args);
                                _channel.BasicAck(args.DeliveryTag, false);
                                break;
                            case ProcessMessageResult.Retry:
                                SetupRetry(args);
                                _channel.BasicAck(args.DeliveryTag, false);
                                break;
                            default:
                                _channel.BasicAck(args.DeliveryTag, false);
                                break;
                        }
                    }
                }
                else
                    Reconnect();
                   

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        private void SendToErrorQueue(BasicDeliverEventArgs args)
        {
            _channel.QueueDeclare(QueueSettings.QueueName + ".error", true, false, true, null);
            _channel.BasicPublish("",  QueueSettings.QueueName + ".error", null, args.Body);
        }

        private void SetupRetry(BasicDeliverEventArgs args)
        {
            if(args.BasicProperties.Headers == null)
                args.BasicProperties.Headers = new Dictionary<string,object>();

            if(!args.BasicProperties.Headers.ContainsKey("retry-count"))
                args.BasicProperties.Headers.Add("retry-count", 0);

            if ((int)args.BasicProperties.Headers["retry-count"] <= 5)
            {
                args.BasicProperties.Headers["retry-count"] = (int)args.BasicProperties.Headers["retry-count"] + 1;

                _channel.ExchangeDeclare(ExchangeSettings.ExchangeName + ".retry", "topic");
                _channel.QueueDeclare(QueueSettings.QueueName + ".retry", true, false, true, new Dictionary<string, object> { { "x-dead-letter-exchange", ExchangeSettings.ExchangeName }, { "x-message-ttl", 30000 } });
                _channel.QueueBind(QueueSettings.QueueName + ".retry", ExchangeSettings.ExchangeName + ".retry", "#");

                _channel.BasicPublish(ExchangeSettings.ExchangeName + ".retry", args.RoutingKey, args.BasicProperties, args.Body);
            }
            else
            {
                SendToErrorQueue(args);
            }
                
        }

        protected abstract ProcessMessageResult ProcessMessage(string message, IDictionary<string, object> headers);

        public void Stop()
        {
            IsRunning = false;

            DisposeRabbit();
        }

        private void DisposeRabbit()
        {
            if(_channel != null)
                _channel.Dispose();

            if(_connection != null)
                _connection.Dispose();
        }

        private void Reconnect()
        {
            DisposeRabbit();
            Thread.Sleep(10000);
            Initialize();
        }

    }
}
