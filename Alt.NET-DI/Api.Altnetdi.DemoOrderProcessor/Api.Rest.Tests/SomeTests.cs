using Api.Rest.Application.OrderProcessing;
using Api.Rest.Application.OrderProcessing.Processors;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;
using Api.Rest.Infrastructure;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Rest.Tests
{
	/// <summary>
	/// Description résumée pour UnitTest1
	/// </summary>
	[TestClass]
	public class SomeTests
	{
        [TestMethod]
        public void Order_Processor_Should_Process_Order1()
        {
            IWindsorContainer container = new WindsorContainer();

            var orderProcessor = new OrderProcessorContainer(container, new UserContext(), new RateExchange(), new AccountsReceivable());

            var order = new Order();

            orderProcessor.Process(order);
        }

        [TestMethod]
        public void Order_Processor_Should_Process_Order2()
        {
            ServiceLocator.Current.Register<IOrderValidator, OrderValidator>();
            var orderProcessor = new OrderProcessorServiceLocator(new UserContext(), new RateExchange(), new AccountsReceivable());

            var order = new Order();

            orderProcessor.Process(order);
        }
	}
}