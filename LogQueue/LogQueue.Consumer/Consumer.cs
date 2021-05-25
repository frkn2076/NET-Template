using Infra.Constants;
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
            var factory = new ConnectionFactory() { HostName = PrebuiltVariables.RabbitMQHost };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: PrebuiltVariables.ReqResLoggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: PrebuiltVariables.ErrorLoggingQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                
                switch (ea.RoutingKey)
                {
                    case PrebuiltVariables.ReqResLoggingQueue:
                        MongoRepo.InsertLog(message);
                        break;
                    case PrebuiltVariables.ErrorLoggingQueue:
                        //TODO:
                        break;
                    default:
                        break;
                }
            };

            channel.BasicConsume(queue: PrebuiltVariables.ReqResLoggingQueue, autoAck: true, consumer: consumer);
            channel.BasicConsume(queue: PrebuiltVariables.ErrorLoggingQueue, autoAck: true, consumer: consumer);

            new ManualResetEvent(false).WaitOne();
        }
    }
}
