using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    public abstract class PipeGuaranteedBroadcast<TInput, TOutput> : IPipeTransform<TInput, TOutput>
    {
        public ActionBlock<TInput> _actionBlock;

        public abstract bool IsEqualTo(IPipeDataflow other);

        public List<IPipeSource<TInput>> Sources { get; set; }
        public List<IPipeTarget<TOutput>> Targets { get; set; }

        public PipeGuaranteedBroadcast()
        {
            Sources = new List<IPipeSource<TInput>>();
            Targets = new List<IPipeTarget<TOutput>>();
        }

        Func<TInput, TOutput> _transform;

        public void SetTransform(Func<TInput, TOutput> transform, ExecutionDataflowBlockOptions dataflowBlockOptions)
        {
            _actionBlock = new ActionBlock<TInput>(async item =>
            {
                List<Task<bool>> tasks = new List<Task<bool>>();
                foreach (var target in Targets)
                {
                    TOutput output = _transform(item);
                    tasks.Add(target.SendAsync(output));
                }
                await Task.WhenAll(tasks);
            }, dataflowBlockOptions);

            _transform = transform;
        }


        public virtual void Attach(IPipeTarget<TOutput> target)
        {
            // TODO: weak references???
            Targets.Add(target);
            target.Sources.Add(this);

            _actionBlock.Completion.ContinueWith(t =>
            {
                foreach (var target2 in Targets)
                {
                    if (t.IsFaulted) ((IDataflowBlock)target2).Fault(t.Exception);
                    else target2.Complete();
                }
            });
        }

        DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
        {
            return ((ITargetBlock<TInput>)_actionBlock).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        void IDataflowBlock.Complete()
        {
            _actionBlock.Complete();
        }

        Task IDataflowBlock.Completion
        {
            get { return ((IDataflowBlock)_actionBlock).Completion; }
        }

        void IDataflowBlock.Fault(Exception exception)
        {
            ((IDataflowBlock)_actionBlock).Fault(exception);
        }

        // the functions below should never be called the way this is implemented

        TOutput ISourceBlock<TOutput>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
        {
            throw new Exception("not implemented");
        }

        IDisposable ISourceBlock<TOutput>.LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
        {
            throw new Exception("not implemented");
        }

        void ISourceBlock<TOutput>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            throw new Exception("not implemented");
        }

        bool ISourceBlock<TOutput>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
        {
            throw new Exception("not implemented");
        }

        bool IReceivableSourceBlock<TOutput>.TryReceive(Predicate<TOutput> filter, out TOutput item)
        {
            throw new Exception("not implemented");
        }

        bool IReceivableSourceBlock<TOutput>.TryReceiveAll(out IList<TOutput> items)
        {
            throw new Exception("not implemented");
        }
    }
}
