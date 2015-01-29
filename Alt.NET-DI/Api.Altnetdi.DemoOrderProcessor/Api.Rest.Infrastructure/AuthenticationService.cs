using System;
using Api.Rest.Infrastructure.Logger;
using Api.Rest.Infrastructure.NHibernate;

namespace Api.Rest.Infrastructure
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly ISession _session;
		private readonly ILogger _logger;

		public Guid InstanceId { get; private set; }

		public AuthenticationService(ISession session, ILogger logger)
		{
			InstanceId = Guid.NewGuid();
			_session = session;
			_logger = logger;
		}
	}
}