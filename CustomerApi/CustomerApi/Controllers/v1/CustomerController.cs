using AutoMapper;
using CustomerApi.Domain.Entities;
using CustomerApi.Models.v1;
using CustomerApi.Service.v1.Command;
using CustomerApi.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CustomerApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CustomerController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            try
            {
                return await this.mediator.Send(new GetCustomersQuery());
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Customer>>CreateCustomer(CreateCustomerModel createCustomerModel)
        {
            try
            {
                return await mediator.Send(new CreateCustomerCommand { Customer = this.mapper.Map<Customer>(createCustomerModel) }); ;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> UpdateCustomer(UpdateCustomerModel updateCustomerModel)
        {
            try
            {
                var customerFromStore = await mediator.Send(new GetCustomerByIdQuery { Id = updateCustomerModel.Id });

                if(customerFromStore==null)
                {
                    return NotFound($"Customer with id {updateCustomerModel.Id} could not be found");
                }

                return await mediator.Send(new UpdateCustomerCommand { Customer = this.mapper.Map(updateCustomerModel,customerFromStore) }); ;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
