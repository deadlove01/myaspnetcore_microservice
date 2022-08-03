using AutoMapper;
using Order.Application.Features.Orders.Commands;
using Order.Application.Features.Orders.Queries;

namespace Order.Application.Mappings;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        
        CreateMap<Domain.Entities.Order, OrdersVm>().ReverseMap();
        CreateMap<Domain.Entities.Order, CheckoutOrderCommand>().ReverseMap();
        CreateMap<Domain.Entities.Order, UpdateOrderCommand>().ReverseMap();
    }
}