using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System.Text;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(@"Logs\Logger.log", rollingInterval: RollingInterval.Day).CreateLogger();
            Log.Information("Logging started");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "logging", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Log.Information(message);
            };

            channel.BasicConsume(queue: "logging", autoAck: true, consumer: consumer);
        }
    }
}
