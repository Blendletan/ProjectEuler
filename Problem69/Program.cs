using System.Numerics;
namespace Problem69
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            BigInteger[] inputs = new BigInteger[numberOfCases];
            BigInteger max = 0;
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                BigInteger nextInput = BigInteger.Parse(Console.ReadLine());
                if (nextInput > max)
                {
                    max = nextInput;
                }
                inputs[i] = nextInput;
            }
            List<BigInteger> primorials = GeneratePrimorials(max);
            primorials.Reverse();
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                BigInteger nextInput = inputs[i];
                Console.WriteLine(Solve(nextInput, primorials));
            }
        }
        static BigInteger Solve(BigInteger input, List<BigInteger> primorials)
        {
            foreach(var v in primorials)
            {
                if (input > v)
                {
                    return v;
                }
            }
            return -1;
        }
        static List<BigInteger> GeneratePrimorials(BigInteger max)
        {
            const Int32 maxPrimeSize = 100;
            bool[] isPrime = new bool[maxPrimeSize+1];
            for(Int32 i = 2; i <= maxPrimeSize; i++)
            {
                isPrime[i] = true;
            }
            for(Int32 prime = 2; prime <= maxPrimeSize; prime++)
            {
                if(isPrime[prime])
                {
                    for(Int32 composite = 2 * prime;composite <= maxPrimeSize; composite += prime)
                    {
                        isPrime[composite] = false;
                    }
                }
            }
            List<BigInteger> output = new List<BigInteger> ();
            BigInteger currentPrimorial = 1;
            for(Int32 i = 2; i <= maxPrimeSize; i++)
            {
                if (isPrime[i])
                {
                    currentPrimorial *= i;
                    if (currentPrimorial > max)
                    {
                        break;
                    }
                    output.Add((BigInteger)currentPrimorial);
                }
            }
            return output;
        }
    }
}