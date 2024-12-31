using System.Numerics;
namespace Problem94
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            List<(BigInteger, Int32)> inputs = new List<(BigInteger, Int32)>();
            BigInteger maxInput = 0;
            BigInteger[] outputs = new BigInteger[numberOfCases];
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                BigInteger nextInput = BigInteger.Parse(Console.ReadLine());
                if (nextInput > maxInput)
                {
                    maxInput = nextInput;
                }
                inputs.Add((nextInput, i));
            }
            var triangles = ValidTriangles(maxInput);
            inputs.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            BigInteger output = 0;
            Int32 index = 0;
            foreach (var v in inputs)
            {
                BigInteger nextInput = v.Item1;
                Int32 outputLocation = v.Item2;
                while (true)
                {
                    if (index >= triangles.Count)
                    {
                        break;
                    }
                    var nextTriangle = triangles[index];
                    BigInteger perimeter = Perimeter(nextTriangle);
                    if (perimeter > nextInput)
                    {
                        break;
                    }
                    output += perimeter;
                    index++;
                }
                outputs[outputLocation] = output;
            }
            foreach (var v in outputs)
            {
                Console.WriteLine(v);
            }
        }
        static BigInteger Perimeter((BigInteger, BigInteger) input)
        {
            return 2 * input.Item1 + input.Item2;
        }
        static List<(BigInteger, BigInteger)> ValidTriangles(BigInteger max)
        {
            var output = new List<(BigInteger, BigInteger)>();
            output.Add((5, 6));
            output.Add((17, 16));
            output.Add((65, 66));
            Int32 index = 0;
            while (true)
            {
                BigInteger a = 3 * output[index + 2].Item1 + 3 * output[index + 1].Item1 - output[index].Item1;
                if (3 * a - 1 > max)
                {
                    break;
                }
                BigInteger b = a - 1;
                if (index % 2 == 1)
                {
                    b = a + 1;
                }
                output.Add((a, b));
                index++;
            }
            return output;
        }
    }
}