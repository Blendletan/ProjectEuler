namespace Problem67
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfTests = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfTests; i++)
            {
                Console.WriteLine(MaxPathSum());
            }
        }
        static Int32 MaxPathSum()
        {
            Int32 numberOfRows = Int32.Parse(Console.ReadLine());
            List<Int32>[] inputTriangle = new List<Int32>[numberOfRows];
            for (Int32 i = 0; i < numberOfRows; i++)
            {
                List<Int32> nextRow = new List<Int32>();
                if (i == 0)
                {
                    Int32 next = Int32.Parse(Console.ReadLine());
                    nextRow.Add(next);
                    inputTriangle[i] = nextRow;
                    continue;
                }
                string[] inputs = Console.ReadLine().Split();
                for (Int32 j = 0; j <= i; j++)
                {
                    string s = inputs[j];
                    Int32 next = Int32.Parse(s);
                    if (j == 0)
                    {
                        next += inputTriangle[i - 1][0];
                    }
                    else if (j == i)
                    {
                        next += inputTriangle[i - 1].Last();
                    }
                    else
                    {
                        Int32 left = inputTriangle[i - 1][j - 1];
                        Int32 right = inputTriangle[i - 1][j];
                        Int32 toAdd = Math.Max(left, right);
                        next += toAdd;
                    }
                    nextRow.Add(next);
                }
                inputTriangle[i] = nextRow;
            }
            List<Int32> lastRow = inputTriangle[numberOfRows - 1];
            return lastRow.Max();
        }
    }
}
