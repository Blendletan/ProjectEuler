using System.Numerics;

namespace Problem53
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 N = Int32.Parse(inputs[0]);
            bool[,] exceedsMax = new bool[N + 1, N + 1];
            BigInteger max = BigInteger.Parse(inputs[1]);
            for(Int32 i = 1; i <= N; i++)
            {
                for(Int32 k = 1; k <= i; k++)
                {
                    if (exceedsMax[i, k])
                    {
                        continue;
                    }
                    BigInteger binomialCoeffecient = NChooseK(i, k);
                    if (binomialCoeffecient > max)
                    {
                        // if i Choose k > max, so is j Choose k for any j > i
                        for(Int32 j = i; j <= N; j++)
                        {
                            exceedsMax[j, k] = true;
                        }
                    }
                }
            }
            Int32 outputSum = 0;
            for (Int32 i = 1; i <= N; i++)
            {
                for (Int32 k = 1; k <= i; k++)
                {
                    if (exceedsMax[i, k])
                    {
                        outputSum++;
                    }
                }
            }
            Console.WriteLine(outputSum);
        }
        static BigInteger NChooseK(Int32 n, Int32 k)
        {
            BigInteger output = 1;
            for(Int32 i = k + 1; i <= n; i++)
            {
                output *= i;
            }

            for(Int32 i = 1; i <= n-k; i++)
            {
                output /= i;
            }
            return output;
        }
    }
}
