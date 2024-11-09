namespace Problem22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfNames = Int32.Parse(Console.ReadLine());
            List<string> listOfNames = new List<string>();
            for(Int32 i = 0; i < numberOfNames; i++)
            {
                listOfNames.Add(Console.ReadLine());
            }
            listOfNames.Sort();
            Int32 numberOfTestCases = Int32.Parse(Console.ReadLine());
            for(Int32 i = 0; i < numberOfTestCases; i++)
            {
                string nextInput = Console.ReadLine();
                Int32 indexOfInput = listOfNames.IndexOf(nextInput)+1;
                Console.WriteLine(indexOfInput * ComputeNameScore(nextInput));
            }
        }
        static Int32 ComputeNameScore(string name)
        {
            char[] chars = name.ToCharArray();
            Int32 output = 0;
            foreach(char c in chars)
            {
                output += ComputeCharScore(c);
            }
            return output;
        }
        static Int32 ComputeCharScore(char c)
        {
            Int32 output = (Int32)(c - 'A');
            output++;
            return output;
        }
    }
}