using System.Text.RegularExpressions;

namespace AdventOfCodeMain.DaySolutions;

internal class Day03
{
    static public void Run()
    {
        string challengeInput = FileReader.ReadFromFile("Day3.1");
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";  
        Regex regex = new Regex(pattern);
        int totalSum = 0;

        MatchCollection matches = regex.Matches(challengeInput);

        foreach (Match match in matches)
        {
            int num1 = int.Parse(match.Groups[1].Value);
            int num2 = int.Parse(match.Groups[2].Value);

            totalSum += num1 * num2;
        }
        Console.WriteLine($"Total Sum of Products: {totalSum}");
    }
}
