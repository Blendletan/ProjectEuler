namespace Problem15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            (Int32, Int32)[] inputs = new (Int32, Int32)[numberOfCases];
            Int32 maxInput = 0;
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                string[] nextInput = Console.ReadLine().Split();
                Int32 x = Int32.Parse(nextInput[0]);
                Int32 y = Int32.Parse(nextInput[1]);
                if (x > maxInput)
                {
                    maxInput = x;
                }
                if (y > maxInput)
                {
                    maxInput = y;
                }
                inputs[i] = (x, y);
            }
            Int64 remainder = 1000000007;
            Int64[,] latticePoints = new Int64[maxInput + 1, maxInput + 1];
            latticePoints[0, 0] = 1;
            for (Int32 i = 0; i <= maxInput; i++)
            {
                latticePoints[0, i] = 1;
                latticePoints[i, 0] = i;
            }
            for (Int64 i = 1; i <= maxInput; i++)
            {
                for (Int32 j = 1; j <= maxInput; j++)
                {
                    latticePoints[j, i] = (latticePoints[j - 1, i] + latticePoints[j, i - 1]) % remainder;
                    latticePoints[i, j] = latticePoints[j, i];
                }
            }
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 x = inputs[i].Item1;
                Int32 y = inputs[i].Item2;
                Console.WriteLine(latticePoints[x, y]);
            }

        }
    }
}
