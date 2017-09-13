using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    // an abstract wrapper for a BufferBlock
    public abstract class PipeBuffer<TOutput> : IPipeSource<TOutput>, IPipeDataflow
    {
        protected BufferBlock<TOutput> _bufferBlock;

        public PipeBuffer(ExecutionDataflowBlockOptions dataflowBlockOptions = null)
        {
            if (dataflowBlockOptions == null)
                dataflowBlockOptions = new ExecutionDataflowBlockOptions { BoundedCapacity = 1 };

            Targets = new List<IPipeTarget<TOutput>>();
            _bufferBlock = new BufferBlock<TOutput>(dataflowBlockOptions);
        }

        public abstract bool IsEqualTo(IPipeDataflow other);

        public async Task SendAsync(TOutput item)
        {
            await _bufferBlock.SendAsync(item);
        }

        public void Attach(IPipeTarget<TOutput> target)
        {
            // TODO: weak references???
            Targets.Add(target);
            target.Sources.Add(this);

            _bufferBlock.LinkTo(target);
            _bufferBlock.Completion.ContinueWith(t =>
            {
                if (t.IsFaulted) ((IDataflowBlock)target).Fault(t.Exception);
                else target.Complete();
            });
        }

        public List<IPipeTarget<TOutput>> Targets { get; set; }

        TOutput ISourceBlock<TOutput>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
        {
            return ((ISourceBlock<TOutput>)_bufferBlock).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        IDisposable ISourceBlock<TOutput>.LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
        {
            return ((ISourceBlock<TOutput>)_bufferBlock).LinkTo(target, linkOptions);
        }

        void ISourceBlock<TOutput>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            ((ISourceBlock<TOutput>)_bufferBlock).ReleaseReservation(messageHeader, target);
        }

        bool ISourceBlock<TOutput>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            return ((ISourceBlock<TOutput>)_bufferBlock).ReserveMessage(messageHeader, target);
        }

        public void Complete()
        {
            _bufferBlock.Complete();
        }

        public Task Completion
        {
            get { return _bufferBlock.Completion; }
        }

        public void Fault(Exception exception)
        {
            ((IDataflowBlock)_bufferBlock).Fault(exception);
        }
    }
}
