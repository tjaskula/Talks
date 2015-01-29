using System;

namespace Api.Rest.Infrastructure.NHibernate
{
	public class Session : ISession
	{
		private readonly IAuditor _auditor;

		public Session()
		{
			InstanceId = Guid.NewGuid();
		}
		
		public Guid InstanceId { get; private set; }

		public Session(IAuditor auditor)
		{
			_auditor = auditor;
		}
	}
}