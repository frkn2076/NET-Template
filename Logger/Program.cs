using DatabaseAccess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "logging", durable: false, exclusive: false, autoDelete: false, arguments: null);

            MongoRepo.InsertLog($"Logging started at {DateTime.Now.ToString("ddMMyyyy")}");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                MongoRepo.InsertLog(message);
            };
            
            channel.BasicConsume(queue: "logging", autoAck: true, consumer: consumer);
        }
    }
}
