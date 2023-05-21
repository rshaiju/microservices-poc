using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using MediatR;

namespace CustomerApi.Service.v1.Command
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await customerRepository.AddAsync(request.Customer);
        }
    }
}
