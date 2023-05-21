using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Query
{
    public class GetPaidOrdersQueryHandler : IRequestHandler<GetPaidOrdersQuery, List<Order>>
    {
        private readonly IOrderRepository orderRepository;

        public GetPaidOrdersQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<List<Order>> Handle(GetPaidOrdersQuery request, CancellationToken cancellationToken)
        {
            return await orderRepository.GetPaidOrdersAsync(cancellationToken);
        }
    }
}
