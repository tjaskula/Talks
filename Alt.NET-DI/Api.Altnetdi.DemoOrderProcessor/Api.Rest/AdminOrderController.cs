using System.Net.Http;
using System.Web.Http;
using Api.Rest.Domain;

namespace Api.Rest
{
	public class AdminOrderController : ApiController
	{
		private readonly IOrderService _orderService;

		public AdminOrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public HttpResponseMessage Get()
		{
			return new HttpResponseMessage
			{
				Content = new StringContent("Administration")
			};
		}
	}
}