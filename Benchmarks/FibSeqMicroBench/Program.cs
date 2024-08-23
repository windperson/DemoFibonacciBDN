using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace FibSeqMicroBench;

class Program
{
    static void Main(string[] args)
    {
        var bdnConfig = DefaultConfig.Instance;
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, bdnConfig);
    }
}