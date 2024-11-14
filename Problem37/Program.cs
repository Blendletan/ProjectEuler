namespace Problem37
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 n = Int32.Parse(Console.ReadLine());
            bool[] isPrime = PrimeSieve(n);
            Int32 sum = 0;
            for (Int32 i = 10; i <= n; i++)
            {
                if (IsTruncatablePrime(i, isPrime))
                {
                    sum += i;
                }
            }
            Console.WriteLine(sum);
        }
        static bool IsTruncatablePrime(Int32 n, bool[] isPrime)
        {
            List<Int32> truncatesList = LeftTruncates(n);
            foreach (Int32 i in truncatesList)
            {
                if (!isPrime[i])
                {
                    return false;
                }
            }
            truncatesList = RightTruncates(n);
            foreach (Int32 i in truncatesList)
            {
                if (!isPrime[i])
                {
                    return false;
                }
            }
            return true;
        }

        static bool[] PrimeSieve(Int32 n)
        {
            bool[] output = new bool[n + 1];
            Int32 sqrtN = (Int32)Math.Sqrt(n);
            output[0] = false;
            output[1] = false;
            for (Int32 i = 2; i <= n; i++)
            {
                output[i] = true;
            }
            for (Int32 i = 2; i <= sqrtN; i++)
            {
                if (output[i])
                {
                    for (Int32 j = 2 * i; j <= n; j += i)
                    {
                        output[j] = false;
                    }
                }
            }
            return output;
        }
        static List<Int32> LeftTruncates(Int32 n)
        {
            List<char> digits = n.ToString().ToList();
            List<Int32> output = new List<Int32>();
            Int32 length = digits.Count;
            for (Int32 i = 0; i < length; i++)
            {
                string nextNumber = new string(digits.ToArray());
                output.Add(Int32.Parse(nextNumber));
                digits.RemoveAt(0);
            }
            return output;
        }
        static List<Int32> RightTruncates(Int32 n)
        {
            List<char> digits = n.ToString().ToList();
            List<Int32> output = new List<Int32>();
            Int32 length = digits.Count;
            for (Int32 i = 0; i < length; i++)
            {
                string nextNumber = new string(digits.ToArray());
                output.Add(Int32.Parse(nextNumber));
                digits.RemoveAt(digits.Count - 1);
            }
            return output;
        }
    }
}
