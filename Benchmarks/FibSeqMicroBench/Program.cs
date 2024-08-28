using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace FibSeqMicroBench;

class Program
{
    static void Main(string[] args)
    {
        var bdnConfig = ManualConfig.Create(DefaultConfig.Instance);
        bdnConfig.AddJob(
            Job.ShortRun.WithStrategy(RunStrategy.Throughput)
                .WithWarmupCount(1)
                .WithIterationCount(5)
                .WithPowerPlan(PowerPlan.UserPowerPlan));

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, bdnConfig);
    }
}