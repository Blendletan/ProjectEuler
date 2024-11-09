namespace NextPermutation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string original = "abcdefghijklm";
            Int32 numberOfCases = int.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 input = Int64.Parse(Console.ReadLine());
                string output = new string(GetPermutation(original.ToList(), input - 1).ToArray());
                Console.WriteLine(output);
            }
        }
        static List<char> GetPermutation(List<char> inputString, Int64 permutationNumber)
        {
            Int32 length = inputString.Count();
            if (length <= 1)
            {
                return inputString;
            }
            if (length == 2 && permutationNumber > 0)
            {
                inputString.Reverse();
                return inputString;
            }
            List<char> result = new List<char>();
            Int64 factorial = Factorial(length - 1);
            Int64 a = permutationNumber / factorial;
            result.Add(inputString[(int)a]);
            inputString.RemoveAt((int)a);
            Int64 b = permutationNumber % factorial;
            result.AddRange(GetPermutation(inputString, b));
            return result;
        }
        static Int64 Factorial(Int32 n)
        {
            if (n == 0) { return 1; }
            if (n == 1) { return 1; }
            Int64 output = 1;
            for (Int64 i = 2; i <= n; i++)
            {
                output *= i;
            }
            return output;
        }
    }
}
