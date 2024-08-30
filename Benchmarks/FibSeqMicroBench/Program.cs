using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
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
                    .WithEnvironmentVariable(new EnvironmentVariable(Const.RecursionUpperLimit,
                        $"{Const.RecursionUpperLimitValue}"))
                    .WithPowerPlan(PowerPlan.UserPowerPlan)
                    .AsDefault());

        bdnConfig.AddExporter(CsvMeasurementsExporter.Default)
                 .AddExporter(RPlotExporter.Default);

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, bdnConfig);
    }
}