namespace Problem41
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<Int64> resultList = GeneratePandigitalPrimes();
            resultList.Sort();
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 input = Int64.Parse(Console.ReadLine());
                Int64 result = LargestPandigital(input, resultList);
                Console.WriteLine(result);
            }
        }
        static Int64 LargestPandigital(Int64 max, List<Int64> sortedList)
        {
            if (sortedList[0] > max) { return -1; }
            if (sortedList.Last() < max) { return sortedList.Last(); }
            Int32 index = 0;
            Int64 currentInput = sortedList[0];
            Int64 previousInput = currentInput;
            while (true)
            {
                if (currentInput > max)
                {
                    return previousInput;
                }
                if (currentInput == max)
                {
                    return currentInput;
                }
                index++;
                previousInput = currentInput;
                currentInput = sortedList[index];
            }
        }
        static List<Int64> GeneratePandigitalPrimes()
        {
            List<Int64> output = new List<Int64>();
            for (Int32 i = 1; i < 10; i++)
            {
                List<Int64> iDigitPandigitals = GenerateNDigitPandigitals(i);
                foreach (Int64 number in iDigitPandigitals)
                {
                    if (!output.Contains(number) && IsPrime(number))
                    {
                        output.Add(number);
                    }
                }
            }
            return output;
        }
        static bool IsPrime(Int64 input)
        {
            if (input == 1) { return false; }
            Int64 maxHeight = (Int64)Math.Sqrt(input);
            for (Int32 i = 2; i <= maxHeight; i++)
            {
                if (input % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        static List<Int64> GenerateNDigitPandigitals(Int32 n)
        {
            List<char> input = new List<char>();
            List<Int64> output = new List<Int64>();
            for (Int32 i = 1; i <= n; i++)
            {
                input.Add(i.ToString()[0]);
            }
            Int64 sizeOfOutput = Factorial(n);
            for (Int32 i = 0; i < sizeOfOutput; i++)
            {
                List<char> resultChars = GetPermutation(input, i);
                Int64 result = Int64.Parse(new string(resultChars.ToArray()));
                output.Add(result);
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
