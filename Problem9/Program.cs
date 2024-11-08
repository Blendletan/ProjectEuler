namespace Problem9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int64[] pythagoreanList = LargestPythagoreanTriples(3001);
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                Console.WriteLine(pythagoreanList[input]);
            }
        }
        static Int64[] LargestPythagoreanTriples(Int32 upperLimit)
        {
            Int64[] output = new long[upperLimit + 1];
            for (Int32 i = 0; i <= upperLimit; i++)
            {
                output[i] = -1;
            }
            for (Int32 k = 1; k <= upperLimit; k++)
            {
                for (Int32 n = 1; n <= upperLimit / k; n++)
                {
                    for (Int32 m = n; m <= upperLimit / (n * k); m++)
                    {
                        Int32 a = k * (m * m - n * n);
                        Int32 b = 2 * k * m * n;
                        Int32 c = k * (m * m + n * n);
                        Int32 index = a + b + c;
                        Int64 product = (Int64)a * (Int64)b * (Int64)c;
                        if (index <= upperLimit && product > 0)
                        {
                            if (product > output[index])
                            {
                                output[index] = product;
                            }
                        }
                    }
                }
            }
            return output;
        }
    }
}
