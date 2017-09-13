using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace test2
{
    // The beginning of a framework that wraps TPL Dataflow Blocks into easier to piece
    // together chunks. Most of my naming for my objects replaces Block with
    // Pipe (e.g. TransformBlock / PipeTransform)
    //
    // This is also a test of an architecture design that would easily allow multiple pipelines
    // to branch off at various points. And, it should be easier to piece together the plumbing as
    // Attach will handle both the LinkTo and Complete/Completion connections.
    //
    // Since BroadcastBlock does not "Guarantee" delivery... I have written a wrapper called
    // PipeGuaranteedBroad.
    //
    // In PipeMovingAverage I can either inherit from PipeTransform or PipeGuaranteedBroadcast...
    // this gives me the ability to test my new class "PipeGuaranteedBroadcast" and compare it with
    // the built in "PipeTransform".
    //
    // Currently running this program and clicking MultiTarget500 it takes about 10 seconds.
    // Note: even though this is labeled MultiTarget500... it current just has a single target as
    // I'm trying to get a sense of performance between PipeGuaranteedBroadcast and TransformBlock.
    //
    public partial class Form1 : Form
    {
        TaskScheduler _uiScheduler;
        public Form1()
        {
            InitializeComponent();
            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        int _count = 100000; // on my machine 100000 takes about 10 seconds, 10000 takes about 1 second

        private void button_SingleTarget_Click(object sender, EventArgs e)
        {
            _pipelineTask = Task.Factory.StartNew(() =>
            {
                ProcessPipelineTest(_count, 500, false);
            });
        }

        private void button_MultiTarget_Click(object sender, EventArgs e)
        {
            _pipelineTask = Task.Factory.StartNew(() =>
            {
                ProcessPipelineTest(_count, 1, true);
            });
        }

        private void button_MultiTarget500_Click(object sender, EventArgs e)
        {
            _pipelineTask = Task.Factory.StartNew(() =>
            {
                ProcessPipelineTest(_count, 500, true);
            });            
        }

        Task _pipelineTask;

        public async void ProcessPipelineTest(int count, int capacity, bool includeGuaranteedBroadcast)
        {
            await Task.Factory.StartNew(() => {
                richTextBox1.Text = "";
            }, CancellationToken.None, TaskCreationOptions.None, _uiScheduler);

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var cancellationSource = new CancellationTokenSource();

            var dataflowBlockOptions = new ExecutionDataflowBlockOptions
            {
                BoundedCapacity = capacity,
                CancellationToken = cancellationSource.Token
            };

            // Setup Pipes
            var source = new PipeGenerateNumbers<double?>(count, dataflowBlockOptions);
            var movingAverage = new PipeMovingAverage<double?>(100, dataflowBlockOptions);
            var standardDeviation = new PipeStandardDeviation<double?>(100, dataflowBlockOptions);
            var output = new PipeTotalCount<double?>(dataflowBlockOptions);

            source.Attach(movingAverage);
            movingAverage.Attach(standardDeviation);
            standardDeviation.Attach(output);

            source.Execute(count);

//            output.Completion.Wait();

            await output.Completion;

            sw.Stop();

            await Task.Factory.StartNew(() => {
                richTextBox1.Text += "Duration=" + sw.Elapsed + "\n";
                richTextBox1.Text += "TotalCount=" + output.TotalCount + "\n";
            }, CancellationToken.None, TaskCreationOptions.None, _uiScheduler);
        }
    }
}
