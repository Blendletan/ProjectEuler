namespace Problem3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int64[] inputs = new long[numberOfCases];
            Int64 maxInput = 0;
            for (int i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = Int64.Parse(Console.ReadLine());
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs[i] = nextInput;
            }
            PrimeHelper pHelper = new PrimeHelper((Int32)Math.Sqrt(maxInput));
            for (int i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(pHelper.LargestFactor(inputs[i]));
            }
        }
    }
    internal class PrimeHelper
    {
        public readonly List<Int64> primeList;
        public PrimeHelper(Int64 maxValue)
        {
            bool[] isPrime = new bool[maxValue + 1];
            isPrime[0] = false;
            isPrime[1] = false;
            for (int i = 2; i <= maxValue; i++)
            {
                isPrime[i] = true;
            }
            Int64 sqrtMax = (Int64)Math.Sqrt(maxValue);
            for (int i = 2; i <= sqrtMax; i++)
            {
                if (isPrime[i])
                {
                    for (int j = 2 * i; j <= maxValue; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            primeList = new List<Int64>();
            for (int i = 0; i <= maxValue; i++)
            {
                if (isPrime[i])
                {
                    primeList.Add(i);
                }
            }
        }
        public Int64 LargestFactor(Int64 input)
        {
            if (input == 1)
            {
                return 1;
            }
            Int64 largestFactorSoFar = 0;
            Int64 sqrtInput = (Int64)Math.Sqrt(input);
            if (sqrtInput * sqrtInput == input)
            {
                return LargestFactor(sqrtInput);
            }
            foreach (var p in primeList)
            {
                if (p > input)
                {
                    break;
                }
                if (input % p == 0)
                {
                    largestFactorSoFar = p;
                    break;
                }
            }
            if (largestFactorSoFar == 0)
            {
                largestFactorSoFar = input;
            }
            return Math.Max(largestFactorSoFar, LargestFactor(input / largestFactorSoFar));
        }
    }
}
