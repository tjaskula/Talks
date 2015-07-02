using System;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Application.OrderProcessing
{
	public class OrderProcessor : IOrderProcessor
	{
		private readonly IOrderValidator _validator;
		private readonly IOrderShipper _orderShipper;
		private readonly IOrderCollector _collector;
		private readonly MessageDispatcher _messageDispatcher;

		public Guid InstanceId { get; private set; }

		public OrderProcessor(IOrderValidator validator,
							  IOrderShipper orderShipper,
							  IOrderCollector collector,
							  MessageDispatcher messageDispatcher)
		{
			InstanceId = Guid.NewGuid();

			_validator = validator;
			_orderShipper = orderShipper;
			_collector = collector;
			_messageDispatcher = messageDispatcher;
		}

		public SuccessResult Process(Order order)
		{
			bool isValid = _validator.Validate(order);
			if (isValid)
			{
				_collector.Collect(order);
				_orderShipper.Ship(order);

				var shippingMessage = new ShippedToCustomerMessage {CutomerId = order.CustomerId, OrderId = order.Id, Content = string.Format("Shipped order {0} to Customer {1}", order.Id, order.CustomerId)};
				var orderMessage = new OrderValidatedForCustomerMessage {CutomerId = order.CustomerId, OrderId = order.Id, Content = string.Format("Validated order {0} for Customer {1}", order.Id, order.CustomerId)};
				_messageDispatcher.Handle(shippingMessage);
				_messageDispatcher.Handle(orderMessage);
			}

			return CreateStatus(isValid);
		}

		private SuccessResult CreateStatus(bool isValid)
		{
			return isValid ? SuccessResult.Success : SuccessResult.Failed;
		}
	}
}