using System.Diagnostics;

namespace AdventOfCodeMain.DaySolutions
{
    internal class Day04
    {
        public static void Run()
        {
            string challengeInput = FileReader.ReadFromFile("Day4.1");
            string[] lines = challengeInput.Split("\r\n");

            int numRows = lines.Length;
            int numCols = lines[0].Length;
            string word = "XMAS";
            int matchCounter = 0;


            var stopwatch = new Stopwatch();

            // Test flat loop
            stopwatch.Start();
            for (int pos = 0; pos < numRows * numCols; pos++)
            {
                int row = pos / numCols;
                int col = pos % numCols;

                if (lines[row][col] == word[0])
                {
                    foreach (var direction in directions)
                    {
                        if (IsWordInDirection(lines, row, col, word, direction, numRows, numCols))
                        {
                            matchCounter++;
                        }
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Flat loop time: {stopwatch.ElapsedMilliseconds}ms"); // about 1-2ms

            // Test LINQ version
            stopwatch.Restart();
            var matchCountLINQ = Enumerable.Range(0, numRows)
                .SelectMany(row => Enumerable.Range(0, numCols), (row, col) => new { row, col })
                .Where(pos => lines[pos.row][pos.col] == word[0])
                .Sum(pos => directions.Count(direction =>
                    IsWordInDirection(lines, pos.row, pos.col, word, direction, numRows, numCols)));

            stopwatch.Stop();
            Console.WriteLine($"LINQ time: {stopwatch.ElapsedMilliseconds}ms"); // about 3-8ms

            Console.WriteLine($"Total matches found: {matchCounter}");
        }

        private static readonly List<(int Row, int Col)> directions = new()
        {
            (-1,  0), // Up
            ( 1,  0), // Down
            ( 0, -1), // Left
            ( 0,  1), // Right
            (-1, -1), // Up-Left
            (-1,  1), // Up-Right
            ( 1, -1), // Down-Left
            ( 1,  1)  // Down-Right
        };

        private static bool IsWordInDirection(string[] lines, int startRow, int startCol, string word, (int, int) direction, int numRows, int numCols)
        {
            for (int i = 1; i < word.Length; i++)
            {
                int newRow = startRow + i * direction.Item1;
                int newCol = startCol + i * direction.Item2;

                // Check boundaries
                if (newRow < 0 || newRow >= numRows || newCol < 0 || newCol >= numCols)
                    return false;

                if (lines[newRow][newCol] != word[i])
                    return false;
            }
            return true;
        }
    }
}
