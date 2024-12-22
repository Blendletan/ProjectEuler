namespace Problem83
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int64[,] input = ParseInputs();
            MatrixPuzzle p = new MatrixPuzzle(input);
            Console.WriteLine(p.Solve());
        }
        static Int64[,] ParseInputs()
        {
            Int32 matrixSize = Int32.Parse(Console.ReadLine());
            Int64[,] output = new Int64[matrixSize, matrixSize];
            for (Int32 i = 0; i < matrixSize; i++)
            {
                string[] nextLine = Console.ReadLine().Split();
                for (Int32 j = 0; j < matrixSize; j++)
                {
                    output[i, j] = Int64.Parse(nextLine[j]);
                }
            }
            return output;
        }
    }
    internal class MatrixPuzzle
    {
        Int32 size;
        Int64[,] matrix;
        PriorityQueue<(Int32, Int32), Int64> unvisitedNodes;
        HashSet<(Int32, Int32)> visitedNodes;
        Int64?[,] distances;
        public MatrixPuzzle(Int64[,] input)
        {
            matrix = input;
            size = matrix.GetLength(0);
            unvisitedNodes = new PriorityQueue<(Int32, Int32), Int64>();
            visitedNodes = new HashSet<(Int32, Int32)>();
            distances = new Int64?[size, size];
            for (Int32 i = 0; i < size; i++)
            {
                for (Int32 j = 0; j < size; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Int64 distance = matrix[0, 0];
                        distances[i, j] = distance;
                        unvisitedNodes.Enqueue((0, 0), distance);
                    }
                    else
                    {
                        distances[i, j] = null;
                    }
                }
            }
        }
        public Int64 Solve()
        {
            while (true)
            {
                if (NextStep() == false)
                {
                    Int64 output = GetWinningValue();
                    return output;
                }
            }
        }
        Int64 GetWinningValue()
        {
            Int64? minValue = distances[size - 1, size - 1];
            if (minValue == null)
            {
                throw new Exception("Min value not found");
            }
            return minValue.Value;
        }
        bool NextStep()
        {
            (Int32, Int32)? nextNode = GetSmallestUnvisitedNode();
            if (nextNode == null)
            {
                return false;
            }
            UpdateNeighbors(nextNode.Value.Item1, nextNode.Value.Item2);
            visitedNodes.Add(nextNode.Value);
            Int64 currentDistance = distances[nextNode.Value.Item1, nextNode.Value.Item2].Value;
            return true;
        }
        void UpdateNeighbors(Int32 i, Int32 j)
        {
            Int64 currentNodeDistance = distances[i, j].Value;
            if (i > 0)
            {
                (Int32, Int32) topNeighbor = (i - 1, j);
                UpdateNextNeighbor(currentNodeDistance, topNeighbor.Item1, topNeighbor.Item2);
            }
            if (j > 0)
            {
                (Int32, Int32) leftNeighbor = (i, j - 1);
                UpdateNextNeighbor(currentNodeDistance, leftNeighbor.Item1, leftNeighbor.Item2);
            }
            if (i < size - 1)
            {
                (Int32, Int32) bottomNeighbor = (i + 1, j);
                UpdateNextNeighbor(currentNodeDistance, bottomNeighbor.Item1, bottomNeighbor.Item2);
            }
            if (j < size - 1)
            {
                (Int32, Int32) rightNeighbor = (i, j + 1);
                UpdateNextNeighbor(currentNodeDistance, rightNeighbor.Item1, rightNeighbor.Item2);
            }
        }
        void UpdateNextNeighbor(Int64 currentNodeDistance, Int32 neighborY, Int32 neighborX)
        {
            Int64 neighborValue = matrix[neighborY, neighborX];
            if (visitedNodes.Contains((neighborY, neighborX)) == false)
            {
                Int64 newDistance = currentNodeDistance + neighborValue;
                if (distances[neighborY, neighborX] == null)
                {
                    distances[neighborY, neighborX] = newDistance;
                    unvisitedNodes.Enqueue((neighborY, neighborX), newDistance);
                }
                else if (newDistance < distances[neighborY, neighborX])
                {
                    unvisitedNodes.Enqueue((neighborY, neighborX), newDistance);
                    distances[neighborY, neighborX] = newDistance;
                }
            }
        }
        (Int32, Int32)? GetSmallestUnvisitedNode()
        {
            if (visitedNodes.Count == size * size)
            {
                return null;
            }
            while (true)
            {
                var v = unvisitedNodes.Dequeue();
                if (visitedNodes.Contains(v) == false)
                {
                    return v;
                }
            }
        }
    }
}