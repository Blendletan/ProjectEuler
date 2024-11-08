namespace Problem18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(MaxPathSum());
            }
        }
        static Int64 MaxPathSum()
        {
            Int32 numberOfRows = Int32.Parse(Console.ReadLine());
            List<Int64> currentRow = ParseLine();
            List<Int64> previousRow = new List<Int64>(currentRow);
            for (Int32 i = 1; i < numberOfRows; i++)
            {
                currentRow = ParseLine();
                currentRow[0] += previousRow[0];
                currentRow[i] += previousRow[i - 1];
                for (Int32 j = 1; j < i; j++)
                {
                    currentRow[j] += Math.Max(previousRow[j - 1], previousRow[j]);
                }
                previousRow = new List<Int64>(currentRow);
            }
            return currentRow.Max();

        }
        static List<Int64> ParseLine()
        {
            List<Int64> output = new List<Int64>();
            string[] inputs = Console.ReadLine().Split();
            foreach (string s in inputs)
            {
                output.Add(Int64.Parse(s));
            }
            return output;
        }
    }
}
