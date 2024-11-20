using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Numerics;

namespace Problem60
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 n = Int32.Parse(inputs[0]);
            Int32 k = Int32.Parse(inputs[1]);
            PrimeHelper ph = new PrimeHelper(n);
            ph.GenerateAdmissablePairs();
            List<List<Int32>> results = ph.PrimeKTuples(k);
            List<Int64> sums = new List<Int64>();
            foreach (var v in results)
            {
                sums.Add(v.Sum());
            }
            foreach (var v in sums)
            {
                Console.WriteLine(v);
            }
        }
    }
    internal class PrimeHelper
    {
        List<Int32> primeList;
        bool[] isPrime;
        HashSet<(Int32, Int32)> admissablePairs;
        Dictionary<Int32, List<Int32>> edgeList;
        public PrimeHelper(Int32 maxPrimeSize)
        {
            isPrime = PrimeSieve(maxPrimeSize);
            primeList = new List<Int32>();
            for (Int32 i = 2; i <= maxPrimeSize; i++)
            {
                if (isPrime[i])
                {
                    primeList.Add(i);
                }
            }
            admissablePairs = new HashSet<(Int32, Int32)>();
            edgeList = new Dictionary<Int32, List<Int32>>();
        }
        public void GenerateAdmissablePairs()
        {
            Int32 length = primeList.Count;
            for (Int32 i = 0; i < length; i++)
            {
                Int32 p = primeList[i];
                for (Int32 j = i + 1; j < length; j++)
                {
                    Int32 l = primeList[j];
                    Int64 concatenation1 = Int64.Parse(p.ToString() + l.ToString());
                    Int64 concatenation2 = Int64.Parse(l.ToString() + p.ToString());
                    if (IsPrime(concatenation1))
                    {
                        if (IsPrime(concatenation2))
                        {
                            admissablePairs.Add((p, l));
                            if (!edgeList.ContainsKey(p))
                            {
                                edgeList.Add(p, new List<Int32>());
                            }
                            edgeList[p].Add(l);
                        }
                    }
                }
            }
        }
        public List<List<Int32>> PrimeKTuples(Int32 k)
        {
            Int32 originalSetSize = primeList.Count;
            List<List<Int32>>[] subsets = new List<List<Int32>>[k + 1];
            subsets[2] = new List<List<Int32>>();
            foreach (var v in admissablePairs)
            {
                Int32 p = v.Item1;
                Int32 l = v.Item2;
                if (p < l)
                {
                    List<Int32> nextSubset = new List<Int32>();
                    nextSubset.Add(p);
                    nextSubset.Add(l);
                    subsets[2].Add(nextSubset);
                }
            }
            for (Int32 subsetSize = 3; subsetSize <= k; subsetSize++)
            {
                subsets[subsetSize] = new List<List<Int32>>();
                foreach (var previousSubset in subsets[subsetSize - 1])
                {
                    List<Int32> availableElements = edgeList[previousSubset[0]];
                    foreach (var newElement in availableElements)
                    {
                        if (previousSubset.Contains(newElement))
                        {
                            continue;
                        }
                        if (previousSubset.Count != 0)
                        {
                            Int32 numberToBeat = previousSubset.Last();
                            if (newElement < numberToBeat)
                            {
                                continue;
                            }
                        }
                        List<Int32> newSubset = new List<Int32>(previousSubset);
                        newSubset.Add(newElement);
                        if (IsAdmissable(newSubset))
                        {
                            subsets[subsetSize].Add(newSubset);
                        }
                    }
                }
            }
            return subsets[k];
        }
        private bool IsAdmissable(List<Int32> inputList)
        {
            foreach (var p in inputList)
            {
                Int32 l = inputList.Last();
                if (p != l)
                {
                    if (!admissablePairs.Contains((p, l)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private static bool[] PrimeSieve(Int32 maxPrimeSize)
        {
            bool[] isPrime = new bool[maxPrimeSize + 1];
            Int32 maxSieveHeight = (Int32)Math.Sqrt(maxPrimeSize);
            for (Int32 i = 2; i <= maxPrimeSize; i++)
            {
                isPrime[i] = true;
            }
            for (Int32 p = 2; p <= maxSieveHeight; p++)
            {
                if (isPrime[p])
                {
                    for (Int32 i = 2 * p; i <= maxPrimeSize; i += p)
                    {
                        isPrime[i] = false;
                    }
                }
            }
            return isPrime;
        }
        private bool IsPrime(Int64 input)
        {
            if (input < isPrime.Length)
            {
                return isPrime[input];
            }
            if (input % 2 == 0)
            {
                return false;
            }
            List<Int64> millerRabinBases = new List<Int64> { 2, 3, 5, 7 };
            if (millerRabinBases.Contains(input))
            {
                return true;
            }
            Int64 s = 0;
            Int64 d = input - 1;
            while (true)
            {
                if (d % 2 == 1)
                {
                    break;
                }
                d /= 2;
                s++;
            }
            foreach (var i in millerRabinBases)
            {
                Int64 x = (Int64)BigInteger.ModPow(i, d, input);
                Int64 y = 1;
                for (Int32 j = 0; j < s; j++)
                {
                    y = (Int64)BigInteger.ModPow(x, 2, input);
                    if (y == 1 && x != 1 && x != input - 1)
                    {
                        return false;
                    }
                    x = y;
                }
                if (y != 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}