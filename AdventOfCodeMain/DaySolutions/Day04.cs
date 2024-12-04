using System.Text.RegularExpressions;

namespace AdventOfCodeMain.DaySolutions;

internal class Day04
{
    public static void Run()
    {
        string challengeInput = FileReader.ReadFromFile("Day4.1");
        string pattern = @"(?=(XMAS|SAMX))";

        string[] horizontalSlices = challengeInput.Split("\r\n");

        int matchCounter = 0;

        int rowLength = horizontalSlices[0].Length - 1;
        int columnLength = horizontalSlices.Length - 1;

        foreach (string horizontalSlice in horizontalSlices)
        {
            matchCounter += Regex.Matches(horizontalSlice, pattern).Count;
        }

        for (int column = 0; column <= rowLength; column++)
        {
            string verticalSlice = new string(horizontalSlices.Select(slice => slice[column]).ToArray());
            matchCounter += Regex.Matches(verticalSlice, pattern).Count;

            string currentDiagonalRightSlice = "";
            int columnIndex = column;
            for (int row = 0; row <= columnLength; row++)
            {
                if (columnIndex <= rowLength)
                {
                    currentDiagonalRightSlice += horizontalSlices[row][columnIndex];
                    columnIndex++;
                }
                else
                {
                    matchCounter += Regex.Matches(currentDiagonalRightSlice, pattern).Count;
                    currentDiagonalRightSlice = "";
                    columnIndex = 0;
                }
            }
            matchCounter += Regex.Matches(currentDiagonalRightSlice, pattern).Count;
            string currentDiagonalLeftSlice = "";
            columnIndex = column;
            for (int row = 0; row <= columnLength; row++)
            {
                if (columnIndex >= 0)
                {
                    currentDiagonalLeftSlice += horizontalSlices[row][columnIndex];
                    columnIndex--;
                }
                else
                {
                    matchCounter += Regex.Matches(currentDiagonalLeftSlice, pattern).Count;
                    
                    currentDiagonalLeftSlice = horizontalSlices[row][rowLength].ToString();
                    columnIndex = rowLength - 1;
                }
            }
            matchCounter += Regex.Matches(currentDiagonalLeftSlice, pattern).Count;
        }
        Console.WriteLine(matchCounter);
    }
}
