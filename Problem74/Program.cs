namespace Problem74
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            (Int128, Int32)[] inputs = new (Int128, Int32)[numberOfCases];
            Int128 maxSize = 0;
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                string[] nextInput = Console.ReadLine().Split();
                inputs[i] = (Int128.Parse(nextInput[0]), Int32.Parse(nextInput[1]));
                if (inputs[i].Item1 > maxSize)
                {
                    maxSize = inputs[i].Item1;
                }
            }
            DigitFactorialHelper helper = new DigitFactorialHelper(maxSize);
            helper.Solve();
            foreach (var nextInput in inputs)
            {
                Int128 maxToSearch = nextInput.Item1;
                Int32 length = nextInput.Item2;
                List<Int128> output = helper.GetChains(maxToSearch, length);
                WriteOutput(output);
            }
        }
        static void WriteOutput(List<Int128> outputs)
        {
            if (outputs.Count == 0)
            {
                Console.Write(-1);
            }
            foreach (var v in outputs)
            {
                Console.Write($"{v} ");
            }
            Console.WriteLine();
        }
    }
    internal class DigitFactorialHelper
    {
        Int128 maxSize;
        Dictionary<Int128, Int32> factorialChainLengths;
        public DigitFactorialHelper(Int128 max)
        {
            maxSize = max;
            factorialChainLengths = new Dictionary<Int128, Int32>();
        }
        public void Solve()
        {
            for (Int32 i = 0; i <= maxSize; i++)
            {
                if (!factorialChainLengths.ContainsKey(i))
                {
                    ProcessTrajectory(i);
                }
            }
        }
        public List<Int128> GetChains(Int128 maxHeightToSearch, Int32 length)
        {
            List<Int128> output = new List<Int128>();
            foreach (var v in factorialChainLengths)
            {
                if (v.Key <= maxHeightToSearch)
                {
                    if (v.Value == length)
                    {
                        output.Add(v.Key);
                    }
                }
            }
            output.Sort();
            return output;
        }
        void TryToAddLength(Int128 n, Int32 length)
        {
            if (!factorialChainLengths.ContainsKey(n))
            {
                factorialChainLengths.Add(n, length);
            }
        }
        void TryToAddTrajectory(List<Int128> trajectory)
        {
            Int32 totalLength = trajectory.Count;
            Int32 loopBackPoint = trajectory.IndexOf(DigitFactorialSum(trajectory.Last()));
            Int32 loopLength = totalLength - loopBackPoint;
            for (Int32 i = 0; i < loopBackPoint; i++)
            {
                TryToAddLength(trajectory[i], totalLength - i);
            }
            for(Int32 i = loopBackPoint; i < totalLength; i++)
            {
                TryToAddLength(trajectory[i], loopLength);
            }
        }
        void ExtendTrajectory(List<Int128> trajectory,Int32 previousLength)
        {
            Int32 trajectoryLength = trajectory.Count;
            for(Int32 i = 0; i < trajectoryLength; i++)
            {
                TryToAddLength(trajectory[i], trajectoryLength - i + previousLength);
            }
        }
        void ProcessTrajectory(Int128 n)
        {
            List<Int128> trajectory = new List<Int128>();
            while (true)
            {
                if (factorialChainLengths.ContainsKey(n))
                {
                    ExtendTrajectory(trajectory, factorialChainLengths[n]);
                    return;
                }
                if (trajectory.Contains(n))
                {
                    TryToAddTrajectory(trajectory);
                    return;
                }
                trajectory.Add(n);
                n = DigitFactorialSum(n);
            }
        }
        static Int128 Factorial(Int128 n)
        {
            if (n < 0)
            {
                throw new Exception("Factorial for positive integers only");
            }
            if (n == 0)
            {
                return 1;
            }
            if (n == 1)
            {
                return 1;
            }
            return n * Factorial(n - 1);
        }
        static Int128 DigitFactorialSum(Int128 n)
        {
            List<Int128> digits = GetDigits(n);
            Int128 sum = 0;
            foreach (var d in digits)
            {
                sum += Factorial(d);
            }
            return sum;
        }
        static List<Int128> GetDigits(Int128 n)
        {
            List<Int128> output = new List<Int128>();
            while (true)
            {
                Int128 nextDigit = n % 10;
                output.Add(nextDigit);
                if (n < 10)
                {
                    return output;
                }
                n /= 10;
            }
        }
    }
}
