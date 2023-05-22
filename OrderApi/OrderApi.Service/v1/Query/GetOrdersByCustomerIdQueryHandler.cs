using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Query
{
    public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, List<Order>>
    {
        private readonly IServiceProvider serviceProvider;

        public GetOrdersByCustomerIdQueryHandler( IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<List<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IOrderRepository orderRepository = scope.ServiceProvider.GetService<IOrderRepository>();
                return await orderRepository.GetOrdersByCustomerIdAsync(request.CustomerId, cancellationToken);
            }
                
        }
    }
}
