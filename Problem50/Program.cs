namespace Problem50
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int64[] inputs = new Int64[numberOfCases];
            string[] outputs = new string[numberOfCases];
            Int64 maxInput = 0;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = Int64.Parse(Console.ReadLine());
                inputs[i] = nextInput;
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
            }
            PrimeHelper ph = new PrimeHelper(maxInput);
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = inputs[i];
                string mytest = ph.LargestConsecutivePrimeSum(nextInput);
                outputs[i] = mytest;
            }
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }
        }
    }
    internal class PrimeHelper
    {
        readonly Int32[] primeList;
        readonly Int64[] consecutivePrimeSum;
        readonly Int32 largestPrime;
        public PrimeHelper(Int64 max)
        {
            //we need to find a list of primes large enough that the sum of them is greater than max
            Int32 size = 10*(Int32)Math.Sqrt(max);
            //Experimental tinkering found that this gives us enough primes
            bool[] isPrime = new bool[size + 1];
            for(Int32 i = 2; i <= size; i++)
            {
                isPrime[i] = true;
            }
            List<Int32> primes = new List<Int32>();
            for (Int32 i = 2; i <= (Int32)Math.Sqrt(size); i++)
            {
                if (isPrime[i])
                {
                    for(Int32 j = 2 * i; j <= size; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            for(Int32 i = 2; i <= size; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }
            primeList = primes.ToArray();
            consecutivePrimeSum = new Int64[primeList.Length];
            for(Int32 i = 0; i < primeList.Length; i++)
            {
                consecutivePrimeSum[0] += primeList[i];
            }
            for(Int32 i = 1; i < primeList.Length; i++)
            {
                consecutivePrimeSum[i] = consecutivePrimeSum[i - 1] - primeList[i - 1];
            }
            largestPrime = primes.Last();
        }
        private bool IsPrime(Int64 n)
        {
            foreach (var p in primeList)
            {
                if (p*p > n)
                {
                    break;
                }
                if (n % p == 0)
                {
                    return false;
                }
            }
            for(Int32 i = largestPrime; i <= (Int64)Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private (Int64,Int32)LargestSumAtIndex(Int32 startIndex,Int64 maxHeight)
        {
            Int32 sumLength = primeList.Length-startIndex;
            Int64 sum = consecutivePrimeSum[startIndex];
            for(Int32 i = primeList.Length - 1; i >= startIndex; i--)
            {
                if (sum <= maxHeight)
                {
                    if (IsPrime(sum))
                    {
                        break;
                    }
                }
                sum -= primeList[i];
                sumLength--;
            }
            return (sum, sumLength);
        }
        public string LargestConsecutivePrimeSum(Int64 maxHeight)
        {
            (Int64, Int32) nextMax = LargestSumAtIndex(0, maxHeight);
            Int64 currentMax = nextMax.Item1;
            Int32 maxSumSize = nextMax.Item2;
            Int32 winningStartIndex = 0;
            Int32 maxIndexToSearchUntil = LargestPrimeIndexToSearchUntil(maxHeight, maxSumSize);
            for (Int32 i = 1; i <= maxIndexToSearchUntil; i++)
            {
                Int64 minLengthOfNextSum = (maxSumSize + 1) * primeList[i];
                if (minLengthOfNextSum > maxHeight)
                {
                    break;
                }
                nextMax = LargestSumAtIndex(i, maxHeight);
                if (nextMax.Item2 > maxSumSize)
                {
                    currentMax = nextMax.Item1;
                    maxSumSize = nextMax.Item2;
                    winningStartIndex = i;
                }
            }
            return currentMax.ToString() +" " + maxSumSize.ToString();
        }
        private Int32 LargestPrimeIndexToSearchUntil(Int64 maxHeight,Int32 sumLengthOfTwo)
        {
            for(Int32 i = 1; i < primeList.Length; i++)
            {
                Int64 sum = 0;
                for(Int32 j = 0; j <= sumLengthOfTwo; j++)
                {
                    sum += primeList[i + j];
                }
                if (sum > maxHeight)
                {
                    return i - 1;
                }
            }
            return primeList.Length - 1;
        }
    }
}