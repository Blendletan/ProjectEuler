using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Problem49
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 max = Int32.Parse(inputs[0]);
            Int32 lengthOfProgression = Int32.Parse(inputs[1]);
            PrimePermutationHelper phelper = new PrimePermutationHelper(1000000);
            HashSet<BigInteger> outputs = new HashSet<BigInteger>();
            HashSet<Int32> alreadyStudiedPrimes = new HashSet<Int32>();
            foreach (var p in phelper.primeList)
            {
                if (p > max)
                {
                    continue;
                }
                if (alreadyStudiedPrimes.Contains(p))
                {
                    continue;
                }
                List<Int32> nextList = phelper.GetPrimePerumtations(p);
                foreach (var l in nextList)
                {
                    alreadyStudiedPrimes.Add(l);
                }
                List<string> nextValidList = phelper.GetArithmeticProgressions(nextList,lengthOfProgression,max);
                foreach(var v in nextValidList)
                {
                    BigInteger vInt = BigInteger.Parse(v);
                    if (!outputs.Contains(vInt))
                    {
                        outputs.Add(vInt);
                    }
                }
            }
            List<BigInteger> sortedOutputs = outputs.ToList();
            sortedOutputs.Sort();
            foreach(var v in sortedOutputs)
            {
                Console.WriteLine(v);
            }
        }
    }
    internal class PrimePermutationHelper
    {
        readonly bool[] isPrime;
        public readonly List<Int32> primeList;
        public PrimePermutationHelper(Int32 max)
        {
            Int32 maxHeight = (Int32)Math.Sqrt(max);
            isPrime = new bool[max + 1];
            primeList = new List<Int32>();
            for(Int32 i = 2; i <= max; i++)
            {
                isPrime[i] = true;
            }
            for(Int32 i = 2; i <= maxHeight; i++)
            {
                if (isPrime[i])
                {
                    for(Int32 j = 2 * i; j <= max; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            for(Int32 i = 2; i <= max; i++)
            {
                if (isPrime[i])
                {
                    primeList.Add(i);
                }
            }
        }
        public List<string> GetArithmeticProgressions(List<Int32> inputList,Int32 lengthOfProgression,Int32 max)
        {
            List<string> output = new List<string>();
            inputList.Sort();
            Int32 length = inputList.Count;
            for(Int32 i = 0; i < length; i++)
            {
                if (inputList[i] > max)
                {
                    continue;
                }
                for(Int32 j = i + 1; j < length; j++)
                {
                    Int32 difference = inputList[j] - inputList[i];
                    Int32 nextNumber = inputList[j] + difference;
                    if (lengthOfProgression == 3)
                    {
                        if (inputList.Contains(nextNumber))
                        {
                            string nextOutput = inputList[i].ToString() + inputList[j].ToString() + nextNumber.ToString();
                            if (!output.Contains(nextOutput))
                            {
                                output.Add(nextOutput);
                            }
                        }
                    }
                    else
                    {
                        Int32 fourthNumber = nextNumber + difference;
                        if (inputList.Contains(nextNumber)&& inputList.Contains(fourthNumber))
                        {
                            string nextOutput = inputList[i].ToString() + inputList[j].ToString() + nextNumber.ToString()+fourthNumber.ToString();
                            if (!output.Contains(nextOutput))
                            {
                                output.Add(nextOutput);
                            }
                        }
                    }
                }
            }
            return output;
        }
        public List<Int32> GetPrimePerumtations(Int32 p)
        {
            List<Int32> output = new List<Int32>();
            Int32 length = p.ToString().Length;
            Int64 numberOfPermutations = Factorial(length);
            for(Int32 i = 0; i < numberOfPermutations; i++)
            {
                Int32 nextNumber = Int32.Parse(GetPermutation(p.ToString().ToList(), i).ToArray());
                if (nextNumber > isPrime.Length)
                {
                    continue;
                }
                if (isPrime[nextNumber]&& !output.Contains(nextNumber) && nextNumber.ToString().Length==length)
                {
                    output.Add(nextNumber);
                }
            }
            return output;
        }
        static List<char> GetPermutation(List<char> inputString, Int64 permutationNumber)
        {
            List<char> inputCopy = new List<char>();
            foreach (char c in inputString) { inputCopy.Add(c); }
            List<char> result;
            Int32 length = inputString.Count();
            if (length <= 1)
            {
                return inputString;
            }
            if (length == 2 && permutationNumber > 0)
            {
                result = inputCopy;
                result.Reverse();
                return result;
            }
            result = new List<char>();
            Int64 factorial = Factorial(length - 1);
            Int64 a = permutationNumber / factorial;
            result.Add(inputString[(Int32)a]);
            inputCopy.RemoveAt((Int32)a);
            Int64 b = permutationNumber % factorial;
            result.AddRange(GetPermutation(inputCopy, b));
            return result;
        }
        static Int64 Factorial(Int32 n)
        {
            if (n == 0) { return 1; }
            if (n == 1) { return 1; }
            Int64 output = 1;
            for (Int64 i = 2; i <= n; i++)
            {
                output *= i;
            }
            return output;
        }
    }
}