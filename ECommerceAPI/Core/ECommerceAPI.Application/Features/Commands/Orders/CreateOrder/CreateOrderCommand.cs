using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Dtos.PaymentDto;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.CreateOrder
{
    public record CreateOrderCommand(decimal ShippingCost, LocationDto ShippingAddress, LocationDto BillingAddress, PaymentCardDto PaymentInfo, decimal price, int installment) : IRequest<string>;
}
