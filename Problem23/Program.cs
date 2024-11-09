namespace Problem23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int32 maxInput = 0;
            Int32[] inputs = new int[numberOfCases];
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > maxInput) { maxInput = nextInput; }
                inputs[i] = nextInput;
            }
            bool[] admissibleList = GenerateAdmissibleNumberList(maxInput);
            foreach(Int32 input in inputs)
            {
                if (admissibleList[input])
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }
        static bool[] GenerateAdmissibleNumberList(Int32 maxSize)
        {
            List<Int32> abundantNumbers = GenerateAbundantNumbersList(maxSize);
            bool[] output = new bool[maxSize + 1];
            foreach (Int32 i in abundantNumbers)
            {
                foreach(Int32 j in abundantNumbers)
                {
                    if (i+j <= maxSize)
                    {
                        output[i + j] = true;
                    }
                }
            }
            return output;
        }
        static List<Int32> GenerateAbundantNumbersList(Int32 maxSize)
        {
            Int32[] sumOfDivisors = new Int32[maxSize + 1];
            for (Int32 i = 1; i <= maxSize; i++)
            {
                sumOfDivisors[i] -= i;
                for (Int32 j = i; j <= maxSize; j += i)
                {
                    sumOfDivisors[j] += i;
                }
            }
            List<Int32> output = new List<Int32>();
            for(Int32 i = 2; i <= maxSize; i++)
            {
                if (sumOfDivisors[i] > i)
                {
                    output.Add(i);
                }
            }
            return output;
        }
    }
}
