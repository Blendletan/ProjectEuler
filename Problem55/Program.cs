using System.Numerics;

namespace Problem55
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 max = Int32.Parse(Console.ReadLine());
            PalindromeHelper ph = new PalindromeHelper(max);
            Dictionary<BigInteger, Int32> lychrel = ph.LychrelCounts();
            BigInteger currentMaxPalindrome = 0;
            Int32 currentMaxCount = 0;
            foreach (var v in lychrel)
            {
                BigInteger palindrome = v.Key;
                Int32 count = v.Value;
                if (count > currentMaxCount)
                {
                    currentMaxCount = count;
                    currentMaxPalindrome = palindrome;
                }
            }
            Console.WriteLine($"{currentMaxPalindrome} {currentMaxCount}");
        }
    }
    internal class PalindromeHelper
    {
        Int32 max;
        public PalindromeHelper(Int32 inputMax)
        {
            this.max = inputMax;
        }
        public Dictionary<BigInteger, Int32> LychrelCounts()
        {
            Dictionary<BigInteger, Int32> output = new Dictionary<BigInteger, Int32>();
            for(Int32 i = 0; i <= max; i++)
            {
                BigInteger? nextValue = GetLychrelTarget(i);
                if (nextValue == null)
                {
                    continue;
                }
                if (output.ContainsKey(nextValue.Value))
                {
                    output[nextValue.Value]++;
                }
                else
                {
                    output.Add(nextValue.Value, 1);
                }
            }
            return output;
        }
        private BigInteger? GetLychrelTarget(BigInteger n)
        {
            BigInteger currentValue = n;
            for(Int32 i = 0; i <= 60; i++)
            {
                if (IsPalindrome(currentValue))
                {
                    return currentValue;
                }
                currentValue = ReverseAndSum(currentValue);
            }
            return null;
        }
        private bool IsPalindrome(BigInteger n)
        {
            string digits = n.ToString();
            Int32 length = digits.Length;
            Int32 midpoint = length / 2;
            for(Int32 i = 0; i <= midpoint; i++)
            {
                if (digits[i] != digits[length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
        private BigInteger ReverseAndSum(BigInteger n)
        {
            return n + ReverseDigits(n);
        }
        private static BigInteger ReverseDigits(BigInteger n)
        {
            char[] charArray = n.ToString().ToCharArray();
            Array.Reverse(charArray);
            return BigInteger.Parse(charArray);
        }
    }
}