namespace AdventOfCodeMain.DaySolutions;

internal class Day07
{
    public static void Run()
    {
        string[] challengeInput = FileReader.ReadFromFile("Day7.1")
            .Split(new string[] { ":", "\r\n" }, StringSplitOptions.None);

        long[] testValues = challengeInput
            .Where((_, index) => index % 2 == 0)
            .Select(long.Parse)
            .ToArray();

        int[][] calibrations = challengeInput
            .Where((_, index) => index % 2 != 0) 
            .Select(equationNumbers =>
                equationNumbers.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(part => int.Parse(part)) 
                    .ToArray()
            )
            .ToArray();


        long totalCalibration = FindViableCalibrations(testValues, calibrations);

        Console.WriteLine(totalCalibration);
    }

    private static long FindViableCalibrations(long[] testValues, int[][] calibrations)
    {
        long totalCalibration = 0;

        for (int i = 0; i < testValues.Length; i++)
        {
            if (IsViableCalculation(testValues[i], calibrations[i]))
            {
                totalCalibration += testValues[i];
            }

        }

        return totalCalibration;
    }

    private static bool IsViableCalculation(long testValue, int[] calibration, int currentIndex = 0, long currentValue = 0)
    {
        if (currentValue > testValue)
            return false;

        bool endOfCalibrationReached = currentIndex == calibration.Length;
        if (endOfCalibrationReached)
        {
            return currentValue == testValue;
        }

        long num = calibration[currentIndex];

        // try adding
        if (IsViableCalculation(testValue, calibration, currentIndex + 1, currentValue + num))
        {
            return true;
        }

        // try multiplying
        if (IsViableCalculation(testValue, calibration, currentIndex + 1, currentValue * num))
        {
            return true;
        }

        return false;
    }
}
