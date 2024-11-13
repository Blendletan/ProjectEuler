using System.Numerics;

namespace Problem63
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 N = Int32.Parse(Console.ReadLine());
            List<BigInteger> output = new List<BigInteger>();
            for(Int32 i = 1; i <= 9; i++)
            {
                BigInteger pow = BigInteger.Pow(i, N);
                if (pow > BigInteger.Pow(10,N-1) && pow < BigInteger.Pow(10, N))
                {
                    output.Add(pow);
                }
            }
            foreach( var v in output)
            {
                Console.WriteLine(v);
            }
        }
    }
}
