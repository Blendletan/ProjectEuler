namespace Problem30
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 exponent = Int32.Parse(Console.ReadLine());
            Int32 max = 2 * (Int32)Math.Pow(10, exponent);
            Int32 sum = 0;
            for(Int32 i = 2; i <= max; i++)
            {
                if (PowerSum(i,exponent) == i)
                {
                    sum += i;
                }
            }
            Console.WriteLine(sum);
        }
        static Int32 PowerSum(Int32 n,Int32 exponent)
        {
            List<Int32> digits = new List<Int32>();
            while (true)
            {
                Int32 nextDigit = n % 10;
                digits.Add(nextDigit);
                if (n < 10)
                {
                    break;
                }
                n = n / 10;
            }
            Int32 sum = 0;
            foreach (Int32 d in digits)
            {
                sum += (Int32)Math.Pow(d, exponent);
            }
            return sum;
        }
    }
}
