using System.Numerics;

namespace FibonacciCore;

public static class SequenceLib
{
    /// <summary>
    /// Calculate Fibonacci number using loop implementation
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static BigInteger FibonacciUsingLoop(int n)
    {
        if (n <= 1)
        {
            return n;
        }

        var a = new BigInteger(0);
        var b = new BigInteger(1);
        var result = new BigInteger(0);

        for (var i = 2; i <= n; i++)
        {
            result = a + b;
            a = b;
            b = result;
        }

        return result;
    }

    /// <summary>
    /// Calculate Fibonacci number using recursion implementation
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static BigInteger FibonacciUsingRecursion(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        else
        {
            return FibonacciUsingRecursion(n - 1) + FibonacciUsingRecursion(n - 2);
        }
    }

    private static readonly double Phi = (1 + Math.Sqrt(5)) / 2;

    /// <summary>
    /// Calculate Fibonacci number using Golden Ration approximation math formula
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static BigInteger FibonacciUsingGoldenRatio(int n)
    {
        return new BigInteger(Math.Round(Math.Pow(Phi, n) / Math.Sqrt(5)));
    }

    /// <summary>
    /// Calculate Fibonacci number using Matrix Exponentiation ( https://www.nayuki.io/page/fast-fibonacci-algorithms )
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static BigInteger FibonacciUsingMatrixExponentiation(int n)
    {
        if (n <= 1)
        {
            return n;
        }

        BigInteger[,] F = { { 1, 1 }, { 1, 0 } };
        Power(F, n - 1);

        return F[0, 0];
    }

    private static void Power(BigInteger[,] F, int n)
    {
        if (n <= 1)
        {
            return;
        }

        BigInteger[,] M = { { 1, 1 }, { 1, 0 } };

        Power(F, n / 2);
        Multiply(F, F);

        if (n % 2 != 0)
        {
            Multiply(F, M);
        }
    }

    private static void Multiply(BigInteger[,] F, BigInteger[,] M)
    {
        var x = F[0, 0] * M[0, 0] + F[0, 1] * M[1, 0];
        var y = F[0, 0] * M[0, 1] + F[0, 1] * M[1, 1];
        var z = F[1, 0] * M[0, 0] + F[1, 1] * M[1, 0];
        var w = F[1, 0] * M[0, 1] + F[1, 1] * M[1, 1];

        F[0, 0] = x;
        F[0, 1] = y;
        F[1, 0] = z;
        F[1, 1] = w;
    }

    /// <summary>
    /// Calculate Fibonacci number using Fast Doubling ( https://www.nayuki.io/page/fast-fibonacci-algorithms )
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static BigInteger FibonacciUsingFastDoubling(int n)
    {
        var a = BigInteger.Zero;
        var b = BigInteger.One;
        for (var i = 31; i >= 0; i--)
        {
            var d = a * (b * 2 - a);
            var e = a * a + b * b;
            a = d;
            b = e;
            if ((((uint)n >> i) & 1) != 0)
            {
                var c = a + b;
                a = b;
                b = c;
            }
        }

        return a;
    }
}