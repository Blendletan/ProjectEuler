using System.Numerics;
namespace Problem66
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 input = Int32.Parse(Console.ReadLine());
            BigInteger currentMax = 0;
            Int32 currentMaxIndex = 0;
            for(Int32 i = 2; i <= input; i++)
            {
                if (IsSquare(i))
                {
                    continue;
                }
                (BigInteger, BigInteger) solution = SolvePellsEquation(i);
                if (solution.Item1 > currentMax)
                {
                    currentMax = solution.Item1;
                    currentMaxIndex = i;
                }
            }
            Console.WriteLine(currentMaxIndex);
        }
        static bool IsSquare(Int32 n)
        {
            Int32 test = (Int32)Math.Sqrt(n);
            if (test * test == n)
            {
                return true;
            }
            return false;
        }
        static (BigInteger,BigInteger) SolvePellsEquation(Int32 n)
        {
            BigInteger previousNumerator = 1;
            BigInteger previousDenominator = 0;
            BigInteger sqrt = new BigInteger(Math.Sqrt(n));
            BigInteger p = 0;
            BigInteger q = 1;
            BigInteger a = (BigInteger)sqrt;
            BigInteger currentNumerator = a;
            BigInteger currendDenominator = 1;
            while (true)
            {
                if (CheckPellSolution(currentNumerator, currendDenominator, n))
                {
                    return (currentNumerator, currendDenominator);
                }
                p = a * q - p;
                q = (n-p*p) / q;
                a = (sqrt + p) / q;
                BigInteger nextNumerator = a * currentNumerator + previousNumerator;
                BigInteger nextDenominator = a * currendDenominator + previousDenominator;
                previousNumerator = currentNumerator;
                previousDenominator = currendDenominator;
                currentNumerator = nextNumerator;
                currendDenominator = nextDenominator;
            }
        }
        static bool CheckPellSolution(BigInteger x, BigInteger y, BigInteger d)
        {
            BigInteger result = x * x - d * y * y;
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}