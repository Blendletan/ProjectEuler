using System.Dynamic;

namespace Problem73
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 denominator = Int32.Parse(inputs[0]);
            Int32 maxDenominator = Int32.Parse(inputs[1]);
            Int128 firstIndex = FerayIndex(denominator, maxDenominator);
            Int128 secondIndex = FerayIndex(denominator+1, maxDenominator);
            Console.WriteLine(firstIndex-secondIndex-1);
        }
        static Int128 FerayIndex(Int32 denominator,Int32 maxDenominator)
        {
            Int128[] modifiedTotient = new Int128[maxDenominator+1];
            for (Int32 i = 0; i <= maxDenominator; i++)
            {
                modifiedTotient[i] = i / denominator;
            }
            for(Int32 i = 1; i <= maxDenominator; i++)
            {
                for(Int32 j = 2 * i; j <= maxDenominator; j += i)
                {
                    modifiedTotient[j] -= modifiedTotient[i];
                }
            }
            Int128 output = 0;
            foreach(var v in modifiedTotient)
            {
                output += v;
            }
            return output;
        }
    }
}
