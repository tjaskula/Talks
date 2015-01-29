using System;
using Api.Rest.Domain;

namespace Api.Rest.Application.OrderProcessing
{
	public class AccountsReceivable : IAccountsReceivable
	{
		public Guid InstanceId { get; private set; }

		public AccountsReceivable()
		{
			InstanceId = Guid.NewGuid();
		}

		public void Collect(User user, Price price)
		{
		}
	}
}