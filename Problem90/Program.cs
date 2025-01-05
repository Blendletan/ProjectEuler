namespace Problem90
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 largestSquare = Int32.Parse(inputs[0]);
            Int32 numberOfCubes = Int32.Parse(inputs[1]);
            var squares = GenerateSquares(largestSquare);
            if (numberOfCubes == 1)
            {
                var cubes = GenerateDice();
                Int32 output = 0;
                foreach (var v in cubes)
                {
                    if (IsValid(v, squares))
                    {
                        output++;
                    }
                }
                Console.WriteLine(output);
            }
            if (numberOfCubes == 2)
            {
                var cubePairs = GenerateCubePairs();
                Int32 output = 0;
                foreach (var v in cubePairs)
                {
                    if (IsValid(v.Item1, v.Item2, squares))
                    {
                        output++;
                    }
                }
                Console.WriteLine(output);
            }
            if (numberOfCubes == 3)
            {
                var cubeTriplets = GenerateCubeTriplets();
                Int32 output = 0;
                foreach (var v in cubeTriplets)
                {
                    if (IsValid(v.Item1, v.Item2, v.Item3, squares))
                    {
                        output++;
                    }
                }
                Console.WriteLine(output);
            }
        }
        static List<(Int32[], Int32[])> GenerateCubePairs()
        {
            var output = new List<(Int32[], Int32[])>();
            var cubes = GenerateDice();
            cubes.Sort(CompareDice);
            Int32 length = cubes.Count;
            for (Int32 i = 0; i < length; i++)
            {
                for (Int32 j = i; j < length; j++)
                {
                    output.Add((cubes[i], cubes[j]));
                }
            }
            return output;
        }
        static List<(Int32[], Int32[], Int32[])> GenerateCubeTriplets()
        {
            var output = new List<(Int32[], Int32[], Int32[])>();
            var cubes = GenerateDice();
            cubes.Sort(CompareDice);
            Int32 length = cubes.Count;
            for (Int32 i = 0; i < length; i++)
            {
                for (Int32 j = i; j < length; j++)
                {
                    for (Int32 k = j; k < length; k++)
                    {
                        output.Add((cubes[i], cubes[j], cubes[k]));
                    }
                }
            }
            return output;
        }
        static List<Int32[]> GenerateDice()
        {
            var diceCombinations = new List<List<Int32>>[7];
            var digits = new List<Int32> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            diceCombinations[1] = new List<List<Int32>>();
            foreach (var digit in digits)
            {
                var nextSet = new List<Int32>();
                nextSet.Add(digit);
                diceCombinations[1].Add(nextSet);
            }
            for (Int32 i = 2; i <= 6; i++)
            {
                diceCombinations[i] = new List<List<Int32>>();
                foreach (var previousSet in diceCombinations[i - 1])
                {
                    var availableDigits = new List<Int32>(digits);
                    foreach (var v in digits)
                    {
                        if (previousSet.Contains(v) || v < previousSet.Last())
                        {
                            availableDigits.Remove(v);
                        }
                    }
                    foreach (var digit in availableDigits)
                    {
                        var nextSet = new List<Int32>(previousSet);
                        nextSet.Add(digit);
                        diceCombinations[i].Add(nextSet);
                    }
                }
            }
            var output = new List<Int32[]>();
            foreach (var v in diceCombinations[6])
            {
                output.Add(v.ToArray());
            }
            return output;
        }
        static Int32 CompareDice(Int32[] firstCube, Int32[] secondCube)
        {
            bool different = false;
            Int32 length = firstCube.Length;
            for (Int32 i = 0; i < length; i++)
            {
                if (firstCube[i] != secondCube[i])
                {
                    different = true;
                }
                if (firstCube[i] > secondCube[i])
                {
                    return 1;
                }
            }
            if (different == false)
            {
                return 0;
            }
            return -1;
        }
        static List<(Int32, Int32,Int32)>GenerateSquares(Int32 n)
        {
            var output = new List<(Int32, Int32,Int32)>();
            for (Int32 i = 1; i <= n; i++)
            {
                Int32 nextSquare = i * i;
                var digits = GetDigits(nextSquare);
                output.Add((digits[0], digits[1], digits[2]));
            }
            return output;
        }
        static bool IsValid(Int32[] firstCube, List<(Int32, Int32,Int32)> listOfSquares)
        {
            Int32 length = firstCube.Length;
            for (Int32 i = 0; i < length; i++)
            {
                if (firstCube[i] == 9)
                {
                    firstCube[i] = 6;
                }
            }
            foreach (var square in listOfSquares)
            {
                if (square.Item2 != 0)
                {
                    return false;
                }
                if (firstCube.Contains(square.Item1) == false)
                {
                    return false;
                }
            }
            return true;
        }
        static bool IsValid(Int32[] firstCube, Int32[] secondCube, List<(Int32, Int32,Int32)> listOfSquares)
        {
            Int32 length = firstCube.Length;
            for (Int32 i = 0; i < length; i++)
            {
                if (firstCube[i] == 9)
                {
                    firstCube[i] = 6;
                }
                if (secondCube[i] == 9)
                {
                    secondCube[i] = 6;
                }
            }
            foreach (var v in listOfSquares)
            {
                if (firstCube.Contains(v.Item1) && secondCube.Contains(v.Item2) || firstCube.Contains(v.Item2) && secondCube.Contains(v.Item1))
                {
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        static bool IsValid(Int32[] firstCube, Int32[] secondCube, Int32[] thirdCube, List<(Int32, Int32,Int32)> listOfSquares)
        {
            Int32 length = firstCube.Length;
            for (Int32 i = 0; i < length; i++)
            {
                if (firstCube[i] == 9)
                {
                    firstCube[i] = 6;
                }
                if (secondCube[i] == 9)
                {
                    secondCube[i] = 6;
                }
                if (thirdCube[i] == 9)
                {
                    thirdCube[i] = 6;
                }
            }
            foreach (var v in listOfSquares)
            {
                if (firstCube.Contains(v.Item1) && secondCube.Contains(v.Item2) && thirdCube.Contains(v.Item3) || firstCube.Contains(v.Item2) && secondCube.Contains(v.Item1) && thirdCube.Contains(v.Item3))
                {
                }
                else if (firstCube.Contains(v.Item1) && thirdCube.Contains(v.Item2) && secondCube.Contains(v.Item3) || firstCube.Contains(v.Item2) && thirdCube.Contains(v.Item1) && secondCube.Contains(v.Item3))
                {
                }
                else if (secondCube.Contains(v.Item1) && thirdCube.Contains(v.Item2) && firstCube.Contains(v.Item3) || secondCube.Contains(v.Item2) && thirdCube.Contains(v.Item1) && firstCube.Contains(v.Item3))
                {
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        static List<Int32> GetDigits(Int32 n)
        {
            List<Int32> output = new List<Int32>();
            Int32 nextDigit = n % 10;
            if (nextDigit == 9)
            {
                nextDigit = 6;
            }
            output.Add(nextDigit);
            n /= 10;
            nextDigit = n % 10;
            if (nextDigit == 9)
            {
                nextDigit = 6;
            }
            output.Add(nextDigit);
            n /= 10;
            nextDigit = n % 10;
            if (nextDigit == 9)
            {
                nextDigit = 6;
            }
            output.Add(nextDigit);
            return output;
        }
    }
}
