using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    public interface IPipeTransform<TInput, TOutput> : IPipeTarget<TInput>, IPipeSource<TOutput>, IPipePropagator<TInput, TOutput>, IPipeReceivableSource<TOutput>, IPipeDataflow
    {
        void Attach(IPipeTarget<TOutput> target);
    }

    public interface IPipeTarget<TOutput> : ITargetBlock<TOutput>
    {
        List<IPipeSource<TOutput>> Sources { get; set; }
    }
    public interface IPipeSource<TInput> : ISourceBlock<TInput>
    {
        List<IPipeTarget<TInput>> Targets { get; set; }
    }
    public interface IPipePropagator<TInput, TOutput> : IPropagatorBlock<TInput, TOutput> {}    
    public interface IPipeReceivableSource<TOutput> : IReceivableSourceBlock<TOutput> {}
    
    public interface IPipeDataflow : IDataflowBlock 
    {
        bool IsEqualTo(IPipeDataflow other); 
    }

}
