namespace Problem44
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 max = Int32.Parse(inputs[0]);
            Int32 k = Int32.Parse(inputs[1]);
            ulong[] pentagonals = GeneratePentagonals(max);
            GenerateValidPentagonals(pentagonals, k);
        }
        static void GenerateValidPentagonals(UInt64[] pentagonals, Int32 k)
        {
            for (Int32 i = 1; i < pentagonals.Length; i++)
            {
                UInt64 candidate = pentagonals[i];
                if (IsValid(i, k, pentagonals))
                {
                    Console.WriteLine(candidate);
                }
            }
        }
        static bool IsValid(Int32 index, Int32 k, UInt64[] pentagonalNumbers)
        {
            UInt64 input = pentagonalNumbers[index];
            Int32 newIndex = index - k;
            if (newIndex < 1) { return false; }
            UInt64 relativePentagonal = pentagonalNumbers[newIndex];
            UInt64 test = input + relativePentagonal;
            if (IsPentagonal(test)) { return true; }
            test = input - relativePentagonal;
            if (IsPentagonal(test)) { return true; }
            return false;
        }
        static bool IsPentagonal(UInt64 input)
        {
            UInt64 root = (UInt64)Math.Sqrt(input * 24 + 1);
            if (root * root != (24 * input + 1))
            {
                return false;
            }
            if (root % 6 != 5)
            {
                return false;
            }
            return true;
        }
        static UInt64[] GeneratePentagonals(Int32 max)
        {
            UInt64[] result = new UInt64[max + 1];
            for (Int32 i = 1; i <= max; i++)
            {
                UInt64 n = (UInt64)i;
                UInt64 output = n * (3 * n - 1);
                output = output / 2;
                result[i] = output;
            }
            return result;
        }
    }
}
