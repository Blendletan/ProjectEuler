namespace Problem8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                string[] firstLineOfInput = Console.ReadLine().Split();
                Int32 numberOfConsecutiveDigits = Int32.Parse(firstLineOfInput[1]);
                string inputNumber = Console.ReadLine();
                Console.WriteLine(MaxConsecutiveProduct(inputNumber, numberOfConsecutiveDigits));
            }
        }
        static Int32[] ConvertToDigits(string input, Int32 length)
        {
            Int32[] output = new Int32[length];
            char[] inputArray = input.ToCharArray();
            for (Int32 i = 0; i < length; i++)
            {
                output[i] = (Int32)(inputArray[i]) - 48;
            }
            return output;
        }
        static Int64 ConsecutiveProduct(Int32[] digits, Int32 numberOfConsecutiveDigits, Int32 startIndex)
        {
            Int64 output = 1;
            for (Int32 i = startIndex; i < startIndex + numberOfConsecutiveDigits; i++)
            {
                output *= digits[i];
            }
            return output;
        }
        static Int64 MaxConsecutiveProduct(string number, Int32 numberOfConsecutiveDigits)
        {
            Int32 length = number.Length;
            Int32[] digits = ConvertToDigits(number, length);
            Int64 maxProduct = 0;
            for (Int32 i = 0; i <= length - numberOfConsecutiveDigits; i++)
            {
                Int64 currentProduct = ConsecutiveProduct(digits, numberOfConsecutiveDigits, i);
                if (currentProduct > maxProduct)
                {
                    maxProduct = currentProduct;
                }
            }
            return maxProduct;
        }
    }
}
