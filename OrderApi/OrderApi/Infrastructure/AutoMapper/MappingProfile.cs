using AutoMapper;
using OrderApi.Domain.Entities;
using OrderApi.Models;

namespace OrderApi.Infrastructure.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderModel, Order>();
        }
    }
}
