using System.Numerics;
namespace Problem91
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 n = Int32.Parse(Console.ReadLine());
            Int32 foundTriangles = 3 * n * n;
            var points = GeneratePoints(n);
            Int32 length = points.Count;
            foreach (var point in points)
            {
                Int32 newFoundTriangles=0;
                Int32 x = point.Item1;
                Int32 y = point.Item2;
                Int32 gcd = (Int32)BigInteger.GreatestCommonDivisor(x, y);
                (Int32, Int32) slope;
                if (gcd != 0)
                {
                    slope = (-point.Item2 / gcd, point.Item1 / gcd);
                }
                else
                {
                    slope = (-point.Item2, point.Item1);
                }
                (Int32, Int32) point2 = Add(point,slope);
                while (point2.Item1 >= 0 && point2.Item2 <= n)
                {
                    newFoundTriangles++;
                    point2 = Add(point2, slope);
                }
                slope = (-slope.Item1, -slope.Item2);
                point2 = Add(point, slope);
                while (point2.Item2 >= 0 && point2.Item1 <= n)
                {
                    newFoundTriangles++;
                    point2 = Add(point2, slope);
                }
                if (point.Item1 != point.Item2)
                {
                    newFoundTriangles *= 2;
                }
                foundTriangles += newFoundTriangles;
            }
            Console.WriteLine(foundTriangles);
        }
        static (Int32,Int32) Add((Int32, Int32) a, (Int32, Int32) b)
        {
            return ((a.Item1+b.Item1,a.Item2+b.Item2));
        }
        static List<(Int32, Int32)> GeneratePoints(Int32 max)
        {
            List<(Int32, Int32)> output = new List<(Int32,Int32)>();
            for (Int32 i = 1; i <= max; i++)
            {
                for (Int32 j = 1; j <= i; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    output.Add((i, j));
                }
            }
            return output;
        }

    }
}