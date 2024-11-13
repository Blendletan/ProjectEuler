namespace Problem45
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int64 maxHeight = Int64.Parse(inputs[0]);
            Int32 caseSwitch = Int32.Parse(inputs[1]);
            List<Int64> output;
            if (caseSwitch == 3)
            {
                output = GenerateAllAdmissable(maxHeight, true);
            }
            else
            {
                output = GenerateAllAdmissable(maxHeight, false);
            }
            foreach(var v in output)
            {
                Console.WriteLine(v);
            }
        }
        static List<Int64> GenerateAllAdmissable(Int64 maxHeight,bool triangular)
        {
            List<Int64> output = new List<Int64>();
            for(Int32 i = 1; i < maxHeight; i++)
            {
                Int64 nextNumber = Pentagonal(i);
                if (nextNumber >= maxHeight)
                {
                    break;
                }
                if (triangular)
                {
                    if (IsTriangular(nextNumber))
                    {
                        output.Add(nextNumber);
                    }
                }
                else
                {
                    if (IsHexagonal(nextNumber))
                    {
                        output.Add(nextNumber);
                    }
                }
            }
            return output;
        }
        static bool IsTriangular(Int64 n)
        {
            if (double.IsInteger(Math.Sqrt(8 * n + 1)))
            {
                Int64 remainder = (Int64)Math.Sqrt(8 * n + 1) - 1;
                return (remainder % 2 == 0);
            }
            return false;
        }
        static Int64 TriangularNumber(Int64 n)
        {
            return n * (n + 1) / 2;
        }
        static bool IsHexagonal(Int64 n)
        {
            if (double.IsInteger(Math.Sqrt(8 * n + 1)))
            {
                Int64 remainder = (Int64)Math.Sqrt(8 * n + 1) + 1;
                return (remainder % 4 == 0);
            }
            return false;
        }
        static Int64 Pentagonal(Int64 n)
        {
            return n * (3 * n - 1) / 2;
        }
    }
}