using System.Diagnostics;
using System.Numerics;

namespace Problem65
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 input = Int32.Parse(Console.ReadLine());
            List<BigInteger> continuedFraction = ContinuedFractionOfEulersNumeber(input);
            (BigInteger, BigInteger) convergent = Convergent(continuedFraction,input-1);
            Console.WriteLine(SumOfDigits(convergent.Item1));
        }
        static BigInteger SumOfDigits(BigInteger n)
        {
            BigInteger sum = 0;
            while (true)
            {
                sum += n % 10;
                if (n < 10)
                {
                    return sum;
                }
                n /= 10;
            }
        }
        static (BigInteger,BigInteger) Convergent(List<BigInteger> continuedFraction,Int32 indexNumber)
        {
            BigInteger previousNumerator = 1;
            BigInteger previousDeoniminator = 0;
            BigInteger currentNumerator = continuedFraction[0];
            BigInteger currentDenominator = 1;
            for(Int32 i = 1; i <= indexNumber; i++)
            {
                BigInteger nextNumerator = continuedFraction[i] * currentNumerator + previousNumerator;
                BigInteger nextDenominator = continuedFraction[i] * currentDenominator + previousDeoniminator;
                previousNumerator = currentNumerator;
                previousDeoniminator = currentDenominator;
                currentNumerator = nextNumerator;
                currentDenominator = nextDenominator;
            }
            return (currentNumerator, currentDenominator);
        }
        static List<BigInteger> ContinuedFractionOfEulersNumeber(Int32 numberOfTerms)
        {
            List<BigInteger> output = new List<BigInteger>();
            Int32 maxHeight = (numberOfTerms + 3 - (numberOfTerms % 3)) / 3;
            output.Add(2);
            for (Int32 i = 1; i <= maxHeight; i++)
            {
                output.Add(1);
                output.Add(2 * i);
                output.Add(1);
            }
            return output;
        }
    }
}