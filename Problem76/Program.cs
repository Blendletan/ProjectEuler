namespace Problem76
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const Int64 remainder = 1000000007;
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int32[] inputs = new int[numberOfCases];
            Int32 maxInput = 0;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs[i] = nextInput;
            }
            Int32[] coinValues = new Int32[maxInput];
            for(Int32 i = 0; i < maxInput; i++)
            {
                coinValues[i] = i + 1;
            }
            Int64[] results = CoinChange(coinValues, maxInput, remainder);
            foreach(var v in inputs)
            {
                Console.WriteLine(results[v]-1);
            }
        }
        static Int64[] CoinChange(Int32[] coinValues,Int32 maxSize,Int64 remainder)
        {
            Int64[] output = new Int64[maxSize + 1];
            output[0] = 1;
            foreach (var coinValue in coinValues)
            {
                for(Int32 i = coinValue; i <= maxSize; i++)
                {
                    output[i] += output[i - coinValue];
                    output[i] = output[i] % remainder;
                }
            }
            return output;
        }
    }
}