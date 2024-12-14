namespace AdventOfCodeMain.DaySolutions;

internal class Day12
{
    public static void Run()
    {
        string[] challengeInput = FileReader.ReadFromFile("Day12.1").Split("\r\n").ToArray();

        garden = challengeInput;
        visited = new HashSet<(int, int)>();


        numRows = challengeInput.Length;
        numCols = challengeInput[0].Length;

        int price = 0;

        for (int i = 0; i < numRows * numCols; i++)
        {
            int row = i / numCols;
            int col = i % numCols;

            if (visited.Contains((row, col))) continue;

            (int perimeter, int area) = FencePlot(row, col);

            price += perimeter * area;
        }

        Console.WriteLine(price);
    }

    private static readonly (int Row, int Col)[] directions = new[]
    {
        (-1, 0), // 0: up
        (0, 1),  // 1: right
        (1, 0),  // 2: down
        (0, -1)  // 3: left
    };

    private static HashSet<(int, int)> visited = new();
    private static string[] garden;
    private static int numRows;
    private static int numCols;

    private static (int, int) FencePlot (int row, int col, int perimeter = 0, int area = 0)
    {
        visited.Add((row, col));
        area++;
        foreach (var direction in directions)
        {
            (int nextRow, int nextCol) = (row + direction.Row, col + direction.Col);

            bool outOfBounds = nextRow < 0 || nextRow >= numRows || nextCol < 0 || nextCol >= numCols;

            bool isSameType = !outOfBounds && garden[nextRow][nextCol] == garden[row][col];

            bool isVisited = !outOfBounds && visited.Contains((nextRow, nextCol));

            if (outOfBounds || !isSameType)
            {
                perimeter++;
            }
            else if (!isVisited)
            {
                (perimeter, area) = FencePlot(nextRow, nextCol, perimeter, area);
            }

        }
        return (perimeter, area);
    }

}
