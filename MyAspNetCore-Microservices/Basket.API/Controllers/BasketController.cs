using System.Net;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repository;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepo _repository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IBasketRepo repo,
        IDiscountService discountService,
        IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repo ?? throw new ArgumentNullException(nameof(repo));
        _discountService = discountService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }
    
    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var test = await _discountService.GetDiscountAsync(userName);
        var basket = await _repository.GetBasket(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        foreach (var item in basket.Items)
        {
            var coupon = await _discountService.GetDiscountAsync(item.ProductName);
            item.Price -= coupon.Amount;
        }
        
        return Ok(await _repository.UpdateBasket(basket).ConfigureAwait(false));
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]        
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await _repository.DeleteBasket(userName);
        return Ok();
    }
    
    
    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout, CancellationToken cancellationToken)
    {
        // get existing basket with total price            
        // Set TotalPrice on basketCheckout eventMessage
        // send checkout event to rabbitmq
        // remove the basket

        // get existing basket with total price
        var basket = await _repository.GetBasket(basketCheckout.UserName);
        if (basket == null)
        {
            return BadRequest();
        }

        // send checkout event to rabbitmq
        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage, cancellationToken);

        // remove the basket
        await _repository.DeleteBasket(basket.UserName);

        return Accepted();
    }
}