using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId));

            if (order == null) throw new NotFoundException("Order cannot be found.");

            order.CancelOrder();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}