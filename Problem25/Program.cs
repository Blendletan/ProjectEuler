namespace Problem25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double c = 1 / Math.Sqrt(5);
            double a = (1 + Math.Sqrt(5)) / 2;
            double b = (1 - Math.Sqrt(5)) / 2;
            Int32 n = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < n; i++)
            {
                double y = (double)Int32.Parse(Console.ReadLine()) - 1;
                double x = (y - Math.Log10(c)) / Math.Log10(a);
                Console.WriteLine((int)Double.Ceiling(x));
            }
        }
    }
}
