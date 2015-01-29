using System;

namespace Api.Rest.Infrastructure.NHibernate
{
	public class Auditor : IAuditor
	{
		public Guid InstanceId { get; private set; }

		public Auditor()
		{
			InstanceId = Guid.NewGuid();
		}
	}
}