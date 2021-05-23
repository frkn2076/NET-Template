using LogQueue.DataAccess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace LogQueue.Consumer
{
    class Consumer
    {
        static void Main(string[] args)
        {
            var logQueueHostName = Environment.GetEnvironmentVariable("LogQueueHostName");
            var reqResLoggingQueue = Environment.GetEnvironmentVariable("reqreslogging");
            var errorLoggingQueue = Environment.GetEnvironmentVariable("ErrorLoggingQueue");
            var factory = new ConnectionFactory() { HostName = logQueueHostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: reqResLoggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: errorLoggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                MongoRepo.InsertLog(message);
                //var a = reqResLoggingQueue
                //switch (ea.RoutingKey)
                //{
                //    case reqResLoggingQueue:
                //        break;
                //    case errorLoggingQueue:
                //        break;
                //    default:
                //        break;
                //}
            };

            channel.BasicConsume(queue: reqResLoggingQueue, autoAck: true, consumer: consumer);

            new ManualResetEvent(false).WaitOne();
        }
    }
}
