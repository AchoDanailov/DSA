namespace DSA.LinearDataStructures.Tests.Utils;

public static class ArrayHelpers
{
    public static int[] RandomFilledIntArray(
        int length = 4,
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
            arr[i] = Random.Shared.Next(
                minValue: randomnessLowerThreshold,
                maxValue: randomnessUpperThreshold);
        }

        return arr;
    }
}
