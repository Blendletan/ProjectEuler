using System.Diagnostics;
namespace Problem96
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] input = ScanArray();
            Sodoku s = new Sodoku(input, null, null);
            Sodoku? output = Sodoku.Solve(s);
            if (output == null)
            {
                Console.WriteLine("No solution");
            }
            else
            {
                PrintArray(output.map);
            }
        }
        static int[,] ScanArray()
        {
            int[,] array = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                string nextLine = Console.ReadLine();
                for (int j = 0; j < 9; j++)
                {
                    char c = nextLine[j];
                    if (c == '.')
                    {
                        array[i, j] = 0;
                    }
                    else
                    {
                        array[i, j] = (int)Char.GetNumericValue(c);
                    }
                }
            }
            return array;
        }
        static void PrintArray(int[,] array)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
    class SodokuGraph
    {
        public readonly Dictionary<(int, int), List<(int, int)>> edgeList;
        public SodokuGraph()
        {
            edgeList = new Dictionary<(int, int), List<(int, int)>>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    edgeList.Add((i, j), new List<(int, int)>());
                    foreach (var friend in GetColumnFriends(i, j))
                    {
                        edgeList[(i, j)].Add(friend);
                    }
                    foreach (var friend in GetRowFriends(i, j))
                    {
                        edgeList[(i, j)].Add(friend);
                    }
                    foreach (var friend in GetSquareFriends(i, j))
                    {
                        edgeList[(i, j)].Add(friend);
                    }
                }
            }
        }
        static List<(int, int)> GetColumnFriends(int y, int x)
        {
            var output = new List<(int, int)>();
            for (int i = 0; i < 9; i++)
            {
                if (i == y)
                {
                    continue;
                }
                output.Add((i, x));
            }
            return output;
        }
        static List<(int, int)> GetRowFriends(int y, int x)
        {
            var output = new List<(int, int)>();
            for (int i = 0; i < 9; i++)
            {
                if (i == x)
                {
                    continue;
                }
                output.Add((y, i));
            }
            return output;
        }
        static List<(int, int)> GetSquareFriends(int y, int x)
        {
            var output = new List<(int, int)>();
            int startX = x / 3;
            startX *= 3;
            int startY = y / 3;
            startY *= 3;
            int endX = startX + 3;
            int endY = startY + 3;
            for (int i = startY; i < endY; i++)
            {
                for (int j = startX; j < endX; j++)
                {
                    if (i == y && j == x)
                    {
                        continue;
                    }
                    output.Add((i, j));
                }
            }
            return output;
        }
    }
    class Sodoku
    {
        public static int numberOfScrapPuzzles;
        public int[,] map;
        public List<int>[,] availableDigits;
        readonly List<int> digits;
        public SodokuGraph graph;
        public Sodoku(int[,] input, List<int>[,]? possibleDigits, SodokuGraph? inputGraph)
        {
            numberOfScrapPuzzles++;
            if (inputGraph == null)
            {
                graph = new SodokuGraph();
            }
            else
            {
                graph = inputGraph;
            }
            map = new int[9, 9];
            digits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            availableDigits = new List<int>[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int d = input[i, j];
                    map[i, j] = d;
                }
            }
            if (possibleDigits == null)
            {
                availableDigits = InitializeAvailableDigits(map, digits, graph);
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        availableDigits[i, j] = new List<int>(possibleDigits[i, j]);
                    }
                }
            }
        }
        static List<int>[,] InitializeAvailableDigits(int[,] map, List<int> digits, SodokuGraph graph)
        {
            var output = new List<int>[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    output[i, j] = new List<int>(digits);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int currentdigit = map[i, j];
                    if (currentdigit == 0)
                    {
                        continue;
                    }
                    foreach (var neighbor in graph.edgeList[(i, j)])
                    {
                        var neighborDigits = output[neighbor.Item1, neighbor.Item2];
                        if (neighborDigits.Contains(currentdigit))
                        {
                            neighborDigits.Remove(currentdigit);
                        }
                    }
                }
            }
            return output;
        }
        public static Sodoku? Solve(Sodoku input)
        {
            if (input.IsValid() == false)
            {
                return null;
            }
            (int, int)? nextBlankPosition = input.NextBlankSpace();
            if (nextBlankPosition == null)
            {
                return input;
            }
            var digitList = input.availableDigits[nextBlankPosition.Value.Item1, nextBlankPosition.Value.Item2];
            foreach (var i in digitList)
            {
                Sodoku s = new Sodoku(input.map, input.availableDigits, input.graph);
                s.FillInDigit(nextBlankPosition.Value.Item1, nextBlankPosition.Value.Item2, i);
                Sodoku? output = Solve(s);
                if (output != null)
                {
                    return output;
                }
            }
            return null;
        }
        public void FillInDigit(int y, int x, int digit)
        {
            map[y, x] = digit;
            foreach (var position in graph.edgeList[(y, x)])
            {
                if (availableDigits[position.Item1, position.Item2].Count != 0)
                {
                    if (availableDigits[position.Item1, position.Item2].Contains(digit))
                    {
                        availableDigits[position.Item1, position.Item2].Remove(digit);
                    }
                }
            }
            availableDigits[y, x].Clear();
        }
        public bool IsValid()
        {
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (availableDigits[i, j].Count == 0)
                        {
                            if (map[i, j] == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }
        (int, int)? NextBlankSpace()
        {
            int fewestAvailableDigits = 10;
            (int, int)? winningIndex = null;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (map[i, j] == 0)
                    {
                        if (availableDigits[i, j].Count < fewestAvailableDigits)
                        {
                            winningIndex = (i, j);
                            fewestAvailableDigits = availableDigits[i, j].Count;
                        }
                    }
                }
            }
            return winningIndex;
        }
    }
}
