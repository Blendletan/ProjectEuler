using System.Diagnostics;

namespace Problem39
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int64 numberOfCases = Int64.Parse(Console.ReadLine());
            List<(Int32, Int64)> inputs = new List<(Int32, Int64)>();
            Int64[] outputs = new long[numberOfCases];
            Int64 maxInput = 0;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 nextInput = Int64.Parse(Console.ReadLine());
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs.Add((i, nextInput));
            }
            inputs.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            HashSet<(Int64,Int64,Int64)> triangles = GeneratePythagoreanTriples(maxInput);
            Int64[] trianglesOfGivenPerimeter = new Int64[maxInput + 1];
            foreach (var v in triangles)
            {
                Int64 perimeter = v.Item1 + v.Item2 + v.Item3;
                trianglesOfGivenPerimeter[perimeter]++;
            }
            Int32 currentIndex = 0;
            Int64 currentMax = 0;
            Int32 maxIndex = 0;
            foreach (var input in inputs)
            {
                Int64 nextInput = input.Item2;
                Int32 outputLocation = input.Item1;
                while (true)
                {
                    if (currentIndex >= nextInput)
                    {
                        break;
                    }
                    currentIndex++;
                    if (trianglesOfGivenPerimeter[currentIndex] > currentMax)
                    {
                        currentMax = trianglesOfGivenPerimeter[currentIndex];
                        maxIndex = currentIndex;
                    }
                }
                outputs[outputLocation] = maxIndex;
            }
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                Console.WriteLine(outputs[i]);
            }
        }
        static HashSet<(Int64,Int64,Int64)> GeneratePythagoreanTriples(Int64 max)
        {
            HashSet<(Int64,Int64,Int64)> output = new HashSet<(Int64,Int64,Int64)>();
            for(Int64 m = 1; m < max; m++)
            {
                for(Int64 n = m + 1; n <= max; n++)
                {
                    if (2*n*m+2*n*n > max)
                    {
                        break;
                    }
                    if (GCD(m,n) != 1)
                    {
                        continue;
                    }
                    for(Int64 k = 1; k <= max; k++)
                    {
                        Int64 a = 2 * k * m * n;
                        Int64 b = k * ( n * n - m * m);
                        Int64 c = k * (m * m + n * n);
                        if (a+b+c > max)
                        {
                            break;
                        }
                        if (b < a)
                        {
                            Int64 temp = a;
                            a = b;
                            b = temp;
                        }
                        if (a>0 && !output.Contains((a, b, c)))
                        {
                            output.Add((a, b, c));
                        }
                    }
                }
            }
            return output;
        }
        static Int64 GCD(Int64 a, Int64 b)
        {
            if (b > a)
            {
                Int64 c = a;
                a = b;
                b = c;
            }
            while (true)
            {
                if (b == 0)
                {
                    return a;
                }
                Int64 c = b;
                b = a % b;
                a = c;
            }
        }
    }
}