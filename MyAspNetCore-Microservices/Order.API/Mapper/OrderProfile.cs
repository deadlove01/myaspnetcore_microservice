using AutoMapper;
using EventBus.Messages.Events;
using Order.Application.Features.Orders.Commands;

namespace Order.API.Mapper;

public class OrderingProfile : Profile
{
    public OrderingProfile()
    {
        CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
    }
}