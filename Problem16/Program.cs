using System.Numerics;

namespace Problem16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                BigInteger power = BigInteger.Pow(2, input);
                Console.WriteLine(SumOfDigts(power));
            }
        }
        static Int64 SumOfDigts(BigInteger n)
        {
            Int64 output = (Int64)(n % 10);
            if (n > 9)
            {
                return output + SumOfDigts(n / 10);
            }
            return output;
        }
    }
}
