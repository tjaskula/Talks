namespace Api.Rest.Infrastructure.NHibernate
{
	public interface ISessionFactory
	{
		ISession OpenSession(IAuditor auditor);
	}
}