using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistence;

namespace Order.Application.Features.Orders.Commands;

public class CheckoutOrderCommand : IRequest<Guid>
{
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }

    // BillingAddress
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string AddressLine { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    // Payment
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public int PaymentMethod { get; set; }
}

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Domain.Entities.Order>(request);
        var newOrder = await _orderRepository.AddAsync(orderEntity);
            
        _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

        return newOrder.Id;
    }
}