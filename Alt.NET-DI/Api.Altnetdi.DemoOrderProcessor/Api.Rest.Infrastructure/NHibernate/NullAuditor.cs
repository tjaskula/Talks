using System;

namespace Api.Rest.Infrastructure.NHibernate
{
	public class NullAuditor : IAuditor
	{
		public Guid InstanceId { get; private set; }

		public NullAuditor()
		{
			InstanceId = Guid.NewGuid();
		}
	}
}