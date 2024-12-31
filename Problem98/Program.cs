namespace Problem98
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfDigits = Int32.Parse(Console.ReadLine());
            var squareDictionary = GetSquares(numberOfDigits);
            Console.WriteLine(GetMaximum(squareDictionary));
        }
        static Int128 GetMaximum(Dictionary<string, List<Int128>> squareList)
        {
            Int128 winningSquare = 0;
            Int32 winningCount = 0;
            foreach (var v in squareList)
            {
                if (v.Value.Count > winningCount)
                {
                    winningCount = v.Value.Count;
                    winningSquare = v.Value.Max();
                }
                if (v.Value.Count == winningCount)
                {
                    if (v.Value.Max() > winningSquare)
                    {
                        winningSquare = v.Value.Max();
                    }
                }
            }
            return winningSquare;
        }
        static Dictionary<string, List<Int128>> GetSquares(Int32 numberOfDigits)
        {
            var output = new Dictionary<string, List<Int128>>();
            Int128 min = PowerOfTen((numberOfDigits - 1) / 2);
            Int128 max = PowerOfTen((numberOfDigits - 1) / 2 + 1);
            Int128 squareMin = PowerOfTen(numberOfDigits - 1);
            Int128 squareMax = PowerOfTen(numberOfDigits);
            for (Int128 i = min; i < max; i++)
            {
                Int128 nextSquare = i * i;
                if (nextSquare > squareMax || nextSquare < squareMin)
                {
                    continue;
                }
                string hash = GetHash(nextSquare);
                if (output.ContainsKey(hash) == false)
                {
                    output.Add(hash, new List<Int128>());
                }
                output[hash].Add(nextSquare);
            }
            return output;
        }
        static string GetHash(Int128 integer)
        {
            var v = integer.ToString().ToCharArray().ToList();
            v.Sort();
            return new string(v.ToArray());
        }
        static Int128 PowerOfTen(Int32 power)
        {
            Int128 output = 1;
            for (Int32 i = 0; i < power; i++)
            {
                output *= 10;
            }
            return output;
        }
    }
}
