namespace Problem47
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 maxSize = Int32.Parse(inputs[0]);
            Int32 numberOfDistinctPrimes = Int32.Parse(inputs[1]);
            Primes p = new Primes(maxSize + numberOfDistinctPrimes);
            for (Int32 i = 0; i <= maxSize; i++)
            {
                if (IsValid(p, numberOfDistinctPrimes, i))
                {
                    Console.WriteLine(i);
                }
            }
        }
        static bool IsValid(Primes p, Int32 numberOfDistinctPrimes, Int32 input)
        {
            for (Int32 i = input; i < input + numberOfDistinctPrimes; i++)
            {
                if (p.distinctPrimeFactors[i] != numberOfDistinctPrimes)
                {
                    return false;
                }
            }
            return true;
        }
    }
    internal class Primes
    {
        readonly Int32 maxSize;
        public readonly bool[] isPrime;
        public readonly Int32[] distinctPrimeFactors;
        public Primes(Int32 N)
        {
            maxSize = N;
            isPrime = new bool[maxSize + 1];
            distinctPrimeFactors = new Int32[maxSize + 1];
            isPrime[0] = false;
            distinctPrimeFactors[0] = 0;
            isPrime[1] = false;
            distinctPrimeFactors[1] = 0;
            for (Int32 i = 2; i <= maxSize; i++)
            {
                isPrime[i] = true;
                distinctPrimeFactors[i] = 0;
            }
            for (Int32 i = 2; i <= maxSize; i++)
            {
                if (isPrime[i])
                {
                    distinctPrimeFactors[i]++;
                    for (Int32 j = 2 * i; j <= maxSize; j += i)
                    {
                        isPrime[j] = false;
                        distinctPrimeFactors[j]++;
                    }
                }
            }
        }
    }
}