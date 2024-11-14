namespace Problem34
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32[] digitFactorials = { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };
            Int32 n = Int32.Parse(Console.ReadLine());
            Int64 sum = 0;
            for (Int32 i = 19; i < n; i++)
            {
                Int64 x = DigitSum(i, digitFactorials) % i;
                if (x == 0)
                {
                    sum += i;
                }
            }
            Console.WriteLine(sum);
        }
        static Int64 DigitSum(Int32 n, Int32[] digitFactorials)
        {
            Int64 sum = 0;
            char[] digitLetters = n.ToString().ToCharArray();
            Int32 length = digitLetters.Length;
            for (Int32 i = 0; i < length; i++)
            {
                string digit = digitLetters[i].ToString();
                sum += digitFactorials[Int32.Parse(digit)];
            }
            return sum;
        }
    }
}
