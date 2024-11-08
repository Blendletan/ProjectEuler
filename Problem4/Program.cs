namespace Problem4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(Int32, Int64)> inputs = new List<(Int32, Int64)>();
            Int64[] outputs = new Int64[numberOfCases];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = Int64.Parse(Console.ReadLine());
                inputs.Add((i, nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            Palindrome pHelper = new Palindrome();
            Int32 palindromeIndex = 0;
            Int64 currentPalindrome = pHelper.listOfPalindromes[palindromeIndex];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 currentTarget = inputs[i].Item2;
                Int32 currentLineNumber = inputs[i].Item1;
                while (currentPalindrome < currentTarget)
                {
                    if (currentPalindrome >= currentTarget) { break; }
                    palindromeIndex++;
                    if (palindromeIndex == pHelper.listOfPalindromes.Count)
                    {
                        break;
                    }
                    else
                    {
                        currentPalindrome = pHelper.listOfPalindromes[palindromeIndex];
                    }
                }
                palindromeIndex--;
                currentPalindrome = pHelper.listOfPalindromes[palindromeIndex];
                outputs[currentLineNumber] = currentPalindrome;
            }
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }

        }
    }
    internal class Palindrome
    {
        public List<Int64> listOfPalindromes;
        public Palindrome()
        {
            listOfPalindromes = new List<Int64>();
            for (Int32 i = 101; i < 1000; i++)
            {
                Int64 candidate = i * 1000 + ReverseDigits(i);
                if (CanBeFactored(candidate))
                {
                    listOfPalindromes.Add(candidate);
                }
            }
        }
        private Int64 ReverseDigits(Int64 n)
        {
            List<char> digits = n.ToString().ToCharArray().ToList();
            digits.Reverse();
            string reversedDigits = new string(digits.ToArray());
            return Int64.Parse(reversedDigits);
        }
        private bool Divides(Int64 a, Int64 b)
        {
            Int64 divisor = b / a;
            if (a * divisor == b)
            {
                return true;
            }
            return false;
        }
        private bool CanBeFactored(Int64 n)
        {
            for (Int32 i = 100; i < 1000; i++)
            {
                if (Divides(i, n))
                {
                    Int64 divisor = n / i;
                    if (divisor > 99 && divisor < 1000)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
