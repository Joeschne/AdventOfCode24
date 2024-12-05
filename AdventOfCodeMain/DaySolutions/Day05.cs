using System.Diagnostics;

namespace AdventOfCodeMain.DaySolutions;

internal class Day05
{
    public static void Run()
    {
        string[] challengeInput = FileReader.ReadFromFile("Day5.1").Split("\r\n\r\n");

        Dictionary<int, List<int>> rulesetDict = challengeInput[0]
            .Split(new string[] { "|", "\r\n" }, StringSplitOptions.None)
            .Select(int.Parse)
            .Chunk(2)
            .Select(chunk => new { Key = chunk[0], Value = chunk[1] }) // Create key-value pairs
            .GroupBy(x => x.Key) // Group by Key ([0])
            .ToDictionary(
                group => group.Key,
                group => group.Select(x => x.Value).ToList() // Group all [0] values into an array
            );

        int[][] updates = challengeInput[1]
            .Split("\r\n")
            .Select(update => update.Split(',').Select(int.Parse).ToArray())
            .ToArray();

        int correctMiddlePageNumbers = 0;
        int incorrectMiddlePageNumbers = 0;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        foreach (int[] update in updates)
        {
            bool validUpdate = true;
            int currentPage = 0;
            while (validUpdate && currentPage < update.Length)
            {
                validUpdate = ViolatesRuleset(rulesetDict, update, currentPage);
                currentPage++;
            }
            if (validUpdate)
            {
                correctMiddlePageNumbers += update[(update.Length - 1) / 2];
            }
        }
        sw.Stop();
        Console.WriteLine(correctMiddlePageNumbers);
        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    private static bool ViolatesRuleset(Dictionary<int, List<int>> rulesetDict, int[] update, int currentPage)
    {
        if (rulesetDict.ContainsKey(update[currentPage]))
        {
            for (int i = 0; i < currentPage; i++)
            {
                if (rulesetDict[update[currentPage]].Contains(update[i]))
                    return false;
            }
        }
        return true;
    }
}
