using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCodeMain.DaySolutions;

internal class Day09
{
    public static void Run()
    {
        string challengeInput = FileReader.ReadFromFile("Day9.1");
        List<string> fileMap = new();
        for (int i = 0; i < challengeInput.Length; i++)
        {
            if (i % 2 == 0)
            {
                fileMap.AddRange(Enumerable.Repeat((i / 2).ToString(), int.Parse(challengeInput[i].ToString())));
            }
            else
            {
                fileMap.AddRange(Enumerable.Repeat(".", int.Parse(challengeInput[i].ToString())));
            }
        }
        long checksum = 0;
        int rightIndex = fileMap.Count - 1;
        for (int i = 0; i < fileMap.Count;i++)
        {
            if (fileMap[i] != ".")
            {
                checksum += i * int.Parse(fileMap[i]);
                continue;
            }
            if (!fileMap.Skip(i).Any(n => int.TryParse(n, out _))) break;
            while (fileMap[rightIndex] == ".") rightIndex--;
            (fileMap[i], fileMap[rightIndex]) = (fileMap[rightIndex], fileMap[i]);
            checksum += i * int.Parse(fileMap[i]);
        }
        Console.WriteLine(checksum);
    }

}
