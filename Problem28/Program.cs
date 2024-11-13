using System.Numerics;

namespace Problem28
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                BigInteger input = BigInteger.Parse(Console.ReadLine());
                BigInteger sum1 = (input * (input + 1) * (2 * input + 1)) / 3;
                BigInteger sum2 = ((input - 1) * (input - 1)) / 2;
                BigInteger output = sum1 - sum2 - 1;
                if (input == 1) { output = 1; }
                Console.WriteLine(output % 1000000007);
            }
        }
    }
}
