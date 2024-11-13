using System.Numerics;

namespace Problem56
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 N = Int32.Parse(Console.ReadLine());
            Int64 max = 0;
            for(Int32 a = 1; a <= N; a++)
            {
                for(Int32 b = 1; b <= N; b++)
                {
                    BigInteger pow = BigInteger.Pow(a, b);
                    Int64 digitSum = SumOfDigits(pow);
                    if (digitSum > max)
                    {
                        max = digitSum;
                    }
                }
            }
            Console.WriteLine(max);
        }
        static Int64 SumOfDigits(BigInteger n)
        {
            Int64 sum = 0;
            while (true)
            {
                Int32 nextDigit = (Int32)(n % 10);
                sum += nextDigit;
                if (n < 10)
                {
                    return sum;
                }
                n = n / 10;
            }
        }
    }
}
