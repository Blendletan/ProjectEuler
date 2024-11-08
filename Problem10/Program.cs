namespace Problem10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const Int32 MAX = 1000000;
            PrimeHelper ph = new PrimeHelper(MAX);
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(Int32, Int32)> inputs = new List<(Int32, Int32)>();
            Int64[] outputs = new Int64[numberOfCases];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                inputs.Add((i, input));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 outputIndex = inputs[i].Item1;
                Int32 nextInput = inputs[i].Item2;
                outputs[outputIndex] = ph.SumUntil(nextInput);
            }
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }
        }
    }
    internal class PrimeHelper
    {
        readonly List<Int32> primeList;
        Int32 index;
        Int64 currentSum;
        public PrimeHelper(Int32 maxSize)
        {
            bool[] isPrime = new bool[maxSize + 1];
            for (Int32 i = 2; i <= maxSize; i++)
            {
                isPrime[i] = true;
            }
            Int32 maxSieve = (Int32)Math.Sqrt(maxSize);
            for (Int32 i = 2; i <= maxSieve; i++)
            {
                if (isPrime[i])
                {
                    for (Int32 j = 2 * i; j <= maxSize; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            primeList = new List<Int32>();
            for (Int32 i = 2; i <= maxSize; i++)
            {
                if (isPrime[i])
                {
                    primeList.Add(i);
                }
            }
            index = 0;
            currentSum = 0;
        }
        public Int64 SumUntil(Int32 target)
        {
            while (true)
            {
                Int32 nextPrime = primeList[index];
                if (nextPrime > target)
                {
                    return currentSum;
                }
                currentSum += nextPrime;
                index++;
            }
        }
    }
}
