using System.Numerics;

namespace Problem40
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Champernownes champ = new Champernownes(19);
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64[] indices = ParseInputLine();
                Int64 product = 1;
                for (Int32 j = 0; j < 7; j++)
                {
                    product *= champ.GetDigitSmart(indices[j]);
                }
                Console.WriteLine(product);
            }
        }
        static Int64[] ParseInputLine()
        {
            string[] inputs = Console.ReadLine().Split();
            Int64[] outputs = new Int64[7];
            for (Int32 i = 0; i < 7; i++)
            {
                outputs[i] = Int64.Parse(inputs[i]);
            }
            return outputs;
        }
    }
    internal class Champernownes
    {
        Int32 maxNumberOfDigits;
        readonly Int64[] startOfNDigitNumbers;
        public Champernownes(Int32 N)
        {
            maxNumberOfDigits = N;
            startOfNDigitNumbers = new Int64[maxNumberOfDigits + 1];
            Int64 sum = 1;
            for (Int32 i = 1; i <= maxNumberOfDigits; i++)
            {
                startOfNDigitNumbers[i] = sum;
                sum += i * 9 * (Int64)BigInteger.Pow(10, i - 1);
            }
        }
        public Int32 GetDigitSmart(Int64 index)
        {
            Int32 numberOfDigits = NumberOfDigits(index);
            Int64 number = CurrentNumber(index, numberOfDigits);
            Int64 digitIndex = GetCurrentDigit(index, numberOfDigits);
            char[] digits = new char[1];
            char digit = number.ToString().ToCharArray()[digitIndex];
            digits[0] = digit;
            string digitS = new string(digits);
            return Int32.Parse(digits);

        }
        Int32 NumberOfDigits(Int64 index)
        {
            Int32 numberOfDigits = 2;
            while (numberOfDigits < 20)
            {
                Int64 startLocation = startOfNDigitNumbers[numberOfDigits];
                if (startLocation > index)
                {
                    return numberOfDigits - 1;
                }
                numberOfDigits++;
            }
            return numberOfDigits;
        }
        Int64 CurrentNumber(Int64 index, int numberOfDigits)
        {
            Int64 number = index - startOfNDigitNumbers[numberOfDigits];
            number = number / numberOfDigits;
            number += (Int64)BigInteger.Pow(10, numberOfDigits - 1);
            return number;
        }
        int GetCurrentDigit(Int64 index, int numberOfDigits)
        {
            Int64 digitIndex = index - startOfNDigitNumbers[numberOfDigits];
            digitIndex = digitIndex % numberOfDigits;
            return (int)digitIndex;
        }
    }
}
