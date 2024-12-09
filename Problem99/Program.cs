namespace Problem99
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(double, string)> inputs = new List<(double, string)>();
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                string nextInput = Console.ReadLine();
                double parsedInput = ParseInput(nextInput);
                inputs.Add((parsedInput, nextInput));
            }
            Int32 targetIndex = Int32.Parse(Console.ReadLine());
            inputs.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            string output = inputs[targetIndex - 1].Item2;
            Console.WriteLine(output);
        }
        static double ParseInput(string input)
        {
            string[] inputs = input.Split();
            Int64 b = Int64.Parse(inputs[0]);
            Int64 e = Int64.Parse(inputs[1]);
            double output = e * Math.Log10(b);
            return output;
        }
    }
}
