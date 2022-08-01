using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;

public class DiscountService : IDiscountService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

    public DiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient ?? throw new ArgumentNullException(nameof(discountProtoServiceClient));
    }

    public async Task<CouponModel> GetDiscountAsync(string produceName)
    {
        var discountRequest = new GetDiscountRequest
        {
            ProductName = produceName
        };

        var result = await _discountProtoServiceClient.GetDiscountAsync(discountRequest);

        return result;
    }
}