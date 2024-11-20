using System.Diagnostics;
using System.Numerics;

namespace Problem57
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Int32 n = Int32.Parse(Console.ReadLine());
            List<Int32> outputs=Solution(n);
            foreach (var v in outputs)
            {
                Console.WriteLine(v);
            }
            return;
        }
        static List<Int32> Solution(Int32 n)
        {
            List<Int32> outputs=new List<Int32>();
            BigInteger numerator = 3;
            BigInteger denominator = 2;
            Int32 previousOutputIndex = 0;
            for (Int32 iterationIndex = 2; iterationIndex <= n; iterationIndex++)
            {
                (BigInteger, BigInteger) nextValue = NextIteration(numerator, denominator);
                numerator = nextValue.Item1;
                denominator = nextValue.Item2;
                Int32 gap = iterationIndex - previousOutputIndex;
                if (gap == 3 || gap == 5 || gap == 8)
                {
                    numerator = nextValue.Item1;
                    denominator = nextValue.Item2;
                    if (NumberOfDigits(numerator) > NumberOfDigits(denominator))
                    {
                        outputs.Add(iterationIndex);
                        previousOutputIndex = iterationIndex;
                    }
                }
            }
            return outputs;
        }
        static Int32 NumberOfDigits(BigInteger n)
        {
            return (Int32)BigInteger.Log10(n);
        }
        static (BigInteger,BigInteger) NextIteration (BigInteger numerator,BigInteger denominator)
        {
            BigInteger newNumerator = 2 * denominator + numerator;
            BigInteger newDenominator = numerator + denominator;
            return (newNumerator, newDenominator);
        }
    }
}