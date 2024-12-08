using System.Collections.Concurrent;

namespace Problem78
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfTestCases = Int32.Parse(Console.ReadLine());
            Int32[] inputs = new Int32[numberOfTestCases];
            Int32 maxInput = 0;
            for (Int32 i = 0; i < numberOfTestCases; i++)
            {
                Int32 nextInput = Int32.Parse(Console.ReadLine()) + 1;
                if (nextInput > maxInput) { maxInput = nextInput; }
                inputs[i] = nextInput;
            }
            Partition p = new Partition(maxInput);
            for (Int32 i = 0; i < numberOfTestCases; i++)
            {
                Console.WriteLine(p.partitionFunction[inputs[i]]);
            }
        }
    }
    internal class Partition
    {
        readonly Int32 maxSize;
        public readonly Int64[] partitionFunction;
        const Int64 modulus = 1000000007;
        public Partition(Int32 N)
        {
            maxSize = N;
            partitionFunction = new Int64[maxSize + 1];
            partitionFunction[0] = 0;
            partitionFunction[1] = 1;
            for (Int32 i = 2; i <= maxSize; i++)
            {
                partitionFunction[i] = 0;
            }
            for (Int32 i = 2; i <= maxSize; i++)
            {
                partitionFunction[i] = ComputePartition(i, partitionFunction);
            }
        }
        static Int64 ComputePartition(Int32 n, Int64[] partitions)
        {
            Int32 parityBit = 1;
            Int64 sum = 0;
            for (Int32 i = 1; i <= n; i++)
            {
                Int32 leftIndex = n - (i * (3 * i - 1)) / 2;
                Int32 rightIndex = n - (i * (3 * i + 1)) / 2;
                if (leftIndex < 0 && rightIndex < 0)
                {
                    break;
                }
                if (leftIndex >= 0 && leftIndex < partitions.Length)
                {
                    Int64 leftSummand = parityBit * partitions[leftIndex];
                    if (leftSummand < 0)
                    {
                        while (leftSummand < 0)
                        {
                            leftSummand += modulus;
                        }
                    }
                    sum += leftSummand;
                    sum = sum % modulus;
                }
                if (rightIndex >= 0 && rightIndex < partitions.Length)
                {
                    Int64 rightSummand = parityBit * partitions[rightIndex];
                    if (rightSummand < 0)
                    {
                        while (rightSummand < 0)
                        {
                            rightSummand += modulus;
                        }
                    }
                    sum += rightSummand;
                    sum = sum % modulus;
                }
                parityBit *= -1;
            }
            return sum;
        }
    }
}
