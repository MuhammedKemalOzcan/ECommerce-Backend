using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Dtos.PaymentDto;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Orders;
using ECommerceAPI.Domain.Enums;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.Iyzicocallback
{
    public class IyzicoCallbackCommandHandler : IRequestHandler<IyzicoCallbackCommand, CallbackResultDto>
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _uow;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICartRepository _cartRepository;

        public IyzicoCallbackCommandHandler(IPaymentService paymentService, IUnitOfWork uow, IProductRepository productRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository, ICartRepository cartRepository)
        {
            _paymentService = paymentService;
            _uow = uow;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _cartRepository = cartRepository;
        }

        public async Task<CallbackResultDto> Handle(IyzicoCallbackCommand request, CancellationToken cancellationToken)
        {
            var paymentResult = await _paymentService.ProcessCallbackAsync(request.Token);

            if (!paymentResult.IsSuccess)
            {
                return new CallbackResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = paymentResult.ErrorMessage
                };
            }

            var order = await _orderRepository.GetByTokenAsync(request.Token);

            if (order == null) throw new NotFoundException("Order cannot found");

            var customer = await _customerRepository.GetById(order.CustomerId.Value);

            if (customer == null) throw new NotFoundException("Customer cannot found");

            var cart = await _cartRepository.GetActiveCartAsync(customer.AppUserId, null);

            if (cart == null) throw new NotFoundException("Cart cannot found");

            if (order.PaymentStatus == PaymentStatus.Success)
            {
                return new CallbackResultDto { IsSuccess = true, OrderCode = order.OrderCode };
            }

            var paymentInfo = PaymentInfo.Create(
                paymentResult.PaymentId,
                "CREDIT_CARD",
                1,
                paymentResult.CardAssociation,
                paymentResult.CardFamily,
                paymentResult.CardLastFourDigits,
                $"{customer.FirstName} {customer.LastName}"
                );

            order.SetPaymentSuccess(paymentInfo);
            cart?.ClearCart();

            foreach (var orderItem in order.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                if (product != null)
                {
                    product.DecreaseStock(orderItem.Quantity);
                    _productRepository.Update(product);
                }
            }

            await _uow.SaveChangesAsync(cancellationToken);
            return new CallbackResultDto
            {
                IsSuccess = true,
                OrderCode = order.OrderCode
            };
        }
    }
}