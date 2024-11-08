using System.Numerics;
namespace Problem2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(Int32, Int64)> inputs = new List<(Int32, Int64)>();
            BigInteger[] outputs = new BigInteger[numberOfCases];
            EvenFibonacci fibIterator = new EvenFibonacci();
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 inputValue = Int64.Parse(Console.ReadLine());
                inputs.Add((i, inputValue));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 input = inputs[i].Item2;
                Int32 outputIndex = inputs[i].Item1;
                fibIterator.IterateUntil(input);
                outputs[outputIndex] = fibIterator.currentSum;
            }
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }
        }
    }
    internal class EvenFibonacci
    {
        public BigInteger currentSum;
        Int64 currentValue;
        Int64 previousValue;
        public EvenFibonacci()
        {
            currentSum = 0;
            currentValue = 2;
            previousValue = 1;
        }
        public void IterateUntil(long maxValue)
        {
            while (true)
            {
                if (currentValue > maxValue)
                {
                    return;
                }
                IterateOneStep();
            }
        }
        private void IterateOneStep()
        {
            currentSum += currentValue;
            Int64 oldCurrentValue = currentValue;
            currentValue = 3 * currentValue + 2 * previousValue;
            previousValue = 2 * oldCurrentValue + previousValue;
        }
    }
}