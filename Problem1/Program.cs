namespace Problem1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfTestCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfTestCases; i++)
            {
                Int64 nextInput = Int64.Parse(Console.ReadLine());
                Console.WriteLine(SumOfMultiples(nextInput));
            }
        }
        static Int64 SumOfMultiples(Int64 input)
        {
            Int64 multiplesOfThree = 3 * SumOfNaturalNumbers((input - 1) / 3);
            Int64 multiplesOfFive = 5 * SumOfNaturalNumbers((input - 1) / 5);
            Int64 multiplesOfFifteen = 15 * SumOfNaturalNumbers((input - 1) / 15);
            return multiplesOfThree + multiplesOfFive - multiplesOfFifteen;
        }
        static Int64 SumOfNaturalNumbers(Int64 n)
        {
            return (n * (n + 1)) / 2;
        }
    }
}