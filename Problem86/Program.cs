using System.Numerics;
namespace Problem86
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int64[] inputs = new long[numberOfCases];
            Int64 maxInput = 0;
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = Int64.Parse(Console.ReadLine());
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs[i] = nextInput;
            }
            var result = Solve(maxInput);
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = inputs[i];
                Console.WriteLine(result[nextInput]);
            }
        }
        static Int64 Combinations(Int64 a, Int64 b)
        {
            if (2 * a < b)
            {
                return 0;
            }
            if (a >= b)
            {
                return b / 2;
            }
            return a - (b - 1) / 2;
        }
        static Int64[] Solve(Int64 max)
        {
            var result = new Int64[max + 1];
            Int64 maxHeight = (Int64)Math.Sqrt(3*max);
            for (Int64 m = 1; m <= maxHeight; m++)
            {
                for (Int64 n = 1; n < m; n++)
                {
                    Int64 a = 2 * m * n;
                    Int64 b = m * m - n * n;
                    if (BigInteger.GreatestCommonDivisor(a, b) != 1)
                    {
                        continue;
                    }
                    if (a % 2 == b % 2)
                    {
                        continue;
                    }
                    for (Int64 k = 1; k * a <= max; k++)
                    {
                        result[k * a] += Combinations(k * a, k * b);
                    }
                    for (Int64 k = 1; k * b <= max; k++)
                    {
                        result[k * b] += Combinations(k * b, k * a);
                    }
                }
            }
            var output = new Int64[max + 1];
            for (Int32 i = 1; i <= max; i++)
            {
                output[i] += output[i - 1];
                output[i] += result[i];
            }
            return output;
        }
    }
}
