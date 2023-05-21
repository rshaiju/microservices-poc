using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(customerRepository.GetAll().ToList());
        }
    }
}
