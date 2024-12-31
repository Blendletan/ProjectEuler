namespace Problem95
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 maxCachedSumOfDivisors = 1000000;
            Int32 input = Int32.Parse(Console.ReadLine());
            HashSet<Int32> foundPoints = new HashSet<Int32>();
            var sigma = new SumOfDivisors(maxCachedSumOfDivisors);
            Int32 largestChainLength = 1;
            Int32 smallestMemberOfWinningChain = 6;
            for (Int32 i = 6; i <= input; i++)
            {
                if (foundPoints.Contains(i))
                {
                    continue;
                }
                List<Int32> nextChain = GetChain(i, sigma, input, foundPoints);
                List<Int32> strippedChain = StripChain(nextChain);
                Int32 nextValue = i;
                Int32 currentChainLength = strippedChain.Count;
                Int32 minOfCurrentChain = strippedChain.Min();
                if (currentChainLength > largestChainLength)
                {
                    smallestMemberOfWinningChain = minOfCurrentChain;
                    largestChainLength = currentChainLength;
                }
                foundPoints.Add(i);
            }
            Console.WriteLine(smallestMemberOfWinningChain);
        }
        static List<Int32> StripChain(List<Int32> chain)
        {
            List<Int32> output = new List<Int32>(chain);
            Int32 repeatedValue = chain.Last();
            Int32 startIndex = chain.IndexOf(repeatedValue);
            output.RemoveRange(0, startIndex + 1);
            return output;
        }
        static List<Int32> GetChain(Int32 n, SumOfDivisors sigma, Int32 max, HashSet<Int32> found)
        {
            Int32 nextValue = n;
            List<Int32> output = new List<Int32>();
            List<Int32> defualt = new List<Int32> { 6, 6 };
            while (output.Contains(nextValue) == false)
            {
                if (nextValue == 1)
                {
                    return defualt;
                }
                if (nextValue > max)
                {
                    return defualt;
                }
                output.Add(nextValue);
                found.Add(nextValue);
                nextValue = sigma.Evaluate(nextValue);
            }
            output.Add(nextValue);
            return output;
        }
    }
    class SumOfDivisors
    {
        readonly Int32[] sigma;
        Int32 size;
        public SumOfDivisors(Int32 max)
        {
            size = max;
            sigma = new Int32[max + 1];
            for (Int32 divisor = 1; divisor <= max; divisor++)
            {
                for (Int32 composite = 2 * divisor; composite <= max; composite += divisor)
                {
                    sigma[composite] += divisor;
                }
            }
        }
        public Int32 Evaluate(Int32 n)
        {
            return sigma[n];
        }
    }
}