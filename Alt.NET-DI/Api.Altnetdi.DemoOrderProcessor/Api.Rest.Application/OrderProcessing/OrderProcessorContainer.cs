using System;
using System.Threading;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;
using Api.Rest.Infrastructure.Logger;
using Api.Rest.Infrastructure.Messages;
using Castle.Windsor;

namespace Api.Rest.Application.OrderProcessing
{
	//public class OrderProcessorContainer : IOrderProcessor
	//{
	//    private readonly IWindsorContainer _container;
	//    public Guid InstanceId { get; private set; }

	//    public OrderProcessorContainer(IWindsorContainer container)
	//    {
	//        _container = container;
	//        InstanceId = Guid.NewGuid();
	//    }

	//    public SuccessResult Process(Order order)
	//    {
	//        var validator = _container.Resolve<IOrderValidator>();
	//        var shipper = _container.Resolve<IOrderShipper>();

	//        bool isValid = validator.Validate(order);
	//        if (isValid)
	//        {
	//            Collect(order);
	//            shipper.Ship(order);
	//        }

	//        return CreateStatus(isValid);
	//    }

	//    private void Collect(Order order)
	//    {
	//        //User user = _userContext.GetCurrentUser();
	//        //Price price = order.GetPrice(_exchange, _userContext);
	//        //_receivable.Collect(user, price);
	//    }

	//    private SuccessResult CreateStatus(bool isValid)
	//    {
	//        return isValid ? SuccessResult.Success : SuccessResult.Failed;
	//    }
	//}
}