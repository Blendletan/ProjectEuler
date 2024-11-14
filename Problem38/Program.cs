namespace Problem38
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputLine = Console.ReadLine().Split();
            Int32 maxSize = Int32.Parse(inputLine[0]);
            bool isEight = false;
            Int32 eight = Int32.Parse(inputLine[1]);
            if (eight == 8) { isEight = true; }
            List<Int32> resultList = GeneratePandigitalProducts(isEight);
            resultList.Sort();
            foreach (Int32 i in resultList)
            {
                if (i <= maxSize)
                {
                    Console.WriteLine(i);
                }
            }
        }
        static List<Int32> GeneratePandigitalProducts(bool isEight)
        {
            List<Int32> output = new List<Int32>();
            for (Int32 candidate = 2; candidate <= 10000; candidate++)
            {
                if (IsPandigitalProduct(candidate, isEight))
                {
                    if (!output.Contains(candidate))
                    {
                        output.Add(candidate);
                    }
                }
            }
            return output;
        }
        static bool IsPandigitalProduct(Int32 candidate, bool isEight)
        {
            string productList = GenerateConcatenatedProducts(candidate, isEight);
            return IsPandigital(productList, isEight);
        }
        static bool IsPandigital(string candidate, bool isEight)
        {
            List<char> sortedCandidate = candidate.ToList();
            sortedCandidate.Sort();
            List<char> desiredResult = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8' };
            if (!isEight) { desiredResult.Add('9'); }
            return sortedCandidate.SequenceEqual(desiredResult);
        }
        static string GenerateConcatenatedProducts(Int32 candidate, bool isEight)
        {
            string result = string.Empty;
            Int64 desiredLength = 9;
            if (isEight) { desiredLength = 8; }
            Int32 index = 1;
            while (true)
            {
                if (result.Length >= desiredLength)
                {
                    return result;
                }
                result = result + (index * candidate).ToString();
                index++;
            }
        }
    }
}
