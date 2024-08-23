# Fibonacci Sequence generate algorithm Benchmark Demo

This repository demo using BenchmarkDotNet to benchmark various Fibonacci Sequence generate algorithm,

## Micro benchmark
  
To run micro benchmark(s), on root project folder, run:

```powershell
dotnet run -c Release --project .\Benchmarks\FibSeqMicroBench\FibSeqMicroBench.csproj -- -f '*'
```

you can see available micro benchmarks by using postfix `-- --list flat` command line argument.