namespace AdventOfCodeMain.DaySolutions;

internal class Day06
{
    public static void Run()
    {
        string challengeInput = FileReader.ReadFromFile("Day6.1");
        char[][] maze = challengeInput.Split("\r\n").Select(row => row.ToCharArray()).ToArray(); // initialize maze as char[][] because strings are immutable

        (int, int) startingPosition = FindStartingPosition(maze);
        int currentDirection = 0; // start as '^' => up

        int amountOfPositions = FindAmountOfPositions(maze, startingPosition, currentDirection);

        Console.WriteLine(amountOfPositions);
    }

    private static readonly (int Row, int Col)[] directions = new[]
    {
    (-1, 0), // 0: up
    (0, 1),  // 1: right
    (1, 0),  // 2: down
    (0, -1)  // 3: left
    };

    private static int FindAmountOfPositions(char[][] maze, (int row, int col) currentPosition, int currentDirection)
    {
        int amountRows = maze.Length - 1;
        int amountCols = maze[0].Length - 1;
        maze[currentPosition.row][currentPosition.col] = 'X'; // visited the start
        while (true)
        {
            (int row, int col) nextPosition = AddTuples(currentPosition, directions[currentDirection]);

            bool isExit = nextPosition.row < 0 || nextPosition.row > amountRows ||
                          nextPosition.col < 0 || nextPosition.col > amountCols;
            if (isExit) return maze.Sum(row => row.Count(cell => cell == 'X'));

            bool isBlocked = maze[nextPosition.row][nextPosition.col] == '#';
            if (isBlocked)
            {
                currentDirection = Turn(currentDirection);
            }
            else
            {
                maze[nextPosition.row][nextPosition.col] = 'X'; // visited
                currentPosition = nextPosition;
            }
        }
    }

    private static (int, int) FindStartingPosition(char[][] maze)
    {
        for (int row = 0; row < maze.Length; row++)
        {
            int col = Array.IndexOf(maze[row], '^');
            if (col != -1)
            {
                return (row, col);
            }
        }
        throw new Exception("Start not found");
    }


    private static int Turn(int currentDirection) => (currentDirection + 1) % directions.Length;

    private static (int, int) AddTuples((int, int) tuple1, (int, int) tuple2) => (tuple1.Item1 + tuple2.Item1, tuple1.Item2 + tuple2.Item2);

}
