using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Domain.Entities;
using OrderApi.Models;
using OrderApi.Service.v1.Command;
using OrderApi.Service.v1.Query;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetPaidOrders()
        {

            try
            {
                return  await mediator.Send(new GetPaidOrdersQuery());
                
            }
            catch (Exception)
            {

                return BadRequest("Error retrieving orders");
            }
            
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderModel order)
        {

            try
            {
                return await mediator.Send(new CreateOrderCommand { Order=mapper.Map<Order>(order) });

            }
            catch (Exception e)
            {

                return BadRequest($"Error creating order: {e}");
            }

        }

        [HttpPut]
        public async Task<ActionResult<Order>> PayOrder(Guid id)
        {
            try
            {
                var orderFromStore = await mediator.Send(new GetOrderByIdQuery { Id = id });

                if (orderFromStore == null)
                {
                    return NotFound($"order with id {id} could not be found");
                }

                return await mediator.Send(new PayOrderCommand{ Order=mapper.Map<Order>(orderFromStore) }); ;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }


    }
}
