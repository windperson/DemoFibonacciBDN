using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;

namespace FibSeqMicroBench;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn(NumeralSystem.Roman)]
public class FibonacciSeqBenchmarks
{
    [ParamsSource(nameof(NthValues))] public int Nth { get; set; }

    public static IEnumerable<int> NthValues => ActualFibonacci.Keys;

    private static int RecursionUpperLimit =>
        int.TryParse(Environment.GetEnvironmentVariable(Const.RecursionUpperLimit), out var limit)
            ? limit
            : Const.RecursionUpperLimitValue;

    [Benchmark(Baseline = true), BenchmarkCategory("simple", "canonical")]
    public BigInteger FibSeqUsingLoop()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingLoop(Nth);
        ValidateCorrectness(Nth, result);
        return result;
    }

    [Benchmark, BenchmarkCategory("simple", "slow")]
    public BigInteger FibSeqUsingRecursion()
    {
        if (Nth > RecursionUpperLimit)
        {
            throw new NotSupportedException($"Recursion will run too long for {Nth}th over {RecursionUpperLimit}th");
        }

        var result = FibonacciCore.SequenceLib.FibonacciUsingRecursion(Nth);
        ValidateCorrectness(Nth, result);
        return result;
    }

    [Benchmark, BenchmarkCategory("math", "approximate")]
    public BigInteger FibSeqUsingGoldenRatio()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingGoldenRatio(Nth);
        ValidateCorrectness(Nth, result);
        return result;
    }

    [Benchmark, BenchmarkCategory("math", "fast")]
    public BigInteger FibSeqUsingMatrixExponentiation()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingMatrixExponentiation(Nth);
        ValidateCorrectness(Nth, result);
        return result;
    }

    [Benchmark, BenchmarkCategory("math", "faster")]
    public BigInteger FibSeqUsingFastDoubling()
    {
        var result = FibonacciCore.SequenceLib.FibonacciUsingFastDoubling(Nth);
        ValidateCorrectness(Nth, result);
        return result;
    }

    #region Check Fibonacci correctness

    // see https://r-knott.surrey.ac.uk/Fibonacci/fibtable.html for precomputed Fibonacci series
    private static IReadOnlyDictionary<int, BigInteger> ActualFibonacci => new Dictionary<int, BigInteger>()
    {
        { 1, 1 },
        { 3, 2 },
        { 5, 5 },
        { 10, 55 },
        { 20, 6765 },
        { 30, 832040 },
        { 35, 9227465 },
        { 40, 102334155 },
        { 45, 1134903170 },
        { 50, 12586269025 },
        { 55, 139583862445 },
        { 60, 1548008755920 },
        { 65, 17167680177565 },
        { 70, 190392490709135 },
        { 71, 308061521170129 },
        { 72, 498454011879264 },
        { 73, 806515533049393 },
        { 74, 1304969544928657 },
        { 75, 2111485077978050 },
        { 80, 23416728348467685 },
        { 85, 259695496911122585 },
        { 90, 2880067194370816120 },
        { 91, 4660046610375530309 },
        { 92, 7540113804746346429 },
        { 93, 12200160415121876738 },
        { 94, BigInteger.Parse("19740274219868223167") },
        { 95, BigInteger.Parse("31940434634990099905") },
        { 96, BigInteger.Parse("51680708854858323072") },
        { 97, BigInteger.Parse("83621143489848422977") },
        { 98, BigInteger.Parse("135301852344706746049") },
        { 99, BigInteger.Parse("218922995834555169026") },
        { 100, BigInteger.Parse("354224848179261915075") },
        { 110, BigInteger.Parse("43566776258854844738105") },
        { 120, BigInteger.Parse("5358359254990966640871840") },
        { 130, BigInteger.Parse("659034621587630041982498215") },
        { 140, BigInteger.Parse("81055900096023504197206408605") },
        { 150, BigInteger.Parse("9969216677189303386214405760200") },
        { 160, BigInteger.Parse("1226132595394188293000174702095995") },
        { 170, BigInteger.Parse("150804340016807970735635273952047185") },
        { 180, BigInteger.Parse("18547707689471986212190138521399707760") },
        { 190, BigInteger.Parse("2281217241465037496128651402858212007295") },
        { 200, BigInteger.Parse("280571172992510140037611932413038677189525") },
        { 210, BigInteger.Parse("34507973060837282187130139035400899082304280") },
        { 220, BigInteger.Parse("4244200115309993198876969489421897548446236915") },
        { 230, BigInteger.Parse("522002106210068326179680117059857997559804836265") },
        { 240, BigInteger.Parse("64202014863723094126901777428873111802307548623680") },
        { 250, BigInteger.Parse("7896325826131730509282738943634332893686268675876375") },
        { 260, BigInteger.Parse("971183874599339129547649988289594072811608739584170445") },
        { 270, BigInteger.Parse("119447720249892581203851665820676436622934188700177088360") },
        { 280, BigInteger.Parse("14691098406862188148944207245954912110548093601382197697835") },
        { 285, BigInteger.Parse("162926777992448823780908130212788963731840407743629812913410") },
        { 290, BigInteger.Parse("1806885656323799249738933639586633513160792578781310139745345") },
        { 295, BigInteger.Parse("20038668997554240570909178165665757608500558774338041350112205") },
        { 300, BigInteger.Parse("222232244629420445529739893461909967206666939096499764990979600") }
    };

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ValidateCorrectness(int Nth, BigInteger result)
    {
        if (ActualFibonacci[Nth] != result)
        {
            throw new ArithmeticException(
                $"Fibonacci calculation failed, actual {Nth}th is '{ActualFibonacci[Nth]}', but calculated is '{result}'");
        }
    }

    #endregion
}