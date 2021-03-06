﻿using Atomiv.Core.Application;
using Atomiv.Template.Core.Application.Commands.Orders;
using Atomiv.Template.Core.Domain.Orders;
using System.Threading.Tasks;

namespace Atomiv.Template.Core.Application.Commands.Handlers.Orders
{
    public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, SubmitOrderCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public SubmitOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<SubmitOrderCommandResponse> HandleAsync(SubmitOrderCommand request)
        {
            var orderId = new OrderIdentity(request.Id);

            var order = await _orderRepository.FindAsync(orderId);

            if(order == null)
            {
                throw new ExistenceException();
            }

            order.Submit();

            await _orderRepository.UpdateAsync(order);
            return _mapper.Map<Order, SubmitOrderCommandResponse>(order);
        }
    }
}