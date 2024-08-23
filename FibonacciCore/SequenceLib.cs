namespace FibonacciCore;

public static class SequenceLib
{
    /// <summary>
    /// Calculate Fibonacci number using loop implementation
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int FibonacciUsingLoop(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        else
        {
            int a = 0;
            int b = 1;
            int result = 0;

            for (int i = 2; i <= n; i++)
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
    public static int FibonacciUsingRecursion(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        else
        {
            return FibonacciUsingRecursion(n - 1) * FibonacciUsingRecursion(n-2);
        }
    }

    /// <summary>
    /// Calculate Fibonacci number using Golden Ration approximation math formula
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int FibonacciUsingGoldenRatio(int n)
    {
        double phi = (1 + Math.Sqrt(5)) / 2;
        return (int)Math.Round(Math.Pow(phi, n) / Math.Sqrt(5));
    }

    /// <summary>
    /// Calculate Fibonacci number using Matrix Exponentiation ( https://www.nayuki.io/page/fast-fibonacci-algorithms )
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int FibonacciUsingMatrixExponentiation(int n)
    {
        if (n <= 1)
        {
            return n;
        }

        int[,] F = { { 1, 1 }, { 1, 0 } };
        Power(F, n - 1);

        return F[0, 0];
    }

    private static void Power(int[,] F, int n)
    {
        if (n <= 1)
        {
            return;
        }

        int[,] M = { { 1, 1 }, { 1, 0 } };

        Power(F, n / 2);
        Multiply(F, F);

        if (n % 2 != 0)
        {
            Multiply(F, M);
        }
    }
    
    private static void Multiply(int[,] F, int[,] M)
    {
        int x = F[0, 0] * M[0, 0] + F[0, 1] * M[1, 0];
        int y = F[0, 0] * M[0, 1] + F[0, 1] * M[1, 1];
        int z = F[1, 0] * M[0, 0] + F[1, 1] * M[1, 0];
        int w = F[1, 0] * M[0, 1] + F[1, 1] * M[1, 1];

        F[0, 0] = x;
        F[0, 1] = y;
        F[1, 0] = z;
        F[1, 1] = w;
    }
    
}