namespace Problem72
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(Int32, Int32)> inputs = new List<(Int32, Int32)>();
            Int64[] outputs = new Int64[numberOfCases];
            Int32 maxInput = 0;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs.Add((i,nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            GCDHelper helper = new GCDHelper(maxInput);
            Int32 currentIndex = 2;
            Int64 currentSum = 0;
            foreach(var v in inputs)
            {
                Int32 nextInput = v.Item2;
                Int32 outputLocation = v.Item1;
                while (true)
                {
                    if (currentIndex > nextInput)
                    {
                        break;
                    }
                    currentSum += helper.eulerTotient[currentIndex];
                    currentIndex++;
                }
                outputs[outputLocation] = currentSum;
            }
            foreach (var v in outputs)
            {
                Console.WriteLine(v);
            }
        }
    }
    internal class GCDHelper
    {
        public readonly Int32[] eulerTotient;
        public GCDHelper(Int32 max)
        {
            bool[] isPrime = new bool[max + 1];
            eulerTotient = new int[max + 1];
            eulerTotient[1] = 1;
            for(Int32 i = 2; i <= max; i++)
            {
                isPrime[i] = true;
                eulerTotient[i] = i;
            }
            for(Int32 p = 2; p <= max; p++)
            {
                if (isPrime[p])
                {
                    eulerTotient[p] = p - 1;
                    for (Int32 i = 2 * p; i <= max; i+=p)
                    {
                        isPrime[i] = false;
                        eulerTotient[i] /= p;
                        eulerTotient[i] *= p - 1;
                    }
                }
            }
        }
    }
}