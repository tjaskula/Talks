using System;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;
using Castle.Windsor;

namespace Api.Rest.Application.OrderProcessing.Processors
{
    public class OrderProcessorContainer : IOrderProcessor
    {
        private readonly IUserContext _userContext;
        private readonly IRateExchange _rateExchange;
        private readonly IAccountsReceivable _accountsReceivable;
        private readonly IWindsorContainer _container;
        public Guid InstanceId { get; private set; }

        public OrderProcessorContainer(IWindsorContainer container, IUserContext userContext, IRateExchange rateExchange, IAccountsReceivable accountsReceivable)
        {
            _userContext = userContext;
            _rateExchange = rateExchange;
            _accountsReceivable = accountsReceivable;

            _container = container;
            InstanceId = Guid.NewGuid();
        }

        public SuccessResult Process(Order order)
        {
            var validator = _container.Resolve<IOrderValidator>();
            var shipper = _container.Resolve<IOrderShipper>();

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