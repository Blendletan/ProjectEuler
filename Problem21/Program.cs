namespace Problem21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(Int32, Int32)> inputs = new List<(Int32, Int32)>();
            Int32 maxInput = 0;
            Int32[] outputs = new Int32[numberOfCases];
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > maxInput) { maxInput = nextInput; }
                inputs.Add((i, nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            Int32[] sumOfDivisorsTable = GenerateSumOfDivisorsTable(maxInput);
            Int32 currentIndex = 1;
            Int32 currentSum = 0;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = inputs[i].Item2;
                Int32 outputLocation = inputs[i].Item1;
                while (currentIndex < nextInput)
                {
                    if (IsAmicable(currentIndex, sumOfDivisorsTable))
                    {
                        //Console.WriteLine(currentIndex);
                        currentSum += currentIndex;
                    }
                    currentIndex++;
                }
                outputs[outputLocation] = currentSum;
            }
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }
        }
        static bool IsAmicable(Int32 n, Int32[] sumOfDivisorsTable)
        {
            Int32 size = sumOfDivisorsTable.Length;
            Int32 partnerNumber = sumOfDivisorsTable[n];
            if (partnerNumber == n)
            {
                return false;
            }
            Int32 partnerSumOfDivisors;
            if (partnerNumber < size)
            {
                partnerSumOfDivisors = sumOfDivisorsTable[partnerNumber];
            }
            else
            {
                partnerSumOfDivisors = SumOfDivisors(partnerNumber);
            }
            if (partnerSumOfDivisors == n)
            {
                return true;
            }
            return false;
        }
        static Int32[] GenerateSumOfDivisorsTable(Int32 maxSize)
        {
            Int32[] output = new Int32[maxSize + 1];
            for(Int32 i = 1; i <= maxSize; i++)
            {
                output[i] -= i;
                for(Int32 j = i; j <= maxSize; j+=i)
                {
                    output[j] += i;
                }
            }
            return output;
        }
        static Int32 SumOfDivisors(Int32 n)
        {
            Int32 maxHeight = (Int32)Math.Sqrt(n);
            Int32 output = 0;
            for(Int32 i = 1; i <= maxHeight; i++)
            {
                if (n % i == 0)
                {
                    output += i;
                    if (i*i != n)
                    {
                        output += n / i;
                    }
                }
            }
            return output-n;
        }
    }
}
