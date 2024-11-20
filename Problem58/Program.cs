using System.Numerics;

namespace Problem58
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int64 N = Int64.Parse(Console.ReadLine());
            PrimeSpiral pSpiral = new PrimeSpiral(N);
            Console.WriteLine(pSpiral.result);
        }
    }
    internal class PrimeSpiral
    {
        public readonly Int64 result;
        public PrimeSpiral(Int64 N)
        {
            Int64 primeCount=0;
            for (Int64 currentSquareSize = 3; currentSquareSize <= Int64.MaxValue; currentSquareSize += 2)
            {
                Int64 bottomRight = currentSquareSize * currentSquareSize;
                Int64 gapSize = currentSquareSize - 1;
                Int64 totalElements = (currentSquareSize - 1) * 2 + 1;
                Int64 bottomLeft = bottomRight - gapSize;
                if (IsPrime(bottomLeft))
                {
                    primeCount++;
                }
                Int64 topLeft = bottomLeft - gapSize;
                if (IsPrime(topLeft))
                {
                    primeCount++;
                }
                Int64 topRight = topLeft - gapSize;
                if (IsPrime(topRight))
                {
                    primeCount++;
                }
                if (100*primeCount < totalElements * N)
                {
                    result = currentSquareSize;
                    return;
                }
            }
        }
        private static bool IsPrime(Int64 input)
        {
            List<Int64> millerRabinBases = new List<Int64> { 2, 3, 5, 7, 11, 13, 17 };
            if (millerRabinBases.Contains(input))
            {
                return true;
            }
            Int64 s = 0;
            Int64 d = input-1;
            while (true)
            {
                if (d % 2 == 1)
                {
                    break;
                }
                d /= 2;
                s++;
            }
            foreach (var i in millerRabinBases)
            {
                Int64 x = (Int64)BigInteger.ModPow(i, d, input);
                Int64 y=1;
                for (Int32 j = 0; j < s; j++)
                {
                    y = (Int64)BigInteger.ModPow(x, 2, input);
                    if (y==1 && x!= 1&& x != input - 1) 
                    {
                        return false;
                    }
                    x = y;
                }
                if (y != 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}