using System;
using Api.Rest.Infrastructure.NHibernate;

namespace Api.Rest.Infrastructure.Logger
{
	public class Logger : ILogger
	{
		private readonly ISession _session;
		public Guid InstanceId { get; private set; }

		public Logger(ISession session)
		{
			InstanceId = Guid.NewGuid();
			_session = session;
		}

		public void LogInfo(string message)
		{
		}
	}
}