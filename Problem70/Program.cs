using System.Data;
using System.Numerics;

namespace Problem70
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 n = Int32.Parse(Console.ReadLine());
            Int32[] totientValues = EulerTotient(n);
            List<(Int32, Int32)> admissableValues = new List<(Int32, Int32)>();
            for(Int32 i = 2; i < n; i++)
            {
                if (IsPermutation(totientValues[i], i))
                {
                    admissableValues.Add((i, totientValues[i]));
                }
            }
            Int32 minIndex = 0;
            (BigInteger, BigInteger) minValue = (n, 1);
            foreach (var v in admissableValues)
            {
                BigInteger numerator = new BigInteger(v.Item1);
                BigInteger denominator = new BigInteger(v.Item2);
                if (numerator * minValue.Item2< minValue.Item1* denominator)
                {
                    minValue = v;
                    minIndex = v.Item1;
                }
            }
            Console.WriteLine(minIndex);
        }
        static Int32[] EulerTotient(Int32 n)
        {
            bool[] isPrime = new bool[n + 1];
            Int32[] output = new Int32[n + 1];
            for(Int32 i = 0; i <= n; i++)
            {
                isPrime[i] = true;
                output[i] = i;
            }
            for(Int32 p = 2; p <= n; p++)
            {
                if (isPrime[p])
                {
                    output[p] *= (p - 1);
                    output[p] /= p;
                    for (Int32 c = 2 * p; c <= n; c += p)
                    {
                        isPrime[c] = false;
                        output[c] /= p;
                        output[c] *= (p - 1);
                    }
                }
            }
            return output;
        }
        static bool IsPermutation(Int32 a, Int32 b)
        {
            List<char> digitsA = a.ToString().ToCharArray().ToList();
            List<char> digitsB = b.ToString().ToCharArray().ToList();
            Int32 length = digitsA.Count;
            if (length != digitsB.Count)
            {
                return false;
            }
            digitsA.Sort();
            digitsB.Sort();
            for(Int32 i = 0; i < length; i++)
            {
                if (digitsA[i] != digitsB[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}