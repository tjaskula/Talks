using System;
using Api.Rest.Domain;
using Api.Rest.Domain.Processors;

namespace Api.Rest.Application.OrderProcessing
{
	public class UserContext : IUserContext
	{
		public Guid InstanceId { get; private set; }

		public UserContext()
		{
			InstanceId = Guid.NewGuid();
		}

		public Currency GetSelectedCurrency(User currentUser)
		{
			return Currency.ILS;
		}

		public User GetCurrentUser()
		{
			return new User();
		}
	}
}