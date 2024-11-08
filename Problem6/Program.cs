namespace Problem6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                Int64 output = SumOfIntegers(input) * SumOfIntegers(input) - SumOfSquares(input);
                Console.WriteLine(output);
            }
        }
        static Int64 SumOfSquares(Int64 n)
        {
            return (n * (n + 1) * (2 * n + 1)) / 6;
        }
        static Int64 SumOfIntegers(Int64 n)
        {
            return n * (n + 1) / 2;
        }
    }
}
