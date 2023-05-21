using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Options.v1;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public class CustomerUpdateSender : ICustomerUpdateSender
    {

        private readonly string rabbitMqHostName;
        private readonly string rabbitMqQueueName;
        private readonly string rabbitMqUserName;
        private readonly string rabbitMqPassword;

        private IConnection connection;

        public CustomerUpdateSender(IOptions<RabbitMqConfiguration> options)
        {
            this.rabbitMqHostName = options.Value.HostName;
            this.rabbitMqQueueName = options.Value.QueueName;
            this.rabbitMqUserName = options.Value.UserName;
            this.rabbitMqPassword = options.Value.Password;

            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = rabbitMqHostName, UserName = this.rabbitMqUserName, Password = this.rabbitMqPassword };
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Could not create connection: {ex.Message}");
            }

        }
        public void SendUpdate(Customer customer)
        {
            if(ConnectionExists())
            {
                using (var channel=connection.CreateModel())
                {
                    channel.QueueDeclare(this.rabbitMqQueueName, false, false, false);
                    var messageBody=Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customer));
                    channel.BasicPublish(exchange:"", routingKey: this.rabbitMqQueueName, basicProperties: null, body: messageBody);
                }
            }
        }
        private bool ConnectionExists()
        {
            if(connection!=null)
            {
                return true;
            }

            CreateConnection();

            return connection != null;
        }
    }
}
