using System.Numerics;
namespace Problem97
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int64 output = 0;
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                string[] nextLine = Console.ReadLine().Split();
                Int64 a = Int64.Parse(nextLine[0]);
                Int64 b = Int64.Parse(nextLine[1]);
                Int64 c = Int64.Parse(nextLine[2]);
                Int64 d = Int64.Parse(nextLine[3]);
                output += LastTwelveDigits(a, b, c, d);
                output = output % 1000000000000;
            }
            string unpaddedOutput = output.ToString();
            if (unpaddedOutput.Length < 12)
            {
                Console.WriteLine(unpaddedOutput.PadLeft(12, '0'));
            }
            else
            {
                Console.WriteLine(unpaddedOutput);
            }
        }
        static Int64 LastTwelveDigits(Int64 a, Int64 b, Int64 c, Int64 d)
        {
            BigInteger modulus = 1000000000000;
            BigInteger output = BigInteger.ModPow(b, c, modulus);
            output *= a;
            output += d;
            output = output % modulus;
            return (Int64)output;
        }
    }
}
