namespace Problem42
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 input = Int64.Parse(Console.ReadLine());
                Console.WriteLine(GetTriangularNumber(input));
            }
        }
        static Int64 GetTriangularNumber(Int64 input)
        {
            Int64 output = (Int64)Math.Sqrt(2 * input);
            Int64 triangularNumber = output * (output + 1) / 2;
            if (triangularNumber == input)
            {
                return output;
            }
            return -1;
        }
    }
}
