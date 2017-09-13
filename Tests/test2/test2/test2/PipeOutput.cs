using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    // an abstract wrapper for an ActionBlock
    public abstract class PipeAction<TInput> : IPipeTarget<TInput>, IPipeDataflow
    {
        protected ActionBlock<TInput> _actionBlock;

        public List<IPipeSource<TInput>> Sources { get; set; }

        public PipeAction() 
        {
            Sources = new List<IPipeSource<TInput>>();
        }

        public abstract bool IsEqualTo(IPipeDataflow other);

        DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
        {
            return ((ITargetBlock<TInput>)_actionBlock).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        public void Complete()
        {
            _actionBlock.Complete();
        }

        public Task Completion
        {
            get { return _actionBlock.Completion; }
        }

        public void Fault(Exception exception)
        {
            ((IDataflowBlock)_actionBlock).Fault(exception);
        }
    }
}
