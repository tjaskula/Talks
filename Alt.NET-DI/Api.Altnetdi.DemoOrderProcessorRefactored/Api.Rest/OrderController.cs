using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;
using Api.Rest.Infrastructure.Logger;

namespace Api.Rest
{
	public class OrderController : ApiController
	{
		private readonly IOrderService _orderService;
		private readonly IOrderProcessorFactory _orderProcessorFactory;
		private readonly ILogger _logger;

		public Guid InstanceId { get; private set; }

		public OrderController(IOrderService orderService, IOrderProcessorFactory orderProcessorFactory, ILogger logger)
		{
			InstanceId = Guid.NewGuid();
			_orderService = orderService;
			_orderProcessorFactory = orderProcessorFactory;
			_logger = logger;
		}

		public HttpResponseMessage Get()
		{
			var stopwatch = new Stopwatch();
			var stringBuilder = new StringBuilder();

			IEnumerable<Order> orders = _orderService.GetOrdersToProcess();

			int i = 1;
			foreach (var order in orders)
			{
				stopwatch.Start();

				var orderProcessor = _orderProcessorFactory.InitializeNewOrderProcessor();

				SuccessResult successResult = orderProcessor.Process(order);
				if (successResult == SuccessResult.Success)
				{
					RecordSuccess(order);
				}
				else
					ReportFailure(order);

				// Verify outcome    
				stopwatch.Stop();
				stringBuilder.AppendLine(string.Format("Order n° {0} : Processing time - {1}", i, stopwatch.Elapsed));
				stopwatch.Reset();
				i++;
			}

			return new HttpResponseMessage
			       	{
			       		Content = new StringContent(stringBuilder.ToString())
			       	};
		}

		private static void ReportFailure(Order order)
		{
		}

		private static void RecordSuccess(Order order)
		{
		}
	}
}