using DSA.LinearDataStructures.ArrayDeque;
using DSA.LinearDataStructures.Interfaces;
using DSA.LinearDataStructures.Tests.Utils;

namespace DSA.LinearDataStructures.Tests;

[TestFixture]
public class ArrayDequeTests
{
    [Test]
    public void Instantiation_WithEmptyCtor_WorksCorrectly()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.That(deque.Size, Is.EqualTo(0));
    }

    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(20)]
    public void Instantiation_WithCollection_WorksCorrectly(int size)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
    
        bool allEqual = true;
        for (int i = 0; i < size; i++)
        {
            if (!arr[i].Equals(deque[i]))
            {
                allEqual = false;
                break;
            }
        }
    
        Assert.That(allEqual, Is.True);
        Assert.That(deque.Size, Is.EqualTo(arr.Length));
    }

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(50, 10)]
    public void Add_ShouldWorkCorrectly(int size, int numberOfTimesAdding)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(size);
        IArrayDeque<int> deque = new ArrayDeque<int>(arr);
        int[] numbersToAdd = ArrayHelpers.RandomFilledIntArray(numberOfTimesAdding);

        for (int i = 0; i < numbersToAdd.Length; i++)
        {
            deque.Add(numbersToAdd[i]);
            Assert.That(deque.Size, Is.EqualTo(size + i + 1));
        }

        for (int i = 0; i < numbersToAdd.Length; i++)
        {
            Assert.That(deque.Contains(numbersToAdd[i]));
        }
    }
    
    [Test]
    public void AddFirst_WorksCorrectly()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
    
        for (int i = 0; i < Random.Shared.Next(100); i++)
        {
            deque.AddFirst(Random.Shared.Next());
            Assert.That(deque.Size, Is.EqualTo(i + 1));
        }
    }
    
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void AddFirst_MultipleTimes_WorksCorrectly(int numberOfAdditions)
    {
        ArrayDeque<int> deque = GetRandomDequeFilledWithAddFirst(Random.Shared.Next(100));
        
        int rndNum = Random.Shared.Next();
        deque.AddFirst(rndNum);
        Assert.That(deque[0], Is.EqualTo(rndNum));
        
        for (int i = 0; i < numberOfAdditions; i++)
        {
            deque.AddFirst(Random.Shared.Next());
        }
    
        Assert.That(deque.Contains(rndNum), Is.True);
    }

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(10, 11)]
    [TestCase(10, 50)]
    public void Insert_WhenPassedOutOfBoundsIndex_ShouldThrow(int size, int index) 
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);

        Assert.Throws<IndexOutOfRangeException>(() => deque.Insert(index, Random.Shared.Next()));
    }
    
    [Test]
    public void Insert_WorksCorrectly()
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Random.Shared.Next() + 1);
        int[] insertNums = ArrayHelpers.RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Random.Shared.Next() + 1);

        ArrayDeque<int> deque = new ArrayDeque<int>(arr);

        for (int i = 0; i < insertNums.Length; i++)
        {
            deque.Insert(
                index: Random.Shared.Next(0, arr.Length),
                element: insertNums[i]);

            Assert.That(deque.Size, Is.EqualTo(arr.Length + i + 1));
        }
    }

    [TestCase(-1, 0)]
    [TestCase(-10, 5)]
    [TestCase(10, 5)]
    [TestCase(0, 0)]
    public void Remove_WhenPassedIndexOutOfBounds_ShouldThrow(int index, int size)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
        Assert.Throws<IndexOutOfRangeException>(() => deque.RemoveAt(index));
    }
    
    [Test]
    public void Remove_WorksCorrectly()
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Random.Shared.Next() + 1);
    
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
    
        int numberOfRemoves = Random.Shared.Next(1, arr.Length);
        for (int i = 0; i < numberOfRemoves; i++)
        {
            int indexToRemoveAt = Random.Shared.Next(0, arr.Length - 1 - i);
            deque.RemoveAt(indexToRemoveAt);
    
            Assert.That(deque.Size, Is.EqualTo(arr.Length - 1 - i));
        }
    }
    
    [Test]
    public void RemoveFirst_WhenDequeEmpty_Throws()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.Throws<InvalidOperationException>(() => deque.RemoveFirst());
    }
    
    [Test]
    public void RemoveFirst_WorksCorrectly()
    {
        int numOfAdditions = Random.Shared.Next(maxValue: 100);
        ArrayDeque<int> deque = GetRandomDequeFilledWithAddFirst(numOfAdditions);
    
        int randomNumToAddFirst = Random.Shared.Next();
        deque.AddFirst(randomNumToAddFirst);
    
        Assert.That(randomNumToAddFirst, Is.EqualTo(deque.RemoveFirst()));
        Assert.That(deque.Size, Is.EqualTo(numOfAdditions));
    }
    
    [Test]
    public void RemoveLast_WhenEmpty_ShouldThrow()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.That(deque.Size, Is.EqualTo(0));
        Assert.Throws<InvalidOperationException>(() => deque.RemoveLast());
    }

    [Test]
    public void RemoveLast_WhenDequeFilledWithAddFirst_ShouldWorkCorrectly()
    {
        int numOfAdditions = Random.Shared.Next(maxValue: 100);
        ArrayDeque<int> deque = GetRandomDequeFilledWithAddFirst(numOfAdditions);
        
        int randomNumAddLast = Random.Shared.Next();
        deque.Add(randomNumAddLast);
        
        Assert.That(randomNumAddLast, Is.EqualTo(deque.RemoveLast()));
        Assert.That(deque.Size, Is.EqualTo(numOfAdditions));
    }
    
    [Test]
    public void IndexOf_WhenElementNotFound_ReturnsMinusOne()
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: Random.Shared.Next(maxValue: 100),
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: 50);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
    
        Assert.That(
            deque.IndexOf(Random.Shared.Next(51, int.MaxValue)), Is.EqualTo(-1));
    }
    
    [Test]
    public void IndexOf_WorksCorrectly()
    {
        int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
    
        Assert.That(deque.IndexOf(0), Is.EqualTo(0));
        Assert.That(deque.IndexOf(5), Is.EqualTo(5));
        Assert.That(deque.IndexOf(9), Is.EqualTo(9));
    }
    
    [Test]
    public void Contains_WhenElementNotFound_ReturnsFalse()
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: 50);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
    
        Assert.That(
            deque.Contains(Random.Shared.Next(51, int.MaxValue)), Is.False);
    }
    
    [Test]
    public void Contains_WorksCorrectly()
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray();
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
    
        int numToInsert = Random.Shared.Next();
        deque.Insert(Random.Shared.Next(0, arr.Length), numToInsert);
    
        Assert.That(deque.Contains(numToInsert), Is.True);
    }
    
    [Test]
    public void IsEmpty_WhenNotEmpty_ReturnsFalse()
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray();
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
    
        Assert.That(deque.IsEmpty(), Is.False);
    }
    
    [Test]
    public void IsEmpty_WhenEmpty_ReturnsTrue()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.That(deque.IsEmpty(), Is.True);
    }
    
    [Test]
    public void Contains_WhenElementIsntContained_ReturnsFalse()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.That(deque.Contains(Random.Shared.Next()), Is.False);
    }
    
    private static ArrayDeque<int> GetRandomDequeFilledWithAddFirst(int numOfAdditions = 100)
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        for (int i = 0; i < numOfAdditions; i++)
        {
            deque.AddFirst(Random.Shared.Next());
        }
    
        return deque;
    }
}