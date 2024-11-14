using System.Text;

namespace Problem36
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            Int32 max = Int32.Parse(input[0]);
            List<Int32> baseTenPalindromes = GeneratePalindromes(max);
            Int32 baseB = Int32.Parse(input[1]);
            List<Int32> validPalindromes = new List<Int32>();
            foreach (Int32 i in baseTenPalindromes)
            {
                if (IsPalindrome(i, baseB))
                {
                    validPalindromes.Add(i);
                }
            }
            Console.WriteLine(validPalindromes.Sum());
        }
        static List<Int32> GeneratePalindromes(Int32 max)
        {
            List<Int32> output = new List<Int32>();
            for (Int32 i = 1; i < 10; i++)
            {
                if (i <= max && !output.Contains(i))
                {
                    output.Add(i);
                }
            }
            for (Int32 i = 1; i < 100; i++)
            {
                Int32 next = CreatePalindrome(i, false, 0);
                if (next <= max && !output.Contains(next))
                {
                    output.Add(next);
                }
                for (Int32 j = 0; j < 10; j++)
                {
                    next = CreatePalindrome(i, true, j);
                    if (next <= max && !output.Contains(next))
                    {
                        output.Add(next);
                    }
                }
            }
            for (Int32 i = 100; i < 1000; i++)
            {
                Int32 next = CreatePalindrome(i, false, 0);
                if (next <= max && !output.Contains(next))
                {
                    output.Add(next);
                }
            }
            return output;
        }
        static string ReverseString(string input)
        {
            return new String(input.ToCharArray().Reverse().ToArray());
        }
        static Int32 CreatePalindrome(Int32 leftHalf, bool odd, Int32 middleDigit)
        {
            string digits = leftHalf.ToString();

            string rightDigits = new String(digits.ToCharArray().Reverse().ToArray());
            string output = String.Empty;
            if (odd)
            {
                output = digits + middleDigit.ToString() + rightDigits;
            }
            else
            {
                output = digits + rightDigits;
            }
            return Int32.Parse(output);
        }
        static bool IsPalindrome(Int32 n, Int32 b)
        {
            StringBuilder digits = new StringBuilder(WriteInBaseB(n, b));
            if (digits.Length == 1) { return true; }
            StringBuilder leftHalf = new StringBuilder();
            StringBuilder rightHalf = new StringBuilder();
            Int32 halfPoint = digits.Length / 2;
            for (Int32 i = 0; i < halfPoint; i++)
            {
                leftHalf.Append(digits[i]);
            }
            Int32 rightHalfStartPoint;
            if (digits.Length % 2 == 0)
            {
                rightHalfStartPoint = halfPoint;
            }
            else
            {
                rightHalfStartPoint = halfPoint + 1;
            }
            for (Int32 i = rightHalfStartPoint; i < digits.Length; i++)
            {
                rightHalf.Append(digits[i]);
            }
            if (leftHalf.ToString() == ReverseString(rightHalf.ToString()))
            {
                return true;
            }
            return false;

        }
        static string WriteInBaseB(Int32 n, Int32 b)
        {
            string output = String.Empty;
            while (n > 0)
            {
                output = (n % b).ToString() + output;
                n /= b;
            }
            return output;
        }
    }
}