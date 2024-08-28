using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace FibSeqMicroBench;

public class FibonacciSeqBenchmarks
{
    [Params(1, 3, 5, 10, 20, 30, 35, 40, 45, 50, 53, 55, 60, 65, 70, 75, 80, 85, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100)]
    public int Nth { get; set; }

    // see https://r-knott.surrey.ac.uk/Fibonacci/fibtable.html for precomputed Fibonacci series
    // we only count the first 92 Fibonacci numbers since it is the long type integer limit
    private static IReadOnlyDictionary<int, BigInteger> ActualFibonacci => new Dictionary<int, BigInteger>()
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
        { 75, 2111485077978050L },
        { 80, 23416728348467685L },
        { 85, 259695496911122585L },
        { 90, 2880067194370816120L },
        { 91, 4660046610375530309L },
        { 92, 7540113804746346429L },
        { 93,  12200160415121876738 },
        { 94, BigInteger.Parse( "19740274219868223167") },
        { 95, BigInteger.Parse( "31940434634990099905") },
        { 96, BigInteger.Parse( "51680708854858323072") },
        { 97, BigInteger.Parse( "83621143489848422977") },
        { 98, BigInteger.Parse( "135301852344706746049") },
        { 99, BigInteger.Parse( "218922995834555169026") },
        { 100, BigInteger.Parse( "354224848179261915075") }
    };

    [Benchmark(Baseline = true), BenchmarkCategory("simple", "canonical")]
    public BigInteger FibSeqUsingLoop()
    {
        return FibonacciCore.SequenceLib.FibonacciUsingLoop(Nth);
    }

    [Benchmark, BenchmarkCategory("simple", "slow")]
    public BigInteger FibSeqUsingRecursion()
    {
        const int upperLimit = 60;
        if (Nth >= upperLimit)
        {
            throw new NotSupportedException($"Recursion will run too long for {Nth}th over {upperLimit}th");
        }

        return FibonacciCore.SequenceLib.FibonacciUsingRecursion(Nth);
    }

    [Benchmark, BenchmarkCategory("math", "approximate")]
    public BigInteger FibSeqUsingGoldenRatio()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingGoldenRatio(Nth);
        if (ActualFibonacci[Nth] != result)
        {
            throw new ArithmeticException(
                $"Fibonacci calculation failed, actual {Nth}th is {ActualFibonacci[Nth]}, but calculated is {result}");
        }

        return result;
    }

    [Benchmark, BenchmarkCategory("math", "fast")]
    public BigInteger FibSeqUsingMatrixExponentiation()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingMatrixExponentiation(Nth);
        if (ActualFibonacci[Nth] != result)
        {
            throw new ArithmeticException(
                $"Fibonacci calculation failed, actual {Nth}th is {ActualFibonacci[Nth]}, but calculated is {result}");
        }

        return result;
    }

    [Benchmark, BenchmarkCategory("math", "faster")]
    public BigInteger FibSeqUsingFastDoubling()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingFastDoubling(Nth);
        if (ActualFibonacci[Nth] != result)
        {
            throw new ArithmeticException(
                $"Fibonacci calculation failed, actual {Nth}th is {ActualFibonacci[Nth]}, but calculated is {result}");
        }

        return result;
    }
}