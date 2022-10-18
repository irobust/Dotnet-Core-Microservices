using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PaymentService{
    public class RabbitMQConsumer : BackgroundService  
    {  
        private IConnection _connection;  
        private IModel _channel;
        private OrderService _storage;
    
        public RabbitMQConsumer(ILoggerFactory loggerFactory, OrderService storage)  
        {  
            InitRabbitMQ();  
            this._storage = storage;
        }
    
        private void InitRabbitMQ()  
        {  
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://webapp:rabbitmq@localhost:5672");   
            _connection = factory.CreateConnection();  
            _channel = _connection.CreateModel();
        }
    
        protected override Task ExecuteAsync(CancellationToken stoppingToken)  
        {  
            stoppingToken.ThrowIfCancellationRequested();  

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, e) => {
                var orderString = Encoding.UTF8.GetString(e.Body.ToArray());
                var order = JsonConvert.DeserializeObject<Order>(orderString);

                this._storage.Add(order);
                Console.WriteLine($"OrderId: {order.OrderID}, Date Created: {order.OrderDate}");
            };            
           
            _channel.BasicConsume("order.create", true, consumer);

            return Task.CompletedTask;  
        }
    
        public override void Dispose()  
        {  
            _channel.Close();  
            _connection.Close();  
            base.Dispose();  
        }  
    }  
}