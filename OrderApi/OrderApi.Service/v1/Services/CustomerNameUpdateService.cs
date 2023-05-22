using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Domain.Entities;
using OrderApi.Service.v1.Command;
using OrderApi.Service.v1.Models;
using OrderApi.Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Services
{
    public class CustomerNameUpdateService : ICustomerNameUpdateService
    {
        private readonly IMediator mediator;

        public CustomerNameUpdateService(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async void UpdateCustomerName(UpdateCustomerFullNameModel updateCustomerFullNameModel)
        {

            try
            {
                
                var customerOrders = await mediator.Send(new GetOrdersByCustomerIdQuery { CustomerId = updateCustomerFullNameModel.Id });
                foreach (Order order in customerOrders)
                {
                    order.CustomerFullName = $"{updateCustomerFullNameModel.FirstName} {updateCustomerFullNameModel.LastName}";
                }

                await mediator.Send(new UpdateOrderCommand { Orders = customerOrders });
                
                    
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error updating customers' fullname");
            }
           
        }
    }
}
