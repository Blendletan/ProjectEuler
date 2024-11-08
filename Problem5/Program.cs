namespace Problem5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                Console.WriteLine(ComputeAnswer(input));
            }
        }
        static Int32 ComputeAnswer(Int32 input)
        {
            Int32[] listOfPrimes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
            Int32 output = 1;
            if (input >= 25) { output *= 5; }
            if (input >= 27) { output *= 3; }
            if (input >= 9) { output *= 3; }
            if (input >= 32) { output *= 2; }
            if (input >= 16) { output *= 2; }
            if (input >= 8) { output *= 2; }
            if (input >= 4) { output *= 2; }
            foreach (var p in listOfPrimes)
            {
                if (p <= input)
                {
                    output *= p;
                }
                else
                {
                    break;
                }
            }
            return output;
        }
    }
}
