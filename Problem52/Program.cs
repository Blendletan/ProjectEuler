namespace Problem52
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            Int32 N = Int32.Parse(input[0]);
            Int32 K = Int32.Parse(input[1]);
            List<Int32> output = new List<Int32>();
            for(Int32 i = 1; i <= N; i++)
            {
                if (TestInteger(i,K))
                {
                    output.Add(i);
                }
            }
            foreach(var v in output)
            {
                for(Int32 i = 1; i <= K; i++)
                {
                    Console.Write($"{v*i} ");
                }
                Console.WriteLine();
            }
        }
        static bool TestInteger(Int32 n, Int32 K)
        {
            for(Int32 k = 2; k <= K; k++)
            {
                if (!IsAdmissible(n, k * n))
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsAdmissible(Int32 a,Int32 b)
        {
            List<Int32> aDigits = GetDigits(a);
            List<Int32> bDigits = GetDigits(b);
            if (aDigits.Count != bDigits.Count)
            {
                return false;
            }
            aDigits.Sort();
            bDigits.Sort();
            for(Int32 i = 0; i < aDigits.Count; i++)
            {
                if (aDigits[i] != bDigits[i])
                {
                    return false;
                }
            }
            return true;
        }
        static List<Int32> GetDigits(Int32 n)
        {
            List<Int32> output = new List<Int32>();
            while (true)
            {
                Int32 nextDigit = n % 10;
                output.Add(nextDigit);
                if (n < 10)
                {
                    return output;
                }
                n = n / 10;
            }
        }
    }
}
