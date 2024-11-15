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
        readonly bool[] isPrime;
        readonly Int32[] primeList;
        readonly Int64[] consecutivePrimeSum;
        readonly Int32 largestPrime;
        public PrimeHelper(Int64 max)
        {
            Int32 size = 10*(Int32)Math.Sqrt(max);//Experimental tinkering found that this gives us enough primes
            bool[] tempIsPrime = new bool[size + 1];
            for(Int32 i = 2; i <= size; i++)
            {
                tempIsPrime[i] = true;
            }
            List<Int32> primes = new List<Int32>();
            for (Int32 i = 2; i <= (Int32)Math.Sqrt(size); i++)
            {
                if (tempIsPrime[i])
                {
                    for(Int32 j = 2 * i; j <= size; j += i)
                    {
                        tempIsPrime[j] = false;
                    }
                }
            }
            for(Int32 i = 2; i <= size; i++)
            {
                if (tempIsPrime[i])
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
            isPrime = new bool[largestPrime + 1];
            for(Int32 i = 2; i <= largestPrime; i++)
            {
                isPrime[i] = tempIsPrime[i];
            }
        }
        private bool IsPrime(Int64 n)
        {
            if (n <= largestPrime)
            {
                return isPrime[n];
            }
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
        public (Int64,Int32)LargestSumAtIndex(Int32 startIndex,Int64 maxHeight,Int32 sumSizeToBeat)
        {
            Int32 sumSize = primeList.Length-startIndex;
            Int64 sum = consecutivePrimeSum[startIndex];
            for(Int32 i = primeList.Length - 1; i >= startIndex; i--)
            {
                if (sumSize <= sumSizeToBeat)
                {
                    return (0, 0);
                }
                if (sum <= maxHeight)
                {
                    if (IsPrime(sum))
                    {
                        break;
                    }
                }
                sum -= primeList[i];
                sumSize--;
            }
            return (sum, sumSize);
        }
        public string LargestConsecutivePrimeSum(Int64 maxHeight)
        {
            Int64 currentMax = 0;
            Int32 maxSumSize = 0;
            Int32 winningStartIndex = 0;
            for (Int32 i = 0; i < 100; i++) //experimental tinkering found that the winning starting index is always very small, this works
            {
                Int64 minLengthOfNextSum = (maxSumSize + 1) * primeList[i];
                if (minLengthOfNextSum > maxHeight)
                {
                    break;
                }
                (Int64, Int32) nextMax = LargestSumAtIndex(i, maxHeight,maxSumSize);
                if (nextMax.Item2 > maxSumSize)
                {
                    currentMax = nextMax.Item1;
                    maxSumSize = nextMax.Item2;
                    winningStartIndex = i;
                }
            }
            return currentMax.ToString() +" " + maxSumSize.ToString();
        }
    }
}