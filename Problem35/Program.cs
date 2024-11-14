using System.Text;

namespace Problem35
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int64 maxSize = 10 * Int64.Parse(Console.ReadLine());
            CircularPrimeSolver solver = new CircularPrimeSolver(maxSize);
            solver.ComputeCircularPrimes();
            Console.WriteLine(solver.CircularPrimeSum());
        }
    }
    internal class CircularPrimeSolver
    {
        bool[] isPrime;
        bool[] isPrimeCopy;
        private List<Int64> circularPrimeList;
        readonly Int64 maxSize;
        public CircularPrimeSolver(Int64 maxSize)
        {
            this.maxSize = maxSize;
            isPrime = new bool[maxSize + 1];
            Int64 maxHeightToSearch = (Int64)Math.Sqrt(maxSize + 1);
            isPrime[0] = false;
            isPrime[1] = false;
            for (Int32 i = 2; i <= maxSize; i++)
            {
                isPrime[i] = true;
            }
            for (Int64 i = 2; i <= maxHeightToSearch; i++)
            {
                if (isPrime[i])
                {
                    for (Int64 j = 2 * i; j <= maxSize; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            isPrimeCopy = new bool[maxSize + 1];
            for (Int32 i = 0; i <= maxSize; i++)
            {
                isPrimeCopy[i] = isPrime[i];
            }
            circularPrimeList = new List<Int64>();
        }
        public void ComputeCircularPrimes()
        {
            for (Int32 i = 2; i <= maxSize / 10; i++)
            {
                if (isPrimeCopy[i])
                {
                    (bool addList, List<Int64> cycleList) = ListOfCycles(i);
                    foreach (Int64 cycle in cycleList)
                    {
                        if (addList && cycle <= (maxSize / 10)) { circularPrimeList.Add(cycle); }
                        isPrimeCopy[cycle] = false;
                    }
                }
            }
        }

        private (bool, List<Int64>) ListOfCycles(Int64 n)
        {
            string inputInt = n.ToString();
            string previousPermutation = inputInt;
            Int32 inputLength = inputInt.Length;
            List<Int64> output2 = new List<Int64>();
            bool output1 = true;
            for (Int32 i = 0; i < inputInt.Length; i++)
            {
                string nextPermutation = NextPermutation(previousPermutation);
                Int64 nextInt = Int64.Parse(nextPermutation);
                if (!isPrime[nextInt])
                {
                    output1 = false;
                }
                if (!output2.Contains(nextInt))
                {
                    output2.Add(nextInt);
                }
                previousPermutation = nextPermutation;
            }
            return (output1, output2);
        }
        private string NextPermutation(string input)
        {
            StringBuilder output = new StringBuilder(input);
            char firstDigit = input[0];
            output.Remove(0, 1);
            output.Append(firstDigit);
            return output.ToString();
        }
        private void LeftPad(StringBuilder inputString, Int32 desiredLength)
        {
            if (inputString.Length >= desiredLength) { return; }
            Int32 amountToAdd = inputString.Length - desiredLength;
            StringBuilder leftPad = new StringBuilder();
            for (Int32 i = 0; i < amountToAdd; i++)
            {
                leftPad.Append('0');
            }
            inputString.Insert(0, leftPad.ToString());
            return;
        }
        public Int64 CircularPrimeSum()
        {
            return circularPrimeList.Sum(x => x);
        }
    }
}
