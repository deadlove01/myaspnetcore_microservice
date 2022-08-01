using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Shared.Entities;

namespace Discount.Grpc.Mappers;

public class DiscountMapper : Profile
{

    public DiscountMapper()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}