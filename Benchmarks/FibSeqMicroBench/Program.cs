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
        var bdnConfig =
            DefaultConfig.Instance.AddJob(
                Job.ShortRun.WithStrategy(RunStrategy.Throughput)
                    .WithIterationCount(5)
                    .WithEnvironmentVariable(new EnvironmentVariable("RecursLimit", "50"))
                    .WithPowerPlan(PowerPlan.UserPowerPlan)
                    .AsDefault());

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, bdnConfig);
    }
}