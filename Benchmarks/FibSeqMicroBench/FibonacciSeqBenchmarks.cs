using System;
using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace FibSeqMicroBench;

[SimpleJob(RunStrategy.Throughput, warmupCount: 1)]
public class FibonacciSeqBenchmarks
{
    [Params(1, 3, 5, 10, 20, 30, 35, 40, 45, 50, 53, 55, 60, 65, 70, 73, 75, 80, 85, 90, 91, 92)]
    public int Nth { get; set; }

    // see https://r-knott.surrey.ac.uk/Fibonacci/fibtable.html for precomputed Fibonacci series
    // we only count the first 92 Fibonacci numbers since it is the long type integer limit
    private static IReadOnlyDictionary<int, long> ActualFibonacci => new Dictionary<int, long>()
    {
        { 1, 1L },
        { 3, 2L },
        { 5, 5L },
        { 10, 55L },
        { 20, 6765L },
        { 30, 832040L },
        { 35, 9227465L },
        { 40, 102334155L },
        { 45, 1134903170L },
        { 50, 12586269025L },
        { 53, 53316291173L },
        { 55, 139583862445L },
        { 60, 1548008755920L },
        { 65, 17167680177565L },
        { 70, 190392490709135L },
        { 73, 806515533049393L },
        { 75, 2111485077978050L },
        { 80, 23416728348467685L },
        { 85, 259695496911122585L },
        { 90, 2880067194370816120L },
        { 91, 4660046610375530309L },
        { 92, 7540113804746346429L }
    };

    [Benchmark(Baseline = true), BenchmarkCategory("simple")]
    public long FibSeqUsingLoop()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingLoop(Nth);
        if (ActualFibonacci[Nth] != result)
        {
            throw new Exception("Fibonacci sequence calculate incorrect  value");
        }

        return result;
    }

    [Benchmark, BenchmarkCategory("simple", "slow")]
    public long FibSeqUsingRecursion()
    {
        const int upperLimit = 55;
        if (Nth >= upperLimit)
        {
            throw new NotSupportedException($"Recursion is not supported for {Nth} over {upperLimit}");
        }

        var result = FibonacciCore.SequenceLib.FibonacciUsingRecursion(Nth);
        if (ActualFibonacci[Nth] != result)
        {
            throw new Exception("Fibonacci sequence calculate incorrect  value");
        }

        return result;
    }

    [Benchmark, BenchmarkCategory("math", "approximate")]
    public long FibSeqUsingGoldenRatio()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingGoldenRatio(Nth);
        if (ActualFibonacci[Nth] != result)
        {
            throw new Exception("Fibonacci sequence calculate incorrect  value");
        }

        return result;
    }

    [Benchmark, BenchmarkCategory("math", "fast")]
    public long FibSeqUsingMatrixExponentiation()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingMatrixExponentiation(Nth);
        if (ActualFibonacci[Nth] != result)
        {
            throw new Exception("Fibonacci sequence calculate incorrect  value");
        }

        return result;
    }
}