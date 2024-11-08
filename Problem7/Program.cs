namespace Problem7
{
    internal class Program
    {
        const Int32 upperBound = 1000000;
        static void Main(string[] args)
        {
            bool[] isPrime = new bool[upperBound + 1];
            for (Int32 i = 2; i <= upperBound; i++)
            {
                isPrime[i] = true;
            }
            Int32 upperSieveBound = (Int32)Math.Sqrt(upperBound);
            List<Int32> primeList = new List<Int32>();
            for (Int32 i = 2; i <= upperSieveBound; i++)
            {
                if (isPrime[i])
                {
                    for (Int32 j = 2 * i; j <= upperBound; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            for (Int32 i = 2; i <= upperBound; i++)
            {
                if (isPrime[i])
                {
                    primeList.Add(i);
                }
            }
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int32 input = Int32.Parse(Console.ReadLine());
                Console.WriteLine(primeList[input - 1]);
            }
        }
    }
}
