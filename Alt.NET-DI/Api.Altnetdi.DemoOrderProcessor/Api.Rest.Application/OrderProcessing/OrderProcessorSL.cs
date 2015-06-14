using System;
using System.Threading;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;
using Api.Rest.Infrastructure;
using Api.Rest.Infrastructure.Logger;
using Api.Rest.Infrastructure.Messages;
using Castle.Windsor;

namespace Api.Rest.Application.OrderProcessing
{
    public class OrderProcessorSL : IOrderProcessor
    {
        private readonly IUserContext _userContext;
        private readonly IRateExchange _rateExchange;
        private readonly IAccountsReceivable _accountsReceivable;
        public Guid InstanceId { get; private set; }

        public OrderProcessorSL(IUserContext userContext, IRateExchange rateExchange, IAccountsReceivable accountsReceivable)
        {
            _userContext = userContext;
            _rateExchange = rateExchange;
            _accountsReceivable = accountsReceivable;

            InstanceId = Guid.NewGuid();
        }

        public SuccessResult Process(Order order)
        {
            var validator = ServiceLocator.Current.GetService<IOrderValidator>();
            var shipper = ServiceLocator.Current.GetService<IOrderShipper>();

            bool isValid = validator.Validate(order);
            if (isValid)
            {
                Collect(order);
                shipper.Ship(order);
            }

            return CreateStatus(isValid);
        }

        private void Collect(Order order)
        {
            User user = _userContext.GetCurrentUser();
            Price price = order.GetPrice(_rateExchange, _userContext);
            _accountsReceivable.Collect(user, price);
        }

        private SuccessResult CreateStatus(bool isValid)
        {
            return isValid ? SuccessResult.Success : SuccessResult.Failed;
        }
    }
}