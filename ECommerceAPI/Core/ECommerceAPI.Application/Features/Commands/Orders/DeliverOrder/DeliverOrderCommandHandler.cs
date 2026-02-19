using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Exceptions;
using ECommerceAPI.Domain.Repositories;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.DeliverOrder
{
    internal class DeliverOrderCommandHandler : IRequestHandler<DeliverOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public DeliverOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public async Task Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(new OrderId(request.OrderId));

            if (order == null) throw new NotFoundException("Order cannot be found");

            order.DeliverOrder();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}