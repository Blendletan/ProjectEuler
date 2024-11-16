namespace Problem87
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
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs.Add((i, nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            PrimePowerHelper helper = new PrimePowerHelper(maxInput);
            List<Int32> admissableIntegers = helper.GenerateAdmissableIntegers();
            Int32 currentIndex = 0;
            foreach (var i in inputs)
            {
                Int32 outputLocation = i.Item1;
                Int32 nextInput = i.Item2;
                while (true)
                {
                    if (currentIndex >= admissableIntegers.Count)
                    {
                        break;
                    }
                    Int32 nextAdmissable = admissableIntegers[currentIndex];
                    if (nextAdmissable > nextInput)
                    {
                        break;
                    }
                    currentIndex++;
                }
                outputs[outputLocation] = currentIndex;
            }
            foreach(var v in outputs)
            {
                Console.WriteLine(v);
            }
        }
    }
    internal class PrimePowerHelper
    {
        Int32 maxIntegerSize;
        List<Int64> primeList;
        public PrimePowerHelper(Int32 max)
        {
            maxIntegerSize = max;
            primeList = new List<Int64>();
            Int32 maxPrimeSize = (Int32)Math.Sqrt(maxIntegerSize);
            Int32 maxSieveSize = (Int32)Math.Sqrt(maxPrimeSize);
            bool[] isPrime = new bool[maxPrimeSize + 1];
            for(Int32 i = 2; i <= maxPrimeSize; i++)
            {
                isPrime[i] = true;
            }
            for(Int32 p = 2; p <= maxSieveSize; p++)
            {
                if (isPrime[p])
                {
                    for(Int32 i = 2 * p; i <= maxPrimeSize; i += p)
                    {
                        isPrime[i] = false;
                    }
                }
            }
            for(Int32 i = 2; i <= maxPrimeSize; i++)
            {
                if (isPrime[i])
                {
                    primeList.Add(i);
                }
            }
        }
        public List<Int32> GenerateAdmissableIntegers()
        {
            HashSet<Int32> foundIntegers = new HashSet<Int32>();
            foreach (var p in primeList)
            {
                Int64 pPower = p * p;
                if (pPower > maxIntegerSize)
                {
                    break;
                }
                foreach(var l in primeList)
                {
                    Int64 lPower = l * l * l;
                    if (lPower > maxIntegerSize)
                    {
                        break;
                    }
                    foreach(var q in primeList)
                    {
                        Int64 qPower = q * q * q * q;
                        Int64 nextResult = qPower + lPower + pPower;
                        if (nextResult < maxIntegerSize)
                        {
                            Int32 nextResultShort = (Int32)nextResult;
                            if (!foundIntegers.Contains(nextResultShort))
                            {
                                foundIntegers.Add(nextResultShort);
                            }
                        }
                    }
                }
            }
            List<Int32> output = foundIntegers.ToList();
            output.Sort();
            return output;
        }
    }
}