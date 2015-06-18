using System;
using System.Threading;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;
using Api.Rest.Infrastructure.Logger;
using Api.Rest.Infrastructure.Messages;

namespace Api.Rest.Application.OrderProcessing.Processors
{
	public class OrderProcessor : IOrderProcessor
	{
		private readonly IOrderValidator _validator;
		private readonly IAccountsReceivable _receivable;
		private readonly IRateExchange _exchange;
		private readonly IUserContext _userContext;
		private readonly ILogger _logger;
		private readonly IOrderShipper _orderShipper;
		private readonly MessageDispatcher _messageDispatcher;

		public Guid InstanceId { get; private set; }

		public OrderProcessor(IOrderValidator validator,
							  IAccountsReceivable receivable,
							  IRateExchange exchange, 
							  IUserContext userContext,
							  ILogger logger,
							  IOrderShipper orderShipper,
							  MessageDispatcher messageDispatcher)
		{
			Thread.Sleep(TimeSpan.FromMilliseconds(1500));
			InstanceId = Guid.NewGuid();

			_validator = validator;
			_receivable = receivable;
			_exchange = exchange;
			_userContext = userContext;
			_logger = logger;
			_orderShipper = orderShipper;
			_messageDispatcher = messageDispatcher;
		}

		public SuccessResult Process(Order order)
		{
			bool isValid = _validator.Validate(order);
			if (isValid)
			{
				Collect(order);
				_orderShipper.Ship(order);

				var shippingMessage = new ShippedToCustomerMessage {CutomerId = order.CustomerId, OrderId = order.Id, Content = string.Format("Shipped order {0} to Customer {1}", order.Id, order.CustomerId)};
				var orderMessage = new OrderValidatedForCustomerMessage {CutomerId = order.CustomerId, OrderId = order.Id, Content = string.Format("Validated order {0} for Customer {1}", order.Id, order.CustomerId)};
				_messageDispatcher.Handle(shippingMessage);
				_messageDispatcher.Handle(orderMessage);
			}

			return CreateStatus(isValid);
		}

		private void Collect(Order order)
		{
			User user = _userContext.GetCurrentUser();
			Price price = order.GetPrice(_exchange, _userContext);
			_receivable.Collect(user, price);
		}

		private SuccessResult CreateStatus(bool isValid)
		{
			return isValid ? SuccessResult.Success : SuccessResult.Failed;
		}
	}
}