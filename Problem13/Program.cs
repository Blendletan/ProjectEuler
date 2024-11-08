namespace Problem13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int64 runningSum = 0;
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                string nextInput = Console.ReadLine();
                runningSum += FirstNDigits(nextInput, 11);
            }
            Console.WriteLine(FirstNDigits(runningSum.ToString(), 10));
        }
        static Int64 FirstNDigits(string number, Int32 length)
        {
            char[] digits = new char[length];
            for (Int32 i = 0; i < length; i++)
            {
                digits[i] = number[i];
            }
            return Int64.Parse(digits);
        }
    }
}
