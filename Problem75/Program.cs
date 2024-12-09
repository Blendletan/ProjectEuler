namespace Problem75
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int32 maxInput = 0;
            List<(Int32, Int32)> inputs = new List<(Int32,Int32)>();
            Int64[] outputs = new Int64[numberOfCases];
            for(Int32 inputIndex = 0; inputIndex < numberOfCases; inputIndex++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine());
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs.Add((nextInput, inputIndex));
            }
            inputs.Sort();
            Triangles t = new Triangles(maxInput);
            Int32 currentIndex = 0;
            Int32 foundTriangles = 0;
            foreach(var input in inputs)
            {
                Int32 stoppingIndex = input.Item1;
                while (true)
                {
                    if (currentIndex > stoppingIndex)
                    {
                        break;
                    }
                    if (t.triangleCount[currentIndex] == 1)
                    {
                        foundTriangles++;
                    }
                    currentIndex++;
                }
                outputs[input.Item2] = foundTriangles;
            }
            foreach (var output in outputs)
            {
                Console.WriteLine(output);
            }
        }
    }
    internal class Triangles
    {
        Int32 maxSize;
        public readonly Int64[] triangleCount;
        HashSet<(Int64, Int64)> foundPairs;
        public Triangles(Int32 max)
        {
            maxSize = max;
            triangleCount = new Int64[maxSize + 1];
            foundPairs=new HashSet<(Int64,Int64)> ();
            for (Int64 n = 1; n <= max; n++)
            {
                if (2 * n * n > maxSize)
                {
                    continue;
                }
                for (Int64 m = n+1; m <= maxSize; m++)
                {
                    if (GCD(n,m) != 1)
                    {
                        continue;
                    }
                    Int64 e = 1;
                    if (m % 2 == 1 && n % 2 == 1)
                    {
                        e = 2;
                    }
                    Int64 a = 2 * m * n/e;
                    Int64 b = (m * m - n * n)/e;
                    Int64 c = (m * m + n * n)/e;
                    if (foundPairs.Contains((b, a)))
                    {
                        continue;
                    }
                    Int64 perimeter = a + b + c;
                    if (perimeter < maxSize)
                    {
                        triangleCount[perimeter]++;
                        foundPairs.Add((a, b));
                    }
                    else
                    {
                        break;
                    }
                    for (Int32 i = 2; i <= maxSize; i++)
                    {
                        Int64 newPerimeter = i * perimeter;
                        if (newPerimeter > maxSize)
                        {
                            break;
                        }
                        triangleCount[newPerimeter]++;
                    }
                }
            }
        }
        Int64 GCD(Int64 a,Int64 b)
        {
            if (b > a)
            {
                Int64 temp = a;
                a = b;
                b = temp;
            }
            while (true)
            {
                if (b == 0)
                {
                    return a;
                }
                Int64 temp = a % b;
                a = b;
                b = temp;
            }
        }
    }
}
