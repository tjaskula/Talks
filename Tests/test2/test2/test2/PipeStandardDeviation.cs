using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    // a wrapper for a TransformBlock that output the StdDev of the input (over a period)
    public class PipeStandardDeviation<T> : PipeTransform<T, T>
    {
        HistoryBuffer<T> _historyBuffer;
        public int Period { get { return _historyBuffer.Capacity; } }

        public PipeStandardDeviation(int period, ExecutionDataflowBlockOptions dataflowBlockOptions)
        {
            _historyBuffer = new HistoryBuffer<T>(period);
            SetTransform(value =>
            {
                if (value == null)
                    return default(T);

                _historyBuffer.Add(value);

                dynamic sum = _historyBuffer.Sum();

                if (sum == null)
                    return default(T);

                return (T)((dynamic)Math.Sqrt((double)sum));
            },
            dataflowBlockOptions);
        }

        public override bool IsEqualTo(IPipeDataflow other)
        {
            if (this.GetType() != other.GetType())
                return false;
            if (this.Period != ((PipeMovingAverage<T>)other).Period)
                return false;

            return true;
        }
    }
}
