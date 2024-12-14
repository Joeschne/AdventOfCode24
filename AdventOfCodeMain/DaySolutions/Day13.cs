using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCodeMain.DaySolutions;

internal class Day13
{
    public static void Run()
    {
        string challengeInput = FileReader.ReadFromFile("Day13.1");
        
        string pattern = @"X\+(\d+), Y\+(\d+)|X=(\d+), Y=(\d+)";
        Regex regex = new Regex(pattern);

        List<(int, int)> aButtons = new();
        List<(int, int)> bButtons = new();
        List<(int, int)> prizes = new();

        var matches = regex.Matches(challengeInput);
        for (int i = 0; i < matches.Count; i += 3)
        {
            var matchA = matches[i];
            aButtons.Add((int.Parse(matchA.Groups[1].Value), int.Parse(matchA.Groups[2].Value)));

            var matchB = matches[i + 1];
            bButtons.Add((int.Parse(matchB.Groups[1].Value), int.Parse(matchB.Groups[2].Value)));

            var matchPrize = matches[i + 2];
            prizes.Add((int.Parse(matchPrize.Groups[3].Value), int.Parse(matchPrize.Groups[4].Value)));
        }

        // my math is shitty so brute forcing it is
        int totalPrice = 0;
        for(int i = 0; i < prizes.Count; i++)
        {
            totalPrice += CalculatePrice(aButtons[i], bButtons[i], prizes[i]);
        }

        Console.WriteLine(totalPrice);
    }
    private static int CalculatePrice((int x, int y) aButton, (int x, int y) bButton, (int x, int y) prizeLocation)
    {

        List<int> possiblePrices = new();

        for (int a = 1; a <= 100; a++)
        {
            for (int b = 1; b <= 100; b++)
            {
                if ((aButton.x * a + bButton.x * b, aButton.y * a + bButton.y * b) == prizeLocation)
                {
                    possiblePrices.Add(a * 3  + b);
                }
            }
        }

        if (possiblePrices.Count == 0) return 0;

        return possiblePrices.Min();

    }
}
