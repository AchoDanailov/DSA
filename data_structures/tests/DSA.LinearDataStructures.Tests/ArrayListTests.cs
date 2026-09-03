using DSA.LinearDataStructures.ArrayList;
using DSA.LinearDataStructures.Interfaces;

namespace DSA.LinearDataStructures.Tests;

[TestFixture]
public class LinearDataStructuresTests
{
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(200)]
    public void OnInstantiation_PassingCapacity_ShouldChangeStructureCapacity(int capacity)
    {
        IArrayList<int> arrayList = new ArrayList<int>(capacity);
        Assert.That(arrayList.Capacity == capacity);
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(5)]
    public void OnInstantiation_PassingACollection_WorksCorrectly(int size)
    {
        int[] arr = RandomFilledIntArray(size);
        IArrayList<int> arrList = new ArrayList<int>(arr);

        Assert.That(arrList.Size == size);

        bool allStored = true;
        for (int i = 0; i < size; i++)
        {
            if (arr[i] != arrList[i])
            {
                allStored = false;
                break;
            }
        }
        Assert.That(allStored, Is.True);
    }

    [Test]
    public void ListWithElements_WhenChangingCapacity_DoesNotLooseElements()
    {
        int[] intArr = new int[] { 1, 2, 3, 4 };
        IArrayList<int> arrList = new ArrayList<int>(intArr);

        arrList.Capacity *= 2;

        Assert.That(arrList.Contains(1) && arrList.Contains(2) && arrList.Contains(3) && arrList.Contains(4));
    }


    [TestCase(-1)]
    [TestCase(-200)]
    public void OnInstantiation_PassingToSmallIndex_ShouldThrow(int capacity)
    {
        Assert.Throws<IndexOutOfRangeException>(() => new ArrayList<int>(capacity));
    }

    [TestCase(4, 2)]
    [TestCase(2, 1)]
    public void ListWithSize_SettingCapacitySmallerThanSize_ShouldThrow(int size, int capacity)
    {
        int[] arr = RandomFilledIntArray(size);
        IArrayList<int> arrList = new ArrayList<int>(arr);
        Assert.Throws<IndexOutOfRangeException>(() => arrList.Capacity = capacity);
    }

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(50, 10)]
    public void Add_ShouldWorkCorrectly(int size, int numberOfTimesAdding)
    {
        int[] arr = RandomFilledIntArray(size);
        IArrayList<int> arrayList = new ArrayList<int>(arr);
        int[] numbersToAdd = RandomFilledIntArray(numberOfTimesAdding);

        for (int i = 0; i < numbersToAdd.Length; i++)
        {
            arrayList.Add(numbersToAdd[i]);
            Assert.That(arrayList.Size, Is.EqualTo(size + i + 1));
        }

        for (int i = 0; i < numbersToAdd.Length; i++)
        {
            Assert.That(arrayList.Contains(numbersToAdd[i]));
        }
    }

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(10, 11)]
    [TestCase(10, 50)]
    public void Insert_WhenPassedOutOfBoundsIndex_ShouldThrow(int size, int index) 
    {
        int[] arr = RandomFilledIntArray(length: size);
        ArrayList<int> arrayList = new ArrayList<int>(arr);

        Assert.Throws<IndexOutOfRangeException>(() => arrayList.Insert(index, Random.Shared.Next()));
    }

    [TestCase(3, 5, 2)]
    [TestCase(2, 8, 1)]
    public void Insert_WorksCorrectly(int size, int capacity, int index)
    {
        int[] arr = RandomFilledIntArray(length: size);
        ArrayList<int> arrayList = new ArrayList<int>(arr);
        arrayList.Capacity = capacity;

        int numToInsert = Random.Shared.Next();
        arrayList.Insert(index, numToInsert);

        Assert.That(arrayList.Size, Is.EqualTo(size + 1));
        Assert.That(arrayList[index], Is.EqualTo(numToInsert));
    }

    [Test]
    public void Insert_MultipleTimes_WorksCorrectly()
    {
        int[] arr = RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Random.Shared.Next() + 1);
        int[] insertNums = RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Random.Shared.Next() + 1);
        
        ArrayList<int> arrayList = new ArrayList<int>(arr);
        int initialCapacity = arrayList.Capacity;
        
        for (int i = 0; i < insertNums.Length; i++)
        {
            arrayList.Insert(
                index: Random.Shared.Next(0, arr.Length),
                element: insertNums[i]);
            
            Assert.That(arrayList.Size, Is.EqualTo(arr.Length + i + 1));
        }

        Assert.That(initialCapacity, Is.Not.EqualTo(arrayList.Capacity));
    }

    [TestCase(-1, 0)]
    [TestCase(-10, 5)]
    [TestCase(10, 5)]
    [TestCase(0, 0)]
    public void Remove_WhenPassedIndexOutOfBounds_ShouldThrow(int index, int size)
    {
        int[] arr = RandomFilledIntArray(length: size);
        ArrayList<int> arrList = new ArrayList<int>(arr);

        Assert.Throws<IndexOutOfRangeException>(() => arrList.Remove(index));
    }

    [TestCase(1, 8, 0)]
    [TestCase(3, 7, 1)]
    public void Remove_ShouldWorkCorrectly(int size, int capacity, int index)
    {
        int[] arr = RandomFilledIntArray(length: size);
        ArrayList<int> arrList = new ArrayList<int>(arr);
        arrList.Capacity = capacity;

        int numThatShouldGoOnIndexPossition = -1;
        if (index < size - 1)
            numThatShouldGoOnIndexPossition = arrList[index + 1];
        
        int removed = arrList.Remove(index);

        Assert.That(arrList.Size, Is.EqualTo(size - 1));
        Assert.That(arrList.Contains(removed), Is.False);
        
        if (index < size - 1)
            Assert.That(arrList[index], Is.EqualTo(numThatShouldGoOnIndexPossition));
    }

    [Test]
    public void Remove_MultipleTimes_ShouldWorkCorrectly()
    {
        int[] arr = RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Random.Shared.Next() + 1);
        
        ArrayList<int> arrList = new ArrayList<int>(arr);
        int initialCapacity = arrList.Capacity;
        
        int numberOfRemoves = Random.Shared.Next(1, arr.Length);
        for (int i = 0; i < numberOfRemoves; i++)
        {
            int indexToRemoveAt = Random.Shared.Next(0, arr.Length - 1 - i);
            arrList.Remove(indexToRemoveAt);
            
            Assert.That(arrList.Size, Is.EqualTo(arr.Length - 1 - i));
        }
    }

    [Test]
    public void IndexOf_WhenElementNotFound_ReturnsMinusOne()
    {
        int[] arr = RandomFilledIntArray(
            length: Random.Shared.Next(maxValue: 100),
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: 50);
        ArrayList<int> arrList = new ArrayList<int>(arr);
        
        Assert.That(
            arrList.IndexOf(Random.Shared.Next(51, int.MaxValue)), Is.EqualTo(-1));
    }

    [Test]
    public void IndexOf_WorksCorrectly()
    {
        int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ArrayList<int> arrList = new ArrayList<int>(arr);
        
        Assert.That(arrList.IndexOf(0), Is.EqualTo(0));
        Assert.That(arrList.IndexOf(5), Is.EqualTo(5));
        Assert.That(arrList.IndexOf(9), Is.EqualTo(9));
    }

    [Test]
    public void Contains_WhenElementNotFound_ReturnsFalse()
    {
        int[] arr = RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: 50);
        ArrayList<int> arrList = new ArrayList<int>(arr);
        
        Assert.That(
            arrList.Contains(Random.Shared.Next(51, int.MaxValue)), Is.False);
    }

    [Test]
    public void Contains_WorksCorrectly()
    {
        int[] arr = RandomFilledIntArray();
        ArrayList<int> arrList = new ArrayList<int>(arr);

        int numToInsert = Random.Shared.Next();
        arrList.Insert(Random.Shared.Next(0, arr.Length), numToInsert);
        
        Assert.That(arrList.Contains(numToInsert), Is.True);
    }

    [Test]
    public void IsEmpty_WhenNotEmpty_ReturnsFalse()
    {
        int[] arr = RandomFilledIntArray();
        ArrayList<int> arrList = new ArrayList<int>(arr);
        
        Assert.That(arrList.IsEmpty(), Is.False);
    }

    [Test]
    public void IsEmpty_WhenEmpty_ReturnsTrue()
    {
        ArrayList<int> arrList = new ArrayList<int>();
        Assert.That(arrList.IsEmpty(), Is.True);
    }

    private static int[] RandomFilledIntArray(
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