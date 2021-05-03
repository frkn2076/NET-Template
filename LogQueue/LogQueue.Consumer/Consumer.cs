using LogQueue.DataAccess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace LogQueue.Consumer
{
    class Consumer
    {
        static void Main(string[] args)
        {
            var logQueueHostName = Environment.GetEnvironmentVariable("LogQueueHostName");
            var loggingQueue = Environment.GetEnvironmentVariable("LoggingQueue");
            var factory = new ConnectionFactory() { HostName = logQueueHostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: loggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                MongoRepo.InsertLog(message);
            };

            channel.BasicConsume(queue: loggingQueue, autoAck: true, consumer: consumer);

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
