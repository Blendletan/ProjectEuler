using System.Diagnostics;
namespace Problem88
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 maxK = Int32.Parse(Console.ReadLine());
            SolveAll(maxK);
        }
        static void SolveAll(Int32 maxK)
        {
            var minProdSum = new Int32[maxK + 1];
            for (Int32 i = 0; i <= maxK; i++)
            {
                minProdSum[i] = -1;
            }
            Int32 maxN = 1000 + maxK;
            if (maxK < 50000)
            {
                maxN = maxK + 10000;
            }
            var factors = new List<Int32>[maxN + 1];
            for (Int32 i = 0; i <= maxN; i++)
            {
                factors[i] = new List<Int32>();
            }
            for (Int32 divisor = 2; divisor <= maxK; divisor++)
            {
                for (Int32 composite = divisor; composite <= maxN; composite+=divisor)
                {
                    factors[composite].Add(divisor);
                }
            }
            for (Int32 i = 2; i <= maxN; i++)
            {
                Solve(i, i, i, 0, 2, factors, maxN, maxK, minProdSum);
            }
            var used = new bool[maxN + 1];
            foreach (var v in minProdSum)
            {
                if (v != -1)
                {
                    used[v] = true;
                }
            }
            Int32 output = 0;
            for (Int32 i = 2; i <= maxN; i++)
            {
                if (used[i])
                {
                    output += i;
                }
            }
            Console.WriteLine(output);
        }

        static void Solve(Int32 start, Int32 prod, Int32 sum, Int32 size, Int32 prev,List <Int32>[] factors, Int32 maxN,Int32 maxK,Int32[] minProdSum)
        {
            if (prod == 1)
            {
                if (size == 1)
                {
                    return;
                }
                if (sum == 0)
                {
                    if (start < minProdSum[size] || minProdSum[size] == -1)
                    {
                        minProdSum[size] = start;
                    }
                }
                else if (size + sum <= maxK)
                {
                    if (minProdSum[size + sum] == -1)
                    {
                        minProdSum[size + sum] = start;
                    }
                    else
                    {
                        minProdSum[size + sum] = Math.Min(minProdSum[size + sum], start);
                    }
                }
                return;
            }
            if (size == maxN || size >= start / 2 + 1) return;

            foreach (var f in factors[prod])
            {
                if (f < prev)
                {
                    continue;
                }
                if (f > sum)
                {
                    break;
                }
                Solve(start, prod / f, sum - f, size + 1, f,factors,maxN,maxK,minProdSum);
            }
        }
    }
}