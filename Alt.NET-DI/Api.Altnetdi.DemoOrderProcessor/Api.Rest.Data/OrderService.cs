using System;
using System.Collections.Generic;
using Api.Rest.Domain;
using Api.Rest.Infrastructure;
using Api.Rest.Infrastructure.Logger;
using Api.Rest.Infrastructure.NHibernate;

namespace Api.Rest.Data
{
	public class OrderService : IOrderService
	{
		private readonly ILogger _logger;
		private readonly IAuthenticationService _authenticationService;
		private readonly ISession _session;

		public Guid InstanceId { get; private set; }

		public OrderService(ILogger logger, IAuthenticationService authenticationService, ISession session)
		{
			InstanceId = Guid.NewGuid();
			_logger = logger;
			_authenticationService = authenticationService;
			_session = session;
		}

		public Order GetOrderById(int id)
		{
			return new Order();
		}

		public IEnumerable<Order> GetOrdersToProcess()
		{
			var list = new List<Order>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(new Order());
			}

			return list.ToArray();
		}
	}
}