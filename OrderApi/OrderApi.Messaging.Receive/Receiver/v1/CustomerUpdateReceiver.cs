using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderApi.Messaging.Receive.Options.v1;
using OrderApi.Service.v1.Models;
using OrderApi.Service.v1.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel;
using System.Text;

namespace OrderApi.Messaging.Receive.Receiver.v1
{
    public class CustomerUpdateReceiver: BackgroundService
    {
        private readonly string rabbitMqHostName;
        private readonly string rabbitMqQueueName;
        private readonly string rabbitMqUserName;
        private readonly string rabbitMqPassword;
        private readonly ICustomerNameUpdateService customerNameUpdateService;
        private IConnection connection;
        private IModel channel;
        public CustomerUpdateReceiver(IOptions<RabbitMqConfiguration> options, ICustomerNameUpdateService customerNameUpdateService)
        {
            this.rabbitMqHostName = options.Value.HostName;
            this.rabbitMqQueueName = options.Value.QueueName;
            this.rabbitMqUserName = options.Value.UserName;
            this.rabbitMqPassword = options.Value.Password;

            InitializeRabbitMqListener();
            this.customerNameUpdateService = customerNameUpdateService;
        }

        private void InitializeRabbitMqListener()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = rabbitMqHostName, UserName = this.rabbitMqUserName, Password = this.rabbitMqPassword };
                connection = factory.CreateConnection();
                connection.ConnectionShutdown += Connection_ConnectionShutdown;
                channel = connection.CreateModel();
                channel.QueueDeclare(this.rabbitMqQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Could not create connection: {ex.Message}");
            }

        }
        

        private void Connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer= new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            consumer.Shutdown += Consumer_Shutdown; 
            consumer.Registered += Consumer_Registered;
            consumer.Unregistered += Consumer_Unregistered;
            consumer.ConsumerCancelled += Consumer_ConsumerCancelled;

            channel.BasicConsume(this.rabbitMqQueueName, false, consumer);

            return Task.CompletedTask;
        }

        private void Consumer_ConsumerCancelled(object? sender, ConsumerEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Consumer_Unregistered(object? sender, ConsumerEventArgs e)
        {

        }

        

        private void Consumer_Registered(object? sender, ConsumerEventArgs e)
        {

        }

        private void Consumer_Shutdown(object? sender, ShutdownEventArgs e)
        {

        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
           
            var customerFullNameModel = JsonConvert.DeserializeObject<UpdateCustomerFullNameModel>(Encoding.UTF8.GetString(e.Body.ToArray()));
            this.customerNameUpdateService.UpdateCustomerName(customerFullNameModel);
            
            channel.BasicAck(e.DeliveryTag, false);
        }
        public override void Dispose()
        {
            channel.Close();
            connection.Close();
            base.Dispose();
        }
    }
}
