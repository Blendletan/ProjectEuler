namespace Problem26
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(Int32, Int32)> inputs = new List<(Int32, Int32)>();
            Int32[] outputs = new int[numberOfCases];
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                inputs.Add((i, nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            Int32 currentIndex = 2;
            Int32 maxLength = 0;
            Int32 maxLengthIndex = 2;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = inputs[i].Item2;
                Int32 outputLocation = inputs[i].Item1;
                while (true)
                {
                    if (currentIndex >= nextInput)
                    {
                        break;
                    }
                    Int32 currentLength = CycleLenth(currentIndex);
                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        maxLengthIndex = currentIndex;
                    }
                    currentIndex++;
                }
                outputs[outputLocation] = maxLengthIndex;
            }
            foreach (Int32 outputValue in outputs)
            {
                Console.WriteLine(outputValue);
            }
        }
        static Int32 RemovePrimeFactors(Int32 n, Int32 p)
        {
            if (n % p != 0)
            {
                return n;
            }
            return RemovePrimeFactors(n / p, p);
        }
        static Int32 CycleLenth(Int32 input)
        {
            input = RemovePrimeFactors(input, 2);
            input = RemovePrimeFactors(input, 5);
            if (input == 1)
            {
                return 0;
            }
            Int32 powerOfTen = 10;
            Int32 output = 1;
            while (true)
            {
                if (powerOfTen % input == 1)
                {
                    return output;
                }
                output++;
                powerOfTen = (10 * powerOfTen) % input;
            }
        }
    }
}