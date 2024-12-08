namespace Problem71
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                (Int128, Int128, Int128) nextInput = ParseInput(Console.ReadLine());
                string output = Solve(nextInput.Item1, nextInput.Item2, nextInput.Item3);
                Console.WriteLine(output);
            }
        }
        static string Solve(Int128 c, Int128 d, Int128 maxDenominator)
        {
            Int128 b = maxDenominator;
            Int128 remainder = (b * c - 1) % d;
            Int128 difference = d - c;
            Int128 predictedStepNumber = ((d - remainder) * ModularDivision(difference, d)) % d;
            if (predictedStepNumber < 0)
            {
                predictedStepNumber = d + predictedStepNumber;
            }
            remainder += difference * predictedStepNumber;
            b = maxDenominator - predictedStepNumber - 1;
            Int128 a = (b * c - 1) / d;
            a++;
            b++;
            return a.ToString() + " " + b.ToString();
        }
        static Int128 ModularDivision(Int128 a, Int128 b)
        {
            Int128 previousX = 1;
            Int128 currentX = 0;
            Int128 currentRemainder = b;
            Int128 previousRemainder = a;
            while (true)
            {
                if (currentRemainder == 0)
                {
                    break;
                }
                Int128 quotient = previousRemainder / currentRemainder;
                Int128 nextRemainder = previousRemainder - quotient * currentRemainder;
                Int128 nextX = previousX - quotient * currentX;
                previousRemainder = currentRemainder;
                currentRemainder = nextRemainder;
                previousX = currentX;
                currentX = nextX;
            }
            if (previousX < 0)
            {
                previousX += b;
            }
            return previousX;
        }
        static (Int128, Int128, Int128) ParseInput(string input)
        {
            string[] inputs = input.Split();
            Int128 a = Int128.Parse(inputs[0]);
            Int128 b = Int128.Parse(inputs[1]);
            Int128 c = Int128.Parse(inputs[2]);
            return (a, b, c);
        }
    }
}