using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.ShipOrder
{
    internal class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public ShipOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public async Task Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId));

            if (order == null) throw new NotFoundException("Order cannot be found");

            order.ShipOrder();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}