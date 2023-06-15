namespace Conways
{
    internal class Grid
    {
        private int rows;
        private int columns;
        private string header;
        private bool[,] grid;

        public Grid(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            header = lines[0];
            rows = int.Parse(header.Split(',')[0]);
            columns = int.Parse(header.Split(',')[1]);

            grid = new bool[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                char[] row = lines[r + 1].ToArray();
                for (int c = 0; c < columns; c++)
                {
                    if (row[c] == '*')
                    {
                        grid[r, c] = true;
                    }
                }
            }

        }

        public void ComputeNextGeneration()
        {
            // Rules:
            /*
               1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
               2. Any live cell with more than three live neighbours dies, as if by overcrowding.
               3. Any live cell with two or three live neighbours lives on to the next generation.
               4. Any dead cell with exactly three live neighbours becomes a live cell.
            */
            // creeate output grid
            // cicle throug grid
            // count alive cell neighbours and apply rules
            // copy the result on output grid in the same position from the input grid
            bool[,] outputGrid = new bool[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    bool isAlive = grid[r, c];
                    int neighbours = CountAliveNeighbours(grid, r, c);

                    if (isAlive)
                    {
                        switch (neighbours)
                        {
                            case < 2:
                                outputGrid[r, c] = false; break;
                            case > 3:
                                outputGrid[r, c] = false; break;
                            case >= 2:
                                outputGrid[r, c] = true; break;
                        }
                    }
                    else
                    {
                        if (neighbours == 3)
                            outputGrid[r, c] = true;
                    }
                }
            }

            grid = outputGrid;
        }

        public void Print()
        {
            string[] outputLines = new string[rows + 1];
            outputLines[0] = header;

            for (int r = 0; r < rows; r++)
            {
                char[] row = new char[columns];
                Array.Fill(row, '.');
                for (int c = 0; c < columns; c++)
                {
                    if (grid[r, c])
                        row[c] = '*';
                }
                outputLines[r + 1] = new string(row);
            }

            foreach (string line in outputLines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        private static int CountAliveNeighbours(bool[,] grid, int r, int c)
        {
            int count = 0;
            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = c - 1; j <= c + 1; j++)
                {
                    if (i >= 0 && j >= 0 && i < rows && j < columns)
                    {
                        bool isAlive = grid[i, j];

                        if (isAlive)
                            count++;
                    }

                }
            }

            return count;
        }

    }
}
