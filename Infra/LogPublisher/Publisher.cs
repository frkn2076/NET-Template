using RabbitMQ.Client;
using System;
using System.Text;

namespace Infra.LogPublisher
{
    public class Publisher
    {
        private static readonly string _reqResLogging = Environment.GetEnvironmentVariable("ReqResLoggingQueue");
        private static readonly string _logQueueHostName = Environment.GetEnvironmentVariable("LogQueueHostName");
        private static readonly ConnectionFactory _factory = new ConnectionFactory() { HostName = _logQueueHostName };
        private static readonly IConnection _connection = _factory.CreateConnection();
        private static IModel _channel => DeclareQueue();
        private static IModel DeclareQueue()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: _reqResLogging, durable: false, exclusive: false, autoDelete: false, arguments: null);
            return channel;
        }

        public static void Send(string payload)
        {
            var body = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(exchange: "", routingKey: _reqResLogging, basicProperties: null, body: body);
        }
    }
}
