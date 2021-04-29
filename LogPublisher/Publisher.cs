using RabbitMQ.Client;
using System.Text;

namespace LogPublisher
{
    public class Publisher
    {
        private static ConnectionFactory _factory = new ConnectionFactory() { HostName = "localhost" };
        private static IConnection _connection = _factory.CreateConnection();
        private static IModel _channel = QueueDeclare("logging");
        private static IModel QueueDeclare(string queue)
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            return channel;
        }

        public static void Send(string queue, string payload)
        {
            var body = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);
        }
    }
}
