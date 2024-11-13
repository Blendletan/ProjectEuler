namespace Problem39
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int32 max = 0;
            List<(Int32, Int32)> inputs = new List<(Int32, Int32)>();
            Int32[] outputs = new int[numberOfCases];
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > max)
                {
                    max = nextInput;
                }
                inputs.Add((i, nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            Int32[] numberOfTriangles = new Int32[max + 1];
            for (Int32 k = 1; k <= max; k++)
            {
                for (Int32 n = 1; n <= max / k; n++)
                {
                    for (Int32 m = n; m <= max / (n * k); m++)
                    {
                        Int32 a = k * (m * m - n * n);
                        Int32 b = 2 * k * m * n;
                        Int32 c = k * (m * m + n * n);
                        if ((a+b+c) <= max)
                        {
                            numberOfTriangles[a + b + c]++;
                        }
                    }
                }
            }
            Int32 currentIndex = 0;
            Int32 currentMax = 0;
            Int32 maxIndex = 0;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 nextInput = inputs[i].Item2;
                Int32 outputLocation = inputs[i].Item1;
                while (true)
                {
                    if (currentIndex > nextInput)
                    {
                        break;
                    }
                    if (numberOfTriangles[currentIndex] > currentMax)
                    {
                        maxIndex = currentIndex;
                        currentMax = numberOfTriangles[currentIndex];
                    }
                    currentIndex++;
                }
                outputs[outputLocation] = maxIndex;
            }
            foreach(var v in outputs)
            {
                Console.WriteLine(v);
            }
        }
    }
}
