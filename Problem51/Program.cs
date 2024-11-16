using System.Text;

namespace Problem51
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 numberOfDigits = Int32.Parse(inputs[0]);
            Int32 numberOfDigitsToReplace = Int32.Parse(inputs[1]);
            Int32 cycleSize = Int32.Parse(inputs[2]);
            PrimeDigitHelper helper = new PrimeDigitHelper(numberOfDigits, numberOfDigitsToReplace, cycleSize);
            List<Int32> answer = helper.GetSmallestKCycle();
            for(Int32 i = 0;i< cycleSize; i++)
            {
                Console.Write($"{answer[i]} ");
            }
        }
    }
    internal class PrimeDigitHelper
    {
        HashSet<Int32> admissablePrimeList;
        Int32 numberOfDigits;
        Int32 numberOfDigitsToReplace;
        Int32 cycleSize;
        public PrimeDigitHelper(Int32 numberOfDigits, Int32 numberOfDigitsToReplace, Int32 cycleSize)
        {
            this.numberOfDigits = numberOfDigits;
            this.numberOfDigitsToReplace = numberOfDigitsToReplace;
            this.cycleSize = cycleSize;
            Int32 maxPrimeSize = (Int32)Math.Pow(10, numberOfDigits);
            Int32 maxSieveHeight = (Int32)Math.Sqrt(maxPrimeSize);
            bool[] isPrime = new bool[maxPrimeSize];
            for (Int32 i = 2; i < maxPrimeSize; i++)
            {
                isPrime[i] = true;
            }
            for (Int32 p = 2; p <= maxSieveHeight; p++)
            {
                if (isPrime[p])
                {
                    for (Int32 i = 2 * p; i < maxPrimeSize; i += p)
                    {
                        isPrime[i] = false;
                    }
                }
            }
            admissablePrimeList = new HashSet<Int32>();
            for (Int32 p = 2; p < maxPrimeSize; p++)
            {
                if (isPrime[p])
                {
                    if (IsAdmissablePrime(p, numberOfDigits, numberOfDigitsToReplace))
                    {
                        admissablePrimeList.Add(p);
                    }
                }
            }
        }
        public List<Int32> GetSmallestKCycle()
        {
            foreach (var p in admissablePrimeList)
            {
                List<Int32> smallestList = new List<Int32>();
                for(Int32 i = 0; i < cycleSize; i++)
                {
                    smallestList.Add(Int32.MaxValue);
                }
                List<List<Int32>> digitMaskList = GetDigitMasks(p);
                foreach(var digitMask in digitMaskList)
                {
                    List<Int32> cycle = GenerateAdmissableDigitReplacements(p, digitMask);
                    if (cycle.Count >= cycleSize)
                    {
                        cycle.Sort();
                        bool isSmaller = true;
                        for (Int32 i = 0; i < cycleSize; i++)
                        {
                            if (cycle[i] > smallestList[i])
                            {
                                isSmaller = false;
                            }
                        }
                        if (isSmaller)
                        {
                            smallestList = cycle;
                        }
                    }
                }
                if (smallestList[0] != Int32.MaxValue)
                {
                    return smallestList;
                }
            }
            return new List<Int32>();
        }
        private List<List<Int32>> GetDigitMasks(Int32 p)
        {
            List<List<Int32>> output = new List<List<Int32>>(); ;
            Int32[] digitHistogram = GenerateDigitHistogram(p);
            List<Int32> eligableDigits = new List<Int32>();
            for(Int32 i = 0; i < 10; i++)
            {
                if (digitHistogram[i] >= numberOfDigitsToReplace)
                {
                    eligableDigits.Add(i);
                }
            }
            foreach(var v in eligableDigits)
            {
                List<Int32> possibleIndices = GenerateMatchingDigitPositions(p, v);
                List<List<Int32>> nextDigitMasks = GetKElementSubsets(possibleIndices, numberOfDigitsToReplace);
                foreach(var w in nextDigitMasks)
                {
                    output.Add(w);
                }
            }
            return output;
        }
        private static List<List<Int32>> GetKElementSubsets(List<Int32> originalSet,Int32 k)
        {
            Int32 originalSetSize = originalSet.Count;
            Int32 midpoint = originalSetSize / 2;
            if (k > midpoint)
            {
                List<List<Int32>> output = new List<List<Int32>>();
                foreach (var subset in GetKElementSubsets(originalSet, originalSetSize - k))
                {
                    List<Int32> nextOutput = new List<Int32>(originalSet);
                    foreach (var element in subset)
                    {
                        nextOutput.Remove(element);
                    }
                    output.Add(nextOutput);
                }
                return output;
            }
            List<List<Int32>>[] subsets = new List<List<Int32>>[k + 1];
            subsets[0] = new List<List<Int32>>();
            subsets[0].Add(new List<Int32>());
            for(Int32 subsetSize = 1; subsetSize <= k; subsetSize++)
            {
                subsets[subsetSize] = new List<List<Int32>>();
                foreach (var previousSubset in subsets[subsetSize - 1])
                {
                    List<Int32> availableElements = new List<Int32>(originalSet);
                    foreach (var alreadyUsedElement in previousSubset)
                    {
                        availableElements.Remove(alreadyUsedElement);
                    }
                    foreach(var newElement in availableElements)
                    {
                        List<Int32> newSubset = new List<Int32>(previousSubset);
                        newSubset.Add(newElement);
                        if (IsSorted(newSubset))
                        {
                            newSubset.Sort();
                            newSubset.Reverse();
                            subsets[subsetSize].Add(newSubset);
                        }
                    }
                }
                subsets[subsetSize].Reverse();
            }
            return subsets[k];
        }
        private static bool IsSorted(List<Int32> input)
        {
            Int32 lastIndex = input.Count-1;
            Int32 index = 0;
            if (lastIndex < 1)
            {
                return true;
            }
            while(index < lastIndex && input[index] <= input[index + 1])
            {
                index++;
            }
            return index == lastIndex;
        }
        static List<Int32> GenerateMatchingDigitPositions(Int32 p,Int32 digitToMatch)
        {
            List<Int32> output = new List<Int32>();
            string digits = p.ToString();
            Int32 length = digits.Length;
            for(Int32 i = 0; i < length; i++)
            {
                if (GetDigit(digits[i]) == digitToMatch)
                {
                    output.Add(i);
                }
            }
            return output;
        }
        static bool IsAdmissablePrime(Int32 p, Int32 numberOfDigits, Int32 numberOfDigitsToReplace)
        {
            Int32 minSize = (Int32)Math.Pow(10, numberOfDigits - 1);
            Int32 maxSize = (Int32)Math.Pow(10, numberOfDigits);
            if (p < minSize || p >= maxSize)
            {
                return false;
            }
            Int32[] digitHistogram = GenerateDigitHistogram(p);
            foreach (var v in digitHistogram)
            {
                if (v >= numberOfDigitsToReplace)
                {
                    return true;
                }
            }
            return false;
        }
        static Int32[] GenerateDigitHistogram(Int32 p)
        {
            Int32[] output = new int[10];
            char[] digits = p.ToString().ToCharArray();
            foreach (var d in digits)
            {
                Int32 digit = GetDigit(d);
                output[digit]++;
            }
            return output;
        }
        static Int32 GetDigit(char c)
        {
            return c - '0';
        }
        private List<Int32> GenerateAdmissableDigitReplacements(Int32 p, List<Int32> digitMask)
        {
            List<Int32> output = new List<Int32>();
            string inputDigits = p.ToString();
            for(Int32 i = 0; i < 10; i++) 
            {
                StringBuilder outputDigits = new StringBuilder(inputDigits);
                foreach (var index in digitMask)
                {
                    outputDigits[index] = i.ToString()[0];
                }
                Int32 nextOutput = Int32.Parse(outputDigits.ToString());
                if (admissablePrimeList.Contains(nextOutput))
                {
                    if (!output.Contains(nextOutput))
                    {
                        output.Add(nextOutput);
                    }
                }
            }
            return output;
        }
    }
}