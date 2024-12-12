using System.Numerics;

namespace Problem92
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfDigits = Int32.Parse(Console.ReadLine());
            BigInteger output = Solve(numberOfDigits);
            Console.WriteLine(output % 1000000007);
        }
        static BigInteger Solve(Int32 maxNumberOfDigits)
        {
            Int32[] digitSquares = [1, 4, 9, 16, 25, 36, 49, 64, 81];
            Int32 maxDigitSum = 81 * maxNumberOfDigits;
            BigInteger[] solution = new BigInteger[maxDigitSum + 1];
            solution[0] = 1;
            solution[1] = 0;
            for(Int32 currenNumberOfDigits = 1; currenNumberOfDigits <= maxNumberOfDigits; currenNumberOfDigits++)
            {
                for (Int32 currentDigitSum = 81*currenNumberOfDigits; currentDigitSum > 0; currentDigitSum--)
                {
                    foreach (var digit in digitSquares)
                    {
                        if (digit > currentDigitSum)
                        {
                            break;
                        }
                        solution[currentDigitSum] += solution[currentDigitSum - digit];
                    }
                }
            }
            BigInteger sum = 0;
            for(Int32 i = 1; i <= maxDigitSum; i++)
            {
                if (IsEightyNine(i))
                {
                    sum += solution[i];
                }
            }
            return sum;
        }
        static bool IsEightyNine(Int32 n)
        {
            while (true)
            {
                if (SquareDigitSum(n) == 89)
                {
                    return true;
                }
                if (SquareDigitSum(n) == 1)
                {
                    return false;
                }
                n = SquareDigitSum(n);
            }
        }
        static Int32 SquareDigitSum(Int32 n)
        {
            Int32 sum = 0;
            while (true)
            {
                Int32 nextDigit = (n % 10);
                sum += nextDigit*nextDigit;
                if (n < 10)
                {
                    return sum;
                }
                n = n / 10;
            }
        }
    }
}
