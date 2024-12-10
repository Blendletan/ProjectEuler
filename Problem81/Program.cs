namespace Problem81
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 N = Int32.Parse(Console.ReadLine());
            Int32[,] inputMatrix = new Int32[N, N];
            for (Int32 i = 0; i < N; i++)
            {
                string[] nextLine = Console.ReadLine().Split();
                for (Int32 j = 0; j < N; j++)
                {
                    inputMatrix[i, j] = Int32.Parse(nextLine[j]);
                }
            }
            Int64[,] outputMatrix = new Int64[N, N];
            outputMatrix[0, 0] = inputMatrix[0, 0];
            for (Int32 i = 1; i < N; i++)
            {
                Int64 left;
                Int64 up;
                Int64 toAdd;
                outputMatrix[0, i] = inputMatrix[0, i] + outputMatrix[0, i - 1];
                outputMatrix[i, 0] = inputMatrix[i, 0] + outputMatrix[i - 1, 0];
                for (Int32 j = 1; j < i; j++)
                {
                    left = outputMatrix[j, i - 1];
                    up = outputMatrix[j - 1, i];
                    toAdd = Math.Min(up, left);
                    outputMatrix[j, i] = inputMatrix[j, i] + toAdd;
                    left = outputMatrix[i, j - 1];
                    up = outputMatrix[i - 1, j];
                    toAdd = Math.Min(up, left);
                    outputMatrix[i, j] = inputMatrix[i, j] + toAdd;
                }
                left = outputMatrix[i, i - 1];
                up = outputMatrix[i - 1, i];
                toAdd = Math.Min(up, left);
                outputMatrix[i, i] = inputMatrix[i, i] + toAdd;
            }
            Console.WriteLine(outputMatrix[N - 1, N - 1]);
        }
    }
}