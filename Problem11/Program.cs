namespace Problem11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32[][] grid = new Int32[20][];
            for (Int32 i = 0; i < 20; i++)
            {
                string[] nextLine = Console.ReadLine().Split(' ');
                grid[i] = Array.ConvertAll(nextLine, Int32.Parse);
            }
            DiagonalGrid gd = new DiagonalGrid(grid);
            Console.WriteLine(gd.LargestProductInGrid());
        }
    }
    class DiagonalGrid
    {
        Int32[][] grid;
        public DiagonalGrid(Int32[][] grid)
        {
            this.grid = grid;
        }
        private Int32 LargestProductInThisRow(Int32 rowIndex)
        {
            Int32 output = 0;
            for (Int32 i = 0; i <= 16; i++)
            {
                Int32 product = grid[rowIndex][i] * grid[rowIndex][i + 1] * grid[rowIndex][i + 2] * grid[rowIndex][i + 3];
                if (product > output)
                {
                    output = product;
                }
            }
            return output;
        }
        private Int32 LargestProductInThisColumn(Int32 columnIndex)
        {
            Int32 output = 0;
            for (Int32 i = 0; i <= 16; i++)
            {
                Int32 product = grid[i][columnIndex] * grid[i + 1][columnIndex] * grid[i + 2][columnIndex] * grid[i + 3][columnIndex];
                if (product > output)
                {
                    output = product;
                }
            }
            return output;
        }
        private Int32 LargestProductInThisIncreasingDiagonal(Int32 rowIndex)
        {
            Int32 output = 0;
            for (Int32 i = 0; i <= 16; i++)
            {
                Int32 product = grid[rowIndex][i] * grid[rowIndex + 1][i + 1] * grid[rowIndex + 2][i + 2] * grid[rowIndex + 3][i + 3];
                if (product > output)
                {
                    output = product;
                }
            }
            return output;
        }
        private Int32 LargestProductInThisDecreasingDiagonal(Int32 rowIndex)
        {
            Int32 output = 0;
            for (Int32 i = 0; i <= 16; i++)
            {
                Int32 product = grid[rowIndex + 3][i] * grid[rowIndex + 2][i + 1] * grid[rowIndex + 1][i + 2] * grid[rowIndex][i + 3];
                if (product > output)
                {
                    output = product;
                }
            }
            return output;
        }
        public Int32 LargestProductInGrid()
        {
            Int32 output = 0;
            for (Int32 i = 0; i < 20; i++)
            {
                Int32 product = LargestProductInThisRow(i);
                if (product > output)
                {
                    output = product;
                }
                product = LargestProductInThisColumn(i);
                if (product > output)
                {
                    output = product;
                }
            }
            for (Int32 i = 0; i <= 16; i++)
            {
                Int32 product = LargestProductInThisIncreasingDiagonal(i);
                if (product > output)
                {
                    output = product;
                }
                product = LargestProductInThisDecreasingDiagonal(i);
                if (product > output)
                {
                    output = product;
                }
            }
            return output;
        }

    }
}
