using System.Text.RegularExpressions;

namespace AdventOfCodeMain.DaySolutions;

internal class Day14
{
    public static void Run()
    {
        string challengeInput = FileReader.ReadFromFile("Day14.1");

        string pattern = @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)";
        Regex regex = new Regex(pattern);

        int numCols = 101,
            numRows = 103,
            seconds = 100,
            halfCols = numCols / 2,
            halfRows = numRows / 2;
       

        // column major order
        List<(int col, int row)> positions = new();
        List<(int col, int row)> velocities = new();

        var matches = regex.Matches(challengeInput);
        foreach (Match match in matches)
        {
            positions.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
            velocities.Add((int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)));
        }

        Dictionary<int, int> quadrants = new ()
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 }
        };

        for (int i = 0; i < positions.Count; i++)
        {
            int finalCol = ((positions[i].col + seconds * velocities[i].col) % numCols + numCols) % numCols;
            int finalRow = ((positions[i].row + seconds * velocities[i].row) % numRows + numRows) % numRows;


            if (finalCol < halfCols && finalRow < halfRows)
                quadrants[1]++; // top-left
            else if (finalCol > halfCols && finalRow < halfRows)
                quadrants[2]++; // top-right
            else if (finalCol < halfCols && finalRow > halfRows)
                quadrants[3]++; // bottom-left
            else if (finalCol > halfCols && finalRow > halfRows)
                quadrants[4]++; // bottom-right
        }

        Console.WriteLine(quadrants[1] *  quadrants[2] * quadrants[3] * quadrants[4]);

    }
}
