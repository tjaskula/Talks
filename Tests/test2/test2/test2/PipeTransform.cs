using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    public abstract class PipeTransform<TInput, TOutput> : IPipeTransform<TInput, TOutput>
    {
        protected TransformBlock<TInput, TOutput> _transformBlock;

        public abstract bool IsEqualTo(IPipeDataflow other);

        public List<IPipeSource<TInput>> Sources { get; set; }
        public List<IPipeTarget<TOutput>> Targets { get; set; }

        public PipeTransform() 
        {
            Sources = new List<IPipeSource<TInput>>();
            Targets = new List<IPipeTarget<TOutput>>();
        }

        public void SetTransform(Func<TInput, TOutput> transform, ExecutionDataflowBlockOptions dataflowBlockOptions) 
        {
            _transformBlock = new TransformBlock<TInput, TOutput>(transform, dataflowBlockOptions);
        }

        public virtual void Attach(IPipeTarget<TOutput> target)
        {
            // TODO: weak references???
            Targets.Add(target);
            target.Sources.Add(this);

            _transformBlock.LinkTo(target);
            _transformBlock.Completion.ContinueWith(t =>
            {
                if (t.IsFaulted) ((IDataflowBlock)target).Fault(t.Exception);
                else target.Complete();
            });
        }

        DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
        {
            return ((ITargetBlock<TInput>)_transformBlock).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        void IDataflowBlock.Complete()
        {
            _transformBlock.Complete();
        }

        Task IDataflowBlock.Completion
        {
            get { return ((IDataflowBlock)_transformBlock).Completion; }
        }

        void IDataflowBlock.Fault(Exception exception)
        {
            ((IDataflowBlock)_transformBlock).Fault(exception);
        }

        TOutput ISourceBlock<TOutput>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
        {
            return ((ISourceBlock<TOutput>)_transformBlock).ConsumeMessage(messageHeader, target, out messageConsumed);
        }

        IDisposable ISourceBlock<TOutput>.LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
        {
            return ((ISourceBlock<TOutput>)_transformBlock).LinkTo(target, linkOptions);
        }

        void ISourceBlock<TOutput>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            ((ISourceBlock<TOutput>)_transformBlock).ReleaseReservation(messageHeader, target);
        }

        bool ISourceBlock<TOutput>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            return ((ISourceBlock<TOutput>)_transformBlock).ReserveMessage(messageHeader, target);
        }

        bool IReceivableSourceBlock<TOutput>.TryReceive(Predicate<TOutput> filter, out TOutput item)
        {
            return ((IReceivableSourceBlock<TOutput>)_transformBlock).TryReceive(filter, out item);
        }

        bool IReceivableSourceBlock<TOutput>.TryReceiveAll(out IList<TOutput> items)
        {
            return ((IReceivableSourceBlock<TOutput>)_transformBlock).TryReceiveAll(out items);
        }
    }
}
