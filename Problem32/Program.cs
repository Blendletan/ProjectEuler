namespace Problem32
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfDigits = Int32.Parse(Console.ReadLine());
            List<Int32> foundProducts = new List<Int32>();
            for(Int32 i = 1; i <= 10000; i++)
            {
                if (2*NumberOfDigits(i)+1 > numberOfDigits)
                {
                    break;
                }
                for(Int32 j = i; j <= 10000; j++)
                {
                    Int32 product = i * j;
                    Int32 totalNumberOfDigits = NumberOfDigits(i) + NumberOfDigits(j) + NumberOfDigits(i * j);
                    if (totalNumberOfDigits > numberOfDigits)
                    {
                        break;
                    }
                    if (IsPandigital(i, j, product, numberOfDigits))
                    {
                        if (!foundProducts.Contains(i * j))
                        {
                            foundProducts.Add(i * j);
                        }
                    }
                }
            }
            Console.WriteLine(foundProducts.Sum());
        }
        static Int32 NumberOfDigits(Int32 n)
        {
            return (Int32)Math.Log10(n) + 1;
        }
        static bool IsPandigital(Int32 a, Int32 b, Int32 c, Int32 numberOfDigits)
        {
            HashSet<Int32> foundDigits = new HashSet<Int32>();
            foreach (var v in GetDigits(a))
            {
                if (foundDigits.Contains(v))
                {
                    return false;
                }
                foundDigits.Add(v);
            }
            foreach (var v in GetDigits(b))
            {
                if (foundDigits.Contains(v))
                {
                    return false;
                }
                foundDigits.Add(v);
            }
            foreach (var v in GetDigits(c))
            {
                if (foundDigits.Contains(v))
                {
                    return false;
                }
                foundDigits.Add(v);
            }
            if (foundDigits.Contains(0))
            {
                return false;
            }
            for(Int32 i = 1; i <= numberOfDigits; i++)
            {
                if (!foundDigits.Contains(i))
                {
                    return false;
                }
            }
            for(Int32 i = numberOfDigits + 1; i <= 9; i++)
            {
                if (foundDigits.Contains(i))
                {
                    return false;
                }
            }
            return true;
        }
        static List<Int32> GetDigits(Int32 input)
        {
            List<Int32> output = new List<Int32>();
            while (true)
            {
                Int32 nextDigit = input % 10;
                output.Add(nextDigit);
                if (input < 10)
                {
                    return output;
                }
                input = input / 10;
            }
        }
    }
}
