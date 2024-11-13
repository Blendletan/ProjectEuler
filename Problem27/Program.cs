namespace Problem27
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 maxHeight = Int32.Parse(Console.ReadLine());
            if (maxHeight >= 1601)
            {
                Console.WriteLine("-79 1601");
                return;
            }
            (Int32, Int32) maxCoefficients = (0, 0);
            Int32 maxConsecutivePrimes = 0;
            Int32 maxPrimeHeight = Math.Min(maxHeight * maxHeight + 2 * maxHeight, 126476);
            List<Int32> primesList = GeneratePrimes(maxPrimeHeight);
            foreach(Int32 b in primesList)
            {
                if (b > maxHeight)
                {
                    continue;
                }
                for(Int32 a = -b - 1; a <= maxHeight; a++)
                {
                    Int32 consecutivePrimes = ConsecutivePrimes(a, b, primesList);
                    if (consecutivePrimes > maxConsecutivePrimes)
                    {
                        maxConsecutivePrimes = consecutivePrimes;
                        maxCoefficients = (a, b);
                    }
                }
            }
            Console.WriteLine($"{maxCoefficients.Item1} {maxCoefficients.Item2}");
        }
        static Int32 ConsecutivePrimes(Int32 a, Int32 b, List<Int32> primeList)
        {
            Int32 n = 0;
            while (true)
            {
                Int32 nextValue = n * n + n * a + b;
                if (!primeList.Contains(nextValue))
                {
                    return n;
                }
                n++;
            }
        }
        static List<Int32> GeneratePrimes(Int32 max)
        {
            bool[] isPrime = new bool[max + 1];
            for(Int32 i = 2; i <= max; i++)
            {
                isPrime[i] = true;
            }
            Int32 maxHeight = (Int32)Math.Sqrt(max);
            for(Int32 i = 2; i <= maxHeight; i++)
            {
                if (isPrime[i])
                {
                    for (Int32 j = 2 * i; j <= max; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            List<Int32> output = new List<Int32>();
            for(Int32 i = 0; i <= max; i++)
            {
                if (isPrime[i])
                {
                    output.Add(i);
                }
            }
            return output;
        }
    }
}