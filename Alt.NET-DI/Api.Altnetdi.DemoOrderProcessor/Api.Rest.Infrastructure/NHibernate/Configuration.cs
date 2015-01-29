namespace Api.Rest.Infrastructure.NHibernate
{
	public  class Configuration
	{
		public ISessionFactory BuildSessionFactory()
		{
			return new SessionFactory();
		}
	}
}