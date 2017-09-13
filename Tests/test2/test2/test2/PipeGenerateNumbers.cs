using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    // a wrapper for a BufferBlock that includes source data (a simple number generator)
    public class PipeGenerateNumbers<TOutput> : PipeBuffer<TOutput>
    {
        public PipeGenerateNumbers(int n, ExecutionDataflowBlockOptions dataflowBlockOptions)
        {
            _bufferBlock = new BufferBlock<TOutput>(dataflowBlockOptions);
        }

        public override bool IsEqualTo(IPipeDataflow other)
        {
            return (this.GetType() == other.GetType());
        }
        public async void Execute(int n)
        {
            for (int i = 0; i < n; i++)
                await SendAsync((TOutput)((dynamic)i));
            Complete();
        }
    }
}
