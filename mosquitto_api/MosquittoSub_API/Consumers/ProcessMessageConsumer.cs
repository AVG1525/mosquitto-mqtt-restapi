using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MosquittoSub_API.Controllers;
using MosquittoSub_API.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MosquittoSub_API.Consumers
{
    public class ProcessMessageConsumer : BackgroundService
    {
        private readonly RabbitMqConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ProcessMessageConsumer(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;

            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host,
                Port = _configuration.Port
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _configuration.Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                try
                {
                    var contentArray = eventArgs.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);

                    var responseMosquittoController = new Mosquitto().Post(contentString);
                
                    Console.WriteLine(" [x] Received {0} - Post {1}", contentString, responseMosquittoController);

                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                } catch
                {
                    _channel.BasicNack(eventArgs.DeliveryTag, false, true);
                }
                
                
            };

            _channel.BasicConsume(_configuration.Queue, false, consumer);

            return Task.CompletedTask;
        }
    }
}
