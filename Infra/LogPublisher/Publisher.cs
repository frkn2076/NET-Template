using Infra.Constants;
using RabbitMQ.Client;
using System.Text;

namespace Infra.LogPublisher
{
    public class Publisher
    {
        private static readonly ConnectionFactory _factory;
        private static readonly IConnection _connection;
        private static IModel _channel;
        static Publisher()
        {
            _factory = new ConnectionFactory() { HostName = PrebuiltVariables.RabbitMQHost };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: PrebuiltVariables.ReqResLoggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public static void Send(string payload)
        {
            var body = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(exchange: "", routingKey: PrebuiltVariables.ReqResLoggingQueue, basicProperties: null, body: body);
        }
    }
}
