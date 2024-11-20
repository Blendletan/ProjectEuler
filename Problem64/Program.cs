namespace Problem64
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 N = Int32.Parse(Console.ReadLine());
            Int32 totalOddPeriods = 0;
            for (Int32 i = 2; i <= N; i++)
            {
                if (IsSquare(i))
                {
                    continue;
                }
                Int32 period = SquareRootContinuedFraction(i).Count;
                if (period % 2 == 1)
                {
                    totalOddPeriods++;
                }
            }
            Console.WriteLine(totalOddPeriods);
        }
        static bool IsSquare(Int32 N)
        {
            Int32 sqrtN = (Int32)Math.Sqrt((double)N);
            if (sqrtN * sqrtN == N)
            {
                return true;
            }
            return false;
        }
        static List<Int32> SquareRootContinuedFraction(Int32 N)
        {
            Int32 sqrtN = (Int32)Math.Sqrt((double)N);
            List<(Int32, Int32, Int32)> outputSequence = new List<(Int32, Int32, Int32)>();
            Int32 mNext = 0;
            Int32 dNext = 1;
            Int32 nextOutput = sqrtN;
            outputSequence.Add((mNext, dNext, nextOutput));
            while (true)
            {
                mNext = dNext * nextOutput - mNext;
                dNext = (N - mNext * mNext) / dNext;
                nextOutput = (sqrtN + mNext) / dNext;
                if (outputSequence.Contains((mNext, dNext, nextOutput)))
                {
                    Int32 startOfPeriodIndex = outputSequence.IndexOf((mNext, dNext, nextOutput));
                    for (Int32 i = 0; i < startOfPeriodIndex; i++)
                    {
                        outputSequence.RemoveAt(0);
                    }
                    List<Int32> output = new List<Int32>();
                    foreach (var v in outputSequence)
                    {
                        Int32 i = v.Item3;
                        output.Add(i);
                    }
                    return output;
                }
                outputSequence.Add((mNext, dNext, nextOutput));
            }
        }



    }
}
