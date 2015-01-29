using System;

namespace Api.Rest.Infrastructure.NHibernate
{
	public class SessionFactory : ISessionFactory
	{
		public Guid InstanceId { get; private set; }

		public SessionFactory()
		{
			InstanceId = Guid.NewGuid();
		}

		public ISession OpenSession(IAuditor auditor)
		{
			return new Session(auditor);
		}
	}
}