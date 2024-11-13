namespace Problem31
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int64 remainder = 1000000007;
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            Int32 largestInput = 100001;
            Int32[] coinValues = { 1, 2, 5, 10, 20, 50, 100, 200 };
            Int32 coinLength = coinValues.Length;
            Int64[] solution = CoinChange(largestInput, coinValues,remainder);
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                Int64 output = solution[input] % remainder;
                Console.WriteLine(output);
            }
        }
        static Int64[] CoinChange(Int32 max, Int32[] coinValues,Int64 remainder)
        {
            Int32 numberOfCoins = coinValues.Length;
            Int64[] solution = new Int64[max + 1];
            solution[0] = 1;
            for(Int32 i = 0; i < coinValues.Length; i++)
            {
                for(Int32 j = coinValues[i]; j <= max; j++)
                {
                    Int64 amountToAdd = solution[j - coinValues[i]] % remainder;
                    solution[j] += amountToAdd;
                }
            }
            return solution;
        }
    }
}
