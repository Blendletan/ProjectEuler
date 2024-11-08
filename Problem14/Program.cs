namespace Problem14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int32 maxInput = 0;
            List<(Int32, Int32)> inputs = new List<(int, int)>();
            Int32[] outputs = new Int32[numberOfCases];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > maxInput) { maxInput = nextInput; }
                inputs.Add((i, nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            Collatz myCollatz = new Collatz(maxInput);
            Int32 currentIndex = 1;
            Int32 currentMaxIndex = currentIndex;
            Int32 currentMax = myCollatz.stoppingTimes[currentMaxIndex];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = inputs[i].Item2;
                Int32 outputLocation = inputs[i].Item1;
                while (currentIndex <= nextInput)
                {
                    if (myCollatz.stoppingTimes[currentIndex] >= currentMax)
                    {
                        currentMaxIndex = currentIndex;
                        currentMax = myCollatz.stoppingTimes[currentMaxIndex];
                    }
                    currentIndex++;
                }
                outputs[outputLocation] = currentMaxIndex;
            }
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }
        }
    }
    internal class Collatz
    {
        public readonly Int32[] stoppingTimes;
        Int32 size;
        public Collatz(Int32 max)
        {
            size = max + 1;
            stoppingTimes = new int[size];
            AddPowersOfTwoTimesItem(1, 0);
            for (Int32 i = 1; i < size; i++)
            {
                if (!StoppingTimeFound(i))
                {
                    ComputeStoppingTime(i);
                }
            }
        }
        void ComputeStoppingTime(Int32 input)
        {
            List<Int64> trajectory = new List<Int64>();
            Int64 currentNumber = input;
            while (true)
            {
                if (currentNumber < size)
                {
                    if (StoppingTimeFound((Int32)currentNumber))
                    {
                        trajectory.Add(currentNumber);
                        break;
                    }
                }
                trajectory.Add(currentNumber);
                currentNumber = CollatzMap(currentNumber);
            }
            AddTrajectoryToList(trajectory);
        }
        void AddPowersOfTwoTimesItem(Int32 input, Int32 stopTime)
        {
            if (input < size)
            {
                stoppingTimes[input] = stopTime;
                AddPowersOfTwoTimesItem(2 * input, stopTime + 1);
            }
        }
        void AddTrajectoryToList(List<Int64> trajectory)
        {
            if (trajectory.Count <= 1)
            {
                return;
            }
            trajectory.Reverse();
            Int64 currentInput = trajectory[0];
            Int32 foundStoppingTime = stoppingTimes[(Int32)currentInput];
            Int32 currentStoppingTime = 0;
            while (trajectory.Count > 1)
            {
                trajectory.RemoveAt(0);
                currentInput = trajectory[0];
                currentStoppingTime++;
                if (currentInput < size && !StoppingTimeFound((Int32)currentInput))
                {
                    AddPowersOfTwoTimesItem((Int32)currentInput, currentStoppingTime + foundStoppingTime);
                }
            }
        }
        bool StoppingTimeFound(Int32 n)
        {
            if (stoppingTimes[n] == 0)
            {
                return false;
            }
            return true;
        }
        Int64 CollatzMap(Int64 input)
        {
            if (input % 2 == 0)
            {
                return input / 2;
            }
            return 3 * input + 1;
        }
    }
}
