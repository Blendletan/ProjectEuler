namespace Problem77
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 maxSize = 1000;
            List<Int32> primes = GeneratePrimes(maxSize);
            Int32 primeCount = primes.Count;
            Int64[] primeCoinChange = CoinChange(primes, maxSize);
            Int32 n = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < n; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                Int64 output = primeCoinChange[input];
                Console.WriteLine(output);
            }
        }
        static Int64[] CoinChange(List<Int32> coins, Int32 max)
        {
            Int32 numberOfCoins = coins.Count;
            Int64[] output = new Int64[max + 1];
            output[0] = 1;
            for (Int32 coinIndex = 0; coinIndex < numberOfCoins; coinIndex++)
            {
                Int32 coinValue = coins[coinIndex];
                for (Int32 currentSum = coinValue; currentSum <= max; currentSum++)
                {
                    output[currentSum] += output[currentSum - coinValue];
                }
            }
            return output;
        }
        static List<Int32> GeneratePrimes(Int32 max)
        {
            bool[] isPrime = new bool[max + 1];
            isPrime[0] = false;
            isPrime[1] = false;
            for (Int32 i = 2; i <= max; i++)
            {
                isPrime[i] = true;
            }
            for (Int32 i = 2; i <= max; i++)
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
            for (Int32 i = 0; i <= max; i++)
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