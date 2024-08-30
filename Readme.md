# Fibonacci Sequence generate algorithm Benchmark Demo

This repository demo using BenchmarkDotNet to benchmark various Fibonacci Sequence generate algorithm,

## Micro benchmark
  
To quickly run all micro benchmark(s), on root project folder, run:

```powershell
dotnet run -c Release --project ./Benchmarks/FibSeqMicroBench/FibSeqMicroBench.csproj -- --warmupcount 1 --runOncePerIteration --filter '*'
```

you can see available micro benchmarks by using postfix `-- --list flat` command line argument.

By default, the recursion version of benchmark will only run up to 50th Fibonacci number (Because of its slowness), If you want to try out more recursion version benchmark, run with the additional argument `--envVar RecursLimit:[The_upper_limit_you_desired]` appended at end.