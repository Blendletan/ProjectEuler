namespace Problem12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(Int32, Int32)> inputs = new List<(Int32, Int32)>();
            Int64[] outputs = new long[numberOfCases];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextLine = Int32.Parse(Console.ReadLine());
                inputs.Add((i, nextLine));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            TriangularNumber tn = new TriangularNumber();
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = inputs[i].Item2;
                while (true)
                {
                    if (tn.currentNumberOfDivisors > input)
                    {
                        Int32 index = inputs[i].Item1;
                        outputs[index] = tn.currentNumber;
                        break;
                    }
                    tn.Increment();
                }
            }
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }
        }
    }
    internal class TriangularNumber
    {
        public Int32 currentNumberOfDivisors = 1;
        public Int64 currentNumber = 1;
        Int32 n = 1;
        private Int32 NumberOfDivisors(Int64 n)
        {
            if (n == 1)
            {
                return 1;
            }
            Int64 max = (Int64)Math.Sqrt(n);
            Int32 output = 0;
            for (Int32 i = 1; i <= max; i++)
            {
                if (n % i == 0)
                {
                    output++;
                    if (i * i < n)
                    {
                        output++;
                    }
                }
            }
            return output;
        }
        public void Increment()
        {
            n++;
            currentNumber = n * (n + 1) / 2;
            Int32 left, right;
            if (n % 2 == 0)
            {
                left = n / 2;
                right = n + 1;
            }
            else
            {
                left = n;
                right = (n + 1) / 2;
            }
            currentNumberOfDivisors = NumberOfDivisors(left) * NumberOfDivisors(right);
        }
    }
}
