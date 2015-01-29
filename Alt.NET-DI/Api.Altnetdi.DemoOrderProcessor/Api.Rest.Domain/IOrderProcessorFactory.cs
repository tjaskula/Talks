using Api.Rest.Domain.Processors;

namespace Api.Rest.Domain
{
	public interface IOrderProcessorFactory
	{
		IOrderProcessor InitializeNewOrderProcessor();
		void ReleaseProcessor(IOrderProcessor orderProcessor);
	}
}