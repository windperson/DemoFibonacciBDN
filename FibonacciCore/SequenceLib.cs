namespace FibonacciCore;

public static class SequenceLib
{
    /// <summary>
    /// Calculate Fibonacci number using loop implementation
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static long FibonacciUsingLoop(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        else
        {
            var a = 0L;
            var b = 1L;
            var result = 0L;

            for (var i = 2; i <= n; i++)
            {
                result = a + b;
                a = b;
                b = result;
            }

            return result;
        }
    }

    /// <summary>
    /// Calculate Fibonacci number using recursion implementation
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static long FibonacciUsingRecursion(int n)
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
    public static long FibonacciUsingGoldenRatio(int n)
    {
        return Convert.ToInt64(Math.Pow(Phi, n) / Math.Sqrt(5));
    }

    /// <summary>
    /// Calculate Fibonacci number using Matrix Exponentiation ( https://www.nayuki.io/page/fast-fibonacci-algorithms )
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static long FibonacciUsingMatrixExponentiation(int n)
    {
        if (n <= 1)
        {
            return n;
        }

        long[,] F = { { 1, 1 }, { 1, 0 } };
        Power(F, n - 1);

        return F[0, 0];
    }

    private static void Power(long[,] F, int n)
    {
        if (n <= 1)
        {
            return;
        }

        long[,] M = { { 1, 1 }, { 1, 0 } };

        Power(F, n / 2);
        Multiply(F, F);

        if (n % 2 != 0)
        {
            Multiply(F, M);
        }
    }

    private static void Multiply(long[,] F, long[,] M)
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
}