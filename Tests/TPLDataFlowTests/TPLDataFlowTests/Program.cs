using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
//using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;

namespace TPLDataFlowTests
{
    public interface IEventHandler<in TEvent> where TEvent : Event
    {
        void Handle(TEvent @event);
    }

    public class SubEvent1Handler : IEventHandler<SubEvent1>
    {
        private int _id;
        private int _timeout;

        public SubEvent1Handler(int id, int timout)
        {
            _id = id;
            _timeout = timout;
        }

        public void Handle(SubEvent1 @event)
        {
            //if (@event.I > 5) throw new Exception("Baddddd");
            Thread.Sleep(_timeout);
            Trace.TraceInformation($"Handler {_id}. Handling event : {@event.I}. Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public class SubEvent2Handler : IEventHandler<SubEvent2>
    {
        private int _id;
        private int _timeout;

        public SubEvent2Handler(int id, int tiemout)
        {
            _id = id;
            _timeout = tiemout;
        }

        public void Handle(SubEvent2 @event)
        {
            Thread.Sleep(_timeout);
            Trace.TraceInformation($"Handler {_id}. Handling event : {@event.Z}. Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public class Event
    {
    }

    public class SubEvent1 : Event
    {
        public SubEvent1(int i)
        {
            I = i;
        }

        public int I;
    }

    public class SubEvent2 : Event
    {
        public SubEvent2(double z)
        {
            Z = z;
        }

        public double Z;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var intActionTargets = new List<ITargetBlock<int>>();
            var doubleActionTargets = new List<ITargetBlock<double>>();
            var intBufferTargets = new List<ISourceBlock<int>>();
            var doubleBufferTargets = new List<ISourceBlock<double>>();

            for (var i = 1; i <= 3; i++)
            {
                var temp = i;
                var buffer = CreateBuffer<int>();
                intActionTargets.Add(CreateAction(buffer, async item =>
                                {
                                    await Task.Delay(temp * 500);
                                    Trace.TraceInformation($"Target: {temp} | Type: {typeof(int).Name} | Thread: {Thread.CurrentThread.ManagedThreadId} | Message: {item}");
                                    //if (item >= 5) throw new Exception($"Something bad happened in action {temp}");
                                }));
                intBufferTargets.Add(buffer);
            }

            for (var i = 1; i <= 2; i++)
            {
                var temp = i;
                var buffer = CreateBuffer<double>();
                doubleBufferTargets.Add(buffer);
                doubleActionTargets.Add(CreateAction(buffer, async item =>
                                {
                                    await Task.Delay(100);
                                    Trace.TraceInformation($"Target: {temp} | Type: {typeof(double).Name} | Thread: {Thread.CurrentThread.ManagedThreadId} | Message: {item}");
                                }));
            }

            var intBroadcaster = CreateBroadcaster(intBufferTargets);
            var doubleBroadcaster = CreateBroadcaster(doubleBufferTargets);

            for (var i = 1; i <= 10; i++)
            {
                intBroadcaster.SendAsync(i).Wait();
                //doubleBroadcaster.SendAsync(i).Wait();
            }

            Thread.Sleep(10000);

            var intCompletions = intBufferTargets.Select(t => t.Completion);
            var doubleCompletions = doubleBufferTargets.Select(t => t.Completion);

            intBroadcaster.Complete();
            doubleBroadcaster.Complete();
            intBufferTargets.ForEach(t => t.Complete());
            doubleBufferTargets.ForEach(t => t.Complete());

            var unprocessedMessages = intBufferTargets.SelectMany(t =>
            {
                IList<int> unprocessed;
                ((IReceivableSourceBlock<int>)t).TryReceiveAll(out unprocessed);
                return unprocessed ?? new List<int>();
            });

            foreach (var unprocessedMessage in unprocessedMessages)
            {
                Trace.TraceInformation($"Unprocessed message : {unprocessedMessage}");
            }

            Task.WhenAll(intBroadcaster.Completion, doubleBroadcaster.Completion).Wait();
            Task.WhenAll(doubleCompletions).Wait();
            Task.WhenAll(intCompletions).Wait();

            Trace.TraceInformation("Finishing the whole pipline");


            var eqDispatcher = new EventQueueDispatcher();
            eqDispatcher.Subscribe(new SubEvent1Handler(1, 100));
            eqDispatcher.Subscribe(new SubEvent2Handler(1, 200));

            for (int i = 1; i <= 10; i++)
            {
                eqDispatcher.Publish(new SubEvent1(i));
                eqDispatcher.Publish(new SubEvent2(i));
            }

            Trace.TraceInformation("Finished publishing");

            ////////////////
            /// RX       ///
            ////////////////

            //var ob = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10).Select<long, Event>(i =>
            //{
            //    if (i % 2 == 0)
            //        return new SubEvent1((int) i);

            //    return new SubEvent2(i);
            //});

            //IConnectableObservable<Event> producerAbstraction = ob.Publish();

            //var obs1 = new SubEvent1Handler(1, 1000);
            //var obs2 = new SubEvent1Handler(2, 1000);
            //var obs3 = new SubEvent2Handler(3, 1);
            ////ob.OfType<SubEvent1>().SubscribeOn(Scheduler.Default).Subscribe(e => obs1.Handle(e));
            ////ob.OfType<SubEvent1>().SubscribeOn(Scheduler.Default).Subscribe(e => obs2.Handle(e));
            ////ob.OfType<SubEvent2>().SubscribeOn(Scheduler.Default).Subscribe(e => obs3.Handle(e));

            //producerAbstraction.Connect();



            ////// Membus
            ///// 

            //var bus = BusSetup.StartWith<Conservative>()
            //    .Apply<FlexibleSubscribePolicy>(a => a.RegisterMethods("Handle"))
            //    .Construct();

            //bus.Subscribe(new SubEvent1Handler(1, 10000));
            //bus.Subscribe(new SubEvent2Handler(2, 100));

            //var tasks = new List<Task>();

            //for (int i = 0; i < 10; i++)
            //{
            //    try
            //    {
            //        tasks.Add(bus.PublishAsync(new SubEvent1(i)));
            //        tasks.Add(bus.PublishAsync(new SubEvent2(i)));
            //    }
            //    catch
            //    { }
            //}

            //try
            //{
            //    Task.WhenAll(tasks).Wait();
            //}
            //catch
            //{ }

            Console.ReadLine();
        }

        //private static void CompleteWhenAll(IDataflowBlock target, params IDataflowBlock[] sources)
        //{
        //    if (target == null) return;
        //    if (sources.Length == 0) { target.Complete(); return; }
        //    Task.Factory.ContinueWhenAll(
        //        sources.Select(b => b.Completion).ToArray(),
        //        tasks => {
        //            var exceptions = (from t in tasks where t.IsFaulted select t.Exception).ToList();
        //            if (exceptions.Count != 0)
        //            {
        //                target.Fault(new AggregateException(exceptions));
        //            }
        //            else
        //            {
        //                target.Complete();
        //            }
        //        }
        //    );
        //}

        private static ActionBlock<T> CreateBroadcaster<T>(IEnumerable<ISourceBlock<T>> targets)
        {
            var broadcaster = new ActionBlock<T>(
                async item =>
                {
                    var processingTasks = targets.Select(async t => await ((ITargetBlock<T>)t).SendAsync(item));
                    await Task.WhenAll(processingTasks);                      
                });

            return broadcaster;
        }

        private static ISourceBlock<T> CreateBuffer<T>()
        {
            return new BufferBlock<T>(new DataflowBlockOptions {BoundedCapacity = 5});
        }

        private static ITargetBlock<T> CreateAction<T>(ISourceBlock<T> source, Func<T, Task> handler)
        {
            var a = new ActionBlock<T>(handler, new ExecutionDataflowBlockOptions { BoundedCapacity = 1 });
            source.LinkTo(a, new DataflowLinkOptions { PropagateCompletion = true });
            return a;
        }
    }
}