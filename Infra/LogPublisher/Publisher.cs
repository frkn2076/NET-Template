using Infra.Constants;
using RabbitMQ.Client;
using System.Text;

namespace Infra.LogPublisher
{
    public class Publisher
    {
        private static readonly ConnectionFactory _factory = new ConnectionFactory() { HostName = PrebuiltVariables.LogQueueHostName };
        private static readonly IConnection _connection = _factory.CreateConnection();
        private static IModel _channel => DeclareQueue();
        private static IModel DeclareQueue()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: PrebuiltVariables.ReqResLoggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            return channel;
        }

        public static void Send(string payload)
        {
            var body = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(exchange: "", routingKey: PrebuiltVariables.ReqResLoggingQueue, basicProperties: null, body: body);
        }
    }
}
