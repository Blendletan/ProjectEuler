using System.Numerics;

namespace Problem48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 N = Int32.Parse(Console.ReadLine());
            BigInteger remainder = 10000000000;
            BigInteger sum = 0;
            for(Int32 i = 1; i <= N; i++)
            {
                sum += BigInteger.ModPow(i, i, remainder);
            }
            sum %= remainder;
            Console.WriteLine(sum);
        }
    }
}
