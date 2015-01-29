using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Api.Rest
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = new HttpSelfHostConfiguration("http://localhost:6666");
			config.Routes.MapHttpRoute("defualt", "{controller}/{id}", new {id = RouteParameter.Optional});
			var server = new HttpSelfHostServer(config);
			server.OpenAsync().Wait();

			Console.WriteLine("Server is running and listening...");
			Console.ReadKey();
		}
	}
}