using ECommerceAPI.Application.Dtos.Customer;
using ECommerceAPI.Application.Dtos.PaymentDto;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.CreateOrder
{
    public record InitializePaymentCommand(decimal ShippingCost, LocationDto ShippingAddress, LocationDto BillingAddress, decimal price, int installment) : IRequest<string>;
}

