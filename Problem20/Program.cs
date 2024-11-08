using System.Numerics;

namespace Problem20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int32 maxInput = 0;
            Int32[] inputs = new int[numberOfCases];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > maxInput) { maxInput = nextInput; }
                inputs[i] = nextInput;
            }
            BigInteger[] factorialValues = new BigInteger[maxInput + 1];
            factorialValues[0] = 1;
            for (Int32 i = 1; i <= maxInput; i++)
            {
                factorialValues[i] = i * factorialValues[i - 1];
            }
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = inputs[i];
                Int64 output = SumOfDigits(factorialValues[input]);
                Console.WriteLine(output);
            }
        }
        static Int64 SumOfDigits(BigInteger n)
        {
            Int64 digit = (Int64)(n % 10);
            if (n < 10)
            {
                return digit;
            }
            return digit + SumOfDigits(n / 10);
        }
    }
}