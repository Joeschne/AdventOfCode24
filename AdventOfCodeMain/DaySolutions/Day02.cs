namespace AdventOfCodeMain.DaySolutions;

internal class Day02
{
    public static void Run()
    {
        string challengeInput = FileReader.ReadFromFile("Day2.1");
        string[] reports = challengeInput.Split("\r\n");
        int safeReports = 0;
        foreach (string report in reports)
        {
            int[] levels = report.Split(" ").Select(int.Parse).ToArray();
            bool ascendingTrue = true;
            bool descendingTrue = true;
            int i = 0;
            while ((ascendingTrue || descendingTrue) && i < levels.Length -1)
            {
                int difference = levels[i] - levels[i + 1];

                // Check for descending condition
                if (descendingTrue && (difference < 1 || difference > 3))
                {
                    descendingTrue = false;
                }

                // Check for ascending condition
                if (ascendingTrue && (difference > -1 || difference < -3))
                {
                    ascendingTrue = false;
                }

                i++;
            }
            if (ascendingTrue || descendingTrue)
            {
                safeReports++;
            }
        }
        Console.WriteLine(safeReports);
    }
}
