using System.Net.Http;
using System.Web.Http;

namespace Api.Rest
{
	public class OrderController : ApiController
	{
		public HttpResponseMessage Get()
		{
			return new HttpResponseMessage
			       	{
			       		Content = new StringContent("Hello ALT.NET")
			       	};
		}
	}
}