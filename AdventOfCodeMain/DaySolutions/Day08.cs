namespace AdventOfCodeMain.DaySolutions;

internal class Day08
{
    public static void Run()
    {
        string[] challengeInput = FileReader.ReadFromFile("Day8.1").Split("\r\n");
        int numRows = challengeInput.Length;
        int numCols = challengeInput[0].Length;

        Dictionary<char, List<(int, int)>> antennaPositions = FindAntennas(challengeInput, numRows, numCols);

        int nodeCount = FindAllNodes(antennaPositions, numRows, numCols);

        Console.WriteLine(nodeCount);


    }

    private static Dictionary<char, List<(int, int)>> FindAntennas(string[] map, int numRows, int numCols)
    {
        Dictionary<char, List<(int, int)>> antennaPositions = new();
        for (int i = 0; i < numRows * numCols; i++)
        {
            int xPos = i / numCols;
            int yPos = i % numCols;

            char currentLocationChar = map[xPos][yPos];

            if (currentLocationChar != '.')
            {
                if (!antennaPositions.TryGetValue(currentLocationChar, out var positions))
                {
                    positions = new List<(int, int)>();
                    antennaPositions[currentLocationChar] = positions;
                }
                positions.Add((xPos, yPos));
            }
        }
        return antennaPositions;
    }

    private static int FindAllNodes(Dictionary<char, List<(int, int)>> antennaPositions, int numRows, int numCols)
    {
        HashSet<(int, int)> nodes = new();
        foreach (var antennaGroup in antennaPositions)
        {
            nodes = MarkNodes(antennaGroup.Value, nodes, numRows, numCols);
        }
        return nodes.Count;
    }

    private static HashSet<(int, int)> MarkNodes(List<(int row, int col)> positions, HashSet<(int, int)> nodes, int numRows, int numCols)
    {
        
        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = i + 1; j < positions.Count; j++) // for each combination (i choose 2, iC2)
            {
                (int, int) vector = SubtractTuples(positions[j], positions[i]);
                (int row, int col) node1 = SubtractTuples(positions[i], vector);
                (int row, int col) node2 = AddTuples(positions[j], vector);

                if (node1.row >= 0 && node1.row < numRows && node1.col >= 0 && node1.col < numCols)
                {
                    nodes.Add(node1);
                }
                if (node2.row >= 0 && node2.row < numRows && node2.col >= 0 && node2.col < numCols)
                {
                    nodes.Add(node2);
                }
            }
        }
        return nodes;
    }

    private static (int, int) AddTuples((int, int) tuple1, (int, int) tuple2) => (tuple1.Item1 + tuple2.Item1, tuple1.Item2 + tuple2.Item2);
    private static (int, int) SubtractTuples((int, int) tuple1, (int, int) tuple2) => (tuple1.Item1 - tuple2.Item1, tuple1.Item2 - tuple2.Item2);
}
