namespace Problem46
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Int32> primes = GenerateOddPrimes(1000000);
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                Int64 output = SatisfiesGoldbach(input, primes);
                Console.WriteLine(output);
            }
        }
        static Int64 SatisfiesGoldbach(Int32 n, List<Int32> primes)
        {
            Int64 output = 0;
            foreach (Int32 p in primes)
            {
                if (p > n)
                {
                    return output;
                }
                if (IsValid(n, p)) { output++; }
            }
            return output;
        }
        static bool IsValid(Int32 n, Int32 p)
        {
            Int32 x = n - p;
            if (x % 2 == 1) { return false; }
            x = x / 2;
            Int32 y = (Int32)Math.Sqrt(x);
            if (y * y == x)
            {
                return true;
            }
            return false;
        }
        static List<Int32> GenerateOddPrimes(Int32 max)
        {
            bool[] isPrime = new bool[max + 1];
            isPrime[0] = false;
            isPrime[1] = false;
            for (Int32 i = 0; i <= max; i++)
            {
                isPrime[i] = true;
            }
            Int32 maxHeight = (Int32)Math.Sqrt(max);
            for (Int32 i = 2; i <= maxHeight; i++)
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
            for (Int32 i = 3; i <= max; i++)
            {
                if (isPrime[i]) { output.Add(i); }
            }
            return output;
        }
    }
}
