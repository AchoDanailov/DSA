namespace DSA.TreeDataStructures.Tests.Utils;

internal static class ArrayHelpers
{
    internal static int[] RandomFilledIntArray(
        int length = 4,
        bool isReacuranceAllowed = false,
        int randomnessLowerThreshold = 0,
        int randomnessUpperThreshold = 100)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(
                message: "Length can not be a negative number",
                paramName: nameof(length));
        }

        if (randomnessLowerThreshold > randomnessUpperThreshold)
        {
            throw new ArgumentException(
                message: "Lower randomness threshold can not be larger than the upper randomness threshold.",
                paramName: $"{nameof(randomnessLowerThreshold)} and {nameof(randomnessUpperThreshold)}");
        }

        int[] arr = new int[length];
        for (int i = 0; i < arr.Length; i++)
        {
            int numToAdd = Random.Shared.Next(
                minValue: randomnessLowerThreshold,
                maxValue: randomnessUpperThreshold);

            if (!isReacuranceAllowed)
            {
                while (arr.Take(i).Contains(numToAdd))
                {
                    numToAdd = Random.Shared.Next(
                            minValue: randomnessLowerThreshold,
                            maxValue: randomnessUpperThreshold);
                }
            }

            arr[i] = numToAdd;
        }

        return arr;
    }
}
