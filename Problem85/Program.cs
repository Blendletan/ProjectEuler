namespace Problem85
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = Int64.Parse(Console.ReadLine());
                Int64 nextOutput = Solve(nextInput);
                Console.WriteLine(nextOutput);
            }
        }
        static Int64 Solve(Int64 input)
        {
            Int64 maxX = (Int64)(2 * Math.Sqrt(input));
            Int64? smallestError = null;
            Int64? winningArea = null;
            for (Int64 x = 1; x <= maxX; x++)
            {
                Int64 y = (Int64)Math.Sqrt((4 * input) / (x * (x + 1)));
                Int64 currentGuess = (x * y * (x + 1) * (y + 1)) / 4;
                Int64 currentError = Math.Abs(input - currentGuess);
                if (smallestError == null)
                {
                    smallestError = currentError;
                    winningArea = x * y;
                }
                if (currentError < smallestError)
                {
                    smallestError = currentError;
                    winningArea = x * y;
                }
                if (currentError == smallestError)
                {
                    Int64 currentArea = x * y;
                    if (currentArea > winningArea)
                    {
                        winningArea = currentArea;
                    }
                }
            }
            if (winningArea == null)
            {
                throw new Exception("Uh oh");
            }
            return winningArea.Value;
        }
    }
}
