using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace FibSeqMicroBench;

[SimpleJob(RunStrategy.Throughput)]
public class FibonacciSeqBenchmarks
{
    [Params(1, 3, 5, 10, 20, 50)]
    public int Nth { get; set; }
    
    [Benchmark(Baseline = true), BenchmarkCategory("simple")]
    public int FibSeqUsingLoop()
    {
        return FibonacciCore.SequenceLib.FibonacciUsingLoop(Nth);
    }

    [Benchmark, BenchmarkCategory("simple", "slow")]
    public int FibSeqUsingRecursion()
    {
        return FibonacciCore.SequenceLib.FibonacciUsingRecursion(Nth);
    }

    [Benchmark, BenchmarkCategory("math", "approximate")]
    public int FibSeqUsingGoldenRatio()
    {
        return FibonacciCore.SequenceLib.FibonacciUsingGoldenRatio(Nth);
    }

    [Benchmark, BenchmarkCategory("math", "fast")]
    public int FibSeqUsingMatrixExponentiation()
    {
        return FibonacciCore.SequenceLib.FibonacciUsingMatrixExponentiation(Nth);
    }
}