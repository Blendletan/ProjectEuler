using System.Diagnostics;

namespace Problem43
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 n = Int32.Parse(Console.ReadLine());
            List<Int64> pandigitals;
            if (n != 9)
            {
                pandigitals = GenerateNDigitPandigitals(n);
            }
            else
            {
                pandigitals = GenerateAdmissibleNinePandigitals();
            }
            Int64 sum = 0;
            foreach(var v in pandigitals)
            {
                if (CheckDivisibility(v, n))
                {
                    sum += v;
                }
            }
            Console.WriteLine(sum);
        }
        static bool CheckDivisibility(Int64 pandigital,Int32 n)
        {
            Int32 firstCheck = NextThreeDigits(pandigital, 1);
            if (firstCheck % 2 != 0)
            {
                return false;
            }
            if (n == 3)
            {
                return true;
            }
            Int32 secondCheck = NextThreeDigits(pandigital, 2);
            if (secondCheck % 3 != 0)
            {
                return false;
            }
            if (n == 4)
            {
                return true;
            }
            Int32 ThirdCheck = NextThreeDigits(pandigital, 3);
            if (ThirdCheck % 5 != 0)
            {
                return false;
            }
            if (n == 5)
            {
                return true;
            }
            Int32 fourthCheck = NextThreeDigits(pandigital, 4);
            if (fourthCheck % 7 != 0)
            {
                return false;
            }
            if (n == 6)
            {
                return true;
            }
            Int32 fifthCheck = NextThreeDigits(pandigital, 5);
            if (fifthCheck % 11 != 0)
            {
                return false;
            }
            if (n == 7)
            {
                return true;
            }
            Int32 sixthCheck = NextThreeDigits(pandigital, 6);
            if (sixthCheck % 13 != 0)
            {
                return false;
            }
            if (n == 8)
            {
                return true;
            }
            Int32 seventhCheck = NextThreeDigits(pandigital, 7);
            if (seventhCheck % 17 != 0)
            {
                return false;
            }
            return true;
        }
        static List<Int64> GenerateAdmissibleNinePandigitals()
        {
            List<Int64> output = new List<Int64>();
            List<Int32> admissibleEndings = GenerateAdmissibleMultiplesOfSeventeen();
            foreach (var ending in admissibleEndings)
            {
                List<char> digits = new List<char>{ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                Int32 nextDigit = ending % 10;
                digits.Remove(nextDigit.ToString()[0]);
                nextDigit = (ending / 10) % 10;
                digits.Remove(nextDigit.ToString()[0]);
                nextDigit = (ending / 100) % 10;
                digits.Remove(nextDigit.ToString()[0]);
                Int64 numberOfPermutations = Factorial(7);
                for(Int32 i = 0; i < numberOfPermutations; i++)
                {
                    List<char> resultChars = GetPermutation(digits, i);
                    string nextOutput = new string(resultChars.ToArray()) + ending.ToString();
                    output.Add(Int64.Parse(nextOutput));
                }
            }
            return output;
        }
        static List<Int32> GenerateAdmissibleMultiplesOfSeventeen()
        {
            List<Int32> output = new List<Int32>();
            for(Int32 i = 1; i <= 1000 / 17; i++)
            {
                Int32 nextOutput = 17 * i;
                if (AdmissibleThreeDigitNumber(nextOutput))
                {
                    output.Add(nextOutput);
                }
            }
            return output;
        }
        static bool AdmissibleThreeDigitNumber(Int32 n)
        {
            Int32 a = (n / 100) % 10;
            Int32 b = (n / 10) % 10;
            Int32 c = n % 10;
            if (a == b || a == c | b == c)
            {
                return false;
            }
            return true;
        }
        static Int32 NextThreeDigits(Int64 pandigital,Int32 startingIndex)
        {
            string digits = pandigital.ToString();
            if (!digits.Contains('0'))
            {
                digits = '0' + digits;
            }
            Int32 a = Int32.Parse(digits[startingIndex].ToString());
            Int32 b = Int32.Parse(digits[startingIndex+1].ToString());
            Int32 c = Int32.Parse(digits[startingIndex+2].ToString());
            return 100 * a + 10 * b + c;
        }
        static List<Int64> GenerateNDigitPandigitals(Int32 n)
        {
            List<char> input = new List<char>();
            List<Int64> output = new List<Int64>();
            for (Int32 i = 0; i <= n; i++)
            {
                input.Add(i.ToString()[0]);
            }
            Int64 sizeOfOutput = Factorial(n+1);
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