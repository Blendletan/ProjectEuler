namespace Problem62
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int64 n = Int64.Parse(inputs[0]);
            Int64 k = Int64.Parse(inputs[1]);
            CubePermutations cp = new CubePermutations(n);
            List<Int64> outputs = new List<Int64>();
            foreach (var v in cp.cubeDigitHistograms)
            {
                if (v.Value.Count == k)
                {
                    Int64 output = v.Value.Min();
                    outputs.Add(output);
                }
            }
            outputs.Sort();
            foreach (var v in outputs)
            {
                Console.WriteLine(v);
            }
        }
    }
    internal class CubePermutations
    {
        public Dictionary<string, List<Int64>> cubeDigitHistograms;
        public CubePermutations(Int64 n)
        {
            Int64 maxInputSize = n;
            cubeDigitHistograms = new Dictionary<string,List<Int64>>();
            for(Int64 i=0;i< maxInputSize; i++)
            {
                Int64 nextCube = i * i * i;
                string digitHash = DigitHash(nextCube);
                if (!cubeDigitHistograms.ContainsKey(digitHash))
                {
                    cubeDigitHistograms.Add(digitHash, new List<Int64>());
                }
                cubeDigitHistograms[digitHash].Add(nextCube);
            }
        }
        static string DigitHash(Int64 a)
        {
            List<char> aDigits = a.ToString().ToCharArray().ToList();
            aDigits.Sort();
            return new string(aDigits.ToArray());
        }
    }
}