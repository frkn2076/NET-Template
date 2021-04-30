using RabbitMQ.Client;
using System;
using System.Text;

namespace LogPublisher
{
    public class Publisher
    {
        private static readonly string _loggingQueue = Environment.GetEnvironmentVariable("LoggingQueue");
        private static readonly string _logQueueHostName = Environment.GetEnvironmentVariable("LogQueueHostName");
        private static readonly ConnectionFactory _factory = new ConnectionFactory() { HostName = _logQueueHostName };
        private static readonly IConnection _connection = _factory.CreateConnection();
        private static IModel _channel => DeclareQueue();
        private static IModel DeclareQueue()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: _loggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            return channel;
        }

        public static void Send(string payload)
        {
            var body = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(exchange: "", routingKey: _loggingQueue, basicProperties: null, body: body);
        }
    }
}
