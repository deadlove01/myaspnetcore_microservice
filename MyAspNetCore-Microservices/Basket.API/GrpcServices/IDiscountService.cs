using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;

public interface IDiscountService
{
    public Task<CouponModel> GetDiscountAsync(string produceName);
}