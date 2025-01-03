using System.Diagnostics;
namespace Problem88
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 maxK = Int32.Parse(Console.ReadLine());
            Solve(maxK);
        }
        static void Solve(Int32 maxK)
        {
            Int32 maxN = 1000 + maxK;
            if (maxK < 50000)
            {
                maxN = maxK + 10000;
            }
            var divisorInfo = new DivisorInfo(maxN);
            var minProdSum = new Int32[maxK + 1];
            for (Int32 i = 0; i <= maxK; i++)
            {
                minProdSum[i] = -1;
            }
            for (Int32 i = 2; i <= maxN; i++)
            {
                ProductSumNumber(i, i, i,2, 0, divisorInfo.divisorList, minProdSum);
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
        static void UpdateProdSum(Int32 n, Int32 numberOfSteps, Int32[] minProdSum)
        {
            if (numberOfSteps > minProdSum.Length - 1)
            {
                return;
            }
            if (minProdSum[numberOfSteps] == -1)
            {
                minProdSum[numberOfSteps] = n;
            }
            else if (n < minProdSum[numberOfSteps])
            {
                minProdSum[numberOfSteps] = n;
            }
            return;
        }
        static void ProductSumNumber(Int32 originalInput, Int32 prod, Int32 sum, Int32 previousDivisor,Int32 numberOfSteps, List<(Int32, Int32)>[] divisorList, Int32[] minProdSum)
        {
            if (numberOfSteps > originalInput / 2 + 1)
            {
                return;
            }
            if (numberOfSteps > minProdSum.Length - 1)
            {
                return;
            }
            if (sum <= 0)
            {
                return;
            }
            if (numberOfSteps > 0)
            {
                if (prod == sum)
                {
                    UpdateProdSum(originalInput, numberOfSteps + 1, minProdSum);
                    return;
                }
                if (prod == 1)
                {
                    UpdateProdSum(originalInput, numberOfSteps + sum, minProdSum);
                    return;
                }
            }
            numberOfSteps++;
            foreach (var v in divisorList[prod])
            {
                var divisor = v.Item1;
                if (divisor > sum)
                {
                    break;
                }
                if (divisor < previousDivisor)
                {
                    continue;
                }
                ProductSumNumber(originalInput, v.Item2, sum - divisor, divisor,numberOfSteps, divisorList, minProdSum);
            }
            return;
        }
    }
    internal class DivisorInfo
    {
        public readonly List<(Int32, Int32)>[] divisorList;
        public DivisorInfo(Int32 max)
        {
            divisorList = new List<(Int32, Int32)>[max + 1];
            for (Int32 i = 0; i <= max; i++)
            {
                divisorList[i] = new List<(Int32, Int32)>();
                divisorList[i].Add((i, 1));
            }
            Int32 maxToSearch = (Int32)Math.Sqrt(max);
            for (Int32 divisor = 2; divisor <= maxToSearch; divisor++)
            {
                for (Int32 composite = divisor * divisor; composite <= max; composite += divisor)
                {
                    divisorList[composite].Add((divisor, composite / divisor));
                }
            }
            return;
        }
    }
}
