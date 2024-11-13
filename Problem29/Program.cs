using System.Numerics;

namespace Problem29
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 max = Int32.Parse(Console.ReadLine());
            Int64 totalDistinctPowers = 0;
            HashSet<BigInteger> foundBases = new HashSet<BigInteger>();
            for (Int32 x = 2; x <= max; x++)
            {
                if (foundBases.Contains(x))
                {
                    continue;
                }
                HashSet<BigInteger> foundPowers = new HashSet<BigInteger>();
                for (Int32 y = 2; Math.Pow(x, y) <= max; y++)
                {
                    foundBases.Add(BigInteger.Pow(x, y));
                    for (Int32 m = 2 * y; m < y * (max + 1); m+=y)
                    {
                        if (m > max)
                        {
                            foundPowers.Add(m);
                        }
                    }
                }
                totalDistinctPowers += foundPowers.Count + max - 1;
            }
            Console.WriteLine(totalDistinctPowers);
        }
    }
}
