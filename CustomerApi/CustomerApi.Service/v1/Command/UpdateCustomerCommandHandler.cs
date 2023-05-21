using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Sender.v1;
using MediatR;

namespace CustomerApi.Service.v1.Command
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerUpdateSender customerUpdateSender;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerUpdateSender customerUpdateSender)
        {
            this.customerRepository = customerRepository;
            this.customerUpdateSender = customerUpdateSender;
        }
        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer= await customerRepository.UpdateAsync(request.Customer);
            customerUpdateSender.SendUpdate(customer);
            return customer;
        }
    }
}
