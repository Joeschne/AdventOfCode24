namespace AdventOfCodeMain.DaySolutions;

internal class Day11
{
    public static void Run()
    {
        List<string> challengeInput = FileReader.ReadFromFile("Day11.1").Split(" ").ToList();

        for (int i = 0; i < 25; i++)
        {
            Blink(challengeInput);
        }

        Console.WriteLine(challengeInput.Count);
    }
    private static void Blink(List<string> challengeInput)
    {
        for(int i = 0; i < challengeInput.Count; i++)
        {
            if (long.Parse(challengeInput[i]) == 0) challengeInput[i] = "1";
            else if (challengeInput[i].Length % 2 == 0)
            {
                int mid = challengeInput[i].Length / 2;

                string firstHalf = challengeInput[i].Substring(0, mid);
                string secondHalf = challengeInput[i].Substring(mid);

                challengeInput[i] = firstHalf;
                challengeInput.Insert(i + 1, (long.Parse(secondHalf).ToString()));
                i++;
            }
            else
            {
                challengeInput[i] = (long.Parse(challengeInput[i]) * 2024).ToString();
            }
        }
    }
}
