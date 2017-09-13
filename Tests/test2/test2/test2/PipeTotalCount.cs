using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace test2
{
    // a wrapper for an ActionBlock that simply maintains a total count
    public class PipeTotalCount<TInput> : PipeAction<TInput>
    {
        public int TotalCount = 0;

        public override bool IsEqualTo(IPipeDataflow other)
        {
            return (this.GetType() == other.GetType());
        }

        public PipeTotalCount(ExecutionDataflowBlockOptions dataflowBlockOptions)
        {
            _actionBlock = new ActionBlock<TInput>(item =>
            {
                TotalCount++;
            },
            dataflowBlockOptions);
        }
    }
}
