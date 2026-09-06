using DSA.LinearDataStructures.Tests.Utils;
using DSA.LinearDataStructures.ArrayDeque;
using DSA.LinearDataStructures.Interfaces;

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

    [TestCase(5, 1)]
    [TestCase(1, 0)] // TODO: validate this one
    [TestCase(10, 9)]
    [TestCase(15, 1)]
    public void InstantiationWithCapacity_WhenCapacityIsLessThanSize_ShouldThrows(
        int size,
        int capacity)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        Assert.Throws<ArgumentException>(() => new ArrayDeque<int>(capacity: capacity));
    }

    [TestCase(0)] 
    [TestCase(1)] 
    [TestCase(5)] 
    [TestCase(21)] 
    public void Instantiation_WithCapaicityParam_WorksCorrectly(int capacity)
    {
        ArrayDeque<int> deque = new ArrayDeque<int>(capacity);
        int actual = deque.Capacity;
        int expected = capacity % 2 == 0 ? capacity + 1 : capacity;
        Assert.That(actual, Is.EqualTo(expected));
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

    // NOTE: All tests from ArrayList, SinglyLinkedListdeque and DoublyLinkedListQueue should also pass after implementation

    // ArrayList Tests (end at 287)
    [Test]
    public void ListWithElements_WhenChangingCapacity_DoesNotLooseElements()
    {
        int[] intArr = new int[] { 1, 2, 3, 4 };
        IArrayDeque<int> deque = new ArrayDeque<int>(intArr);

        deque.Capacity *= 2;

        Assert.That(deque.Contains(1) && deque.Contains(2) && deque.Contains(3) && deque.Contains(4));
    }


    [TestCase(-1)]
    [TestCase(-200)]
    public void OnInstantiation_PassingToSmallIndex_ShouldThrow(int capacity)
    {
        Assert.Throws<IndexOutOfRangeException>(() => new ArrayDeque<int>(capacity));
    }

    [TestCase(4, 2)]
    [TestCase(2, 1)]
    public void ListWithSize_SettingCapacitySmallerThanSize_ShouldThrow(int size, int capacity)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(size);
        IArrayDeque<int> deque = new ArrayDeque<int>(arr);
        Assert.Throws<IndexOutOfRangeException>(() => deque.Capacity = capacity);
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

    [TestCase(3, 5, 2)]
    [TestCase(2, 8, 1)]
    public void Insert_WorksCorrectly(int size, int capacity, int index)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
        deque.Capacity = capacity;

        int numToInsert = Random.Shared.Next();
        deque.Insert(index, numToInsert);

        Assert.That(deque.Size, Is.EqualTo(size + 1));
        Assert.That(deque[index], Is.EqualTo(numToInsert));
    }

    [Test]
    public void Insert_MultipleTimes_WorksCorrectly()
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
        int initialCapacity = deque.Capacity;
        
        for (int i = 0; i < insertNums.Length; i++)
        {
            deque.Insert(
                index: Random.Shared.Next(0, arr.Length),
                element: insertNums[i]);
            
            Assert.That(deque.Size, Is.EqualTo(arr.Length + i + 1));
        }

        Assert.That(initialCapacity, Is.Not.EqualTo(deque.Capacity));
    }

    [TestCase(-1, 0)]
    [TestCase(-10, 5)]
    [TestCase(10, 5)]
    [TestCase(0, 0)]
    public void Remove_WhenPassedIndexOutOfBounds_ShouldThrow(int index, int size)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);

        Assert.Throws<IndexOutOfRangeException>(() => deque.Remove(index));
    }

    [TestCase(1, 8, 0)]
    [TestCase(3, 7, 1)]
    public void Remove_ShouldWorkCorrectly(int size, int capacity, int index)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
        deque.Capacity = capacity;

        int numThatShouldGoOnIndexPossition = -1;
        if (index < size - 1)
            numThatShouldGoOnIndexPossition = deque[index + 1];
        
        int removed = deque.Remove(index);

        Assert.That(deque.Size, Is.EqualTo(size - 1));
        Assert.That(deque.Contains(removed), Is.False);
        
        if (index < size - 1)
            Assert.That(deque[index], Is.EqualTo(numThatShouldGoOnIndexPossition));
    }

    [Test]
    public void Remove_MultipleTimes_ShouldWorkCorrectly()
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Random.Shared.Next() + 1);
        
        ArrayDeque<int> deque = new ArrayDeque<int>(arr);
        int initialCapacity = deque.Capacity;
        
        int numberOfRemoves = Random.Shared.Next(1, arr.Length);
        for (int i = 0; i < numberOfRemoves; i++)
        {
            int indexToRemoveAt = Random.Shared.Next(0, arr.Length - 1 - i);
            deque.Remove(indexToRemoveAt);
            
            Assert.That(deque.Size, Is.EqualTo(arr.Length - 1 - i));
        }
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

    // SinglyLinkedListStack tests (end at 368)
    [Test]
    public void Push_WorksCorrectly()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();

        for (int i = 0; i < Random.Shared.Next(100); i++)
        {
            deque.Push(Random.Shared.Next());
            Assert.That(deque.Size, Is.EqualTo(i + 1));
        }
    }

    [Test]
    public void Pop_WhenEmpty_ShouldThrow()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.Throws<InvalidOperationException>(() => deque.Pop());
    }
    
    [Test]
    public void Pop_WorksCorrectly()
    {
        int numOfPushes = Random.Shared.Next(maxValue: 100);
        ArrayDeque<int> deque = GetRandomFilledDequeStack(numOfPushes);

        int randomNumToPush = Random.Shared.Next();
        deque.Push(randomNumToPush);
        
        Assert.That(randomNumToPush, Is.EqualTo(deque.Pop()));
        Assert.That(deque.Size, Is.EqualTo(numOfPushes));
    }

    [Test]
    public void Peek_WhenEmpty_Throws()
    {
        int numOfPushes = Random.Shared.Next(maxValue: 100);
        ArrayDeque<int> deque = GetRandomFilledDequeStack(numOfPushes);

        int randomNumToPush = Random.Shared.Next();
        deque.Push(randomNumToPush);

        Assert.That(randomNumToPush, Is.EqualTo(deque.Peek()));
        Assert.That(deque.Size, Is.EqualTo(numOfPushes + 1));
    }

    [Test]
    public void Contains_WhenElementIsntContained_ReturnsFalse()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.That(deque.Contains(Random.Shared.Next()), Is.False);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void Contains_WorksCorrectly(int pushesAfterPushingTarget)
    {
        ArrayDeque<int> deque = GetRandomFilledDequeStack(Random.Shared.Next(100));
        int pushed = Random.Shared.Next();
        deque.Push(pushed);
        
        for (int i = 0; i < pushesAfterPushingTarget; i++)
        {
            deque.Push(Random.Shared.Next());
        }

        Assert.That(deque.Contains(pushed), Is.True);
    }

    private static ArrayDeque<int> GetRandomFilledDequeStack(int numOfPushes = 100)
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        for (int i = 0; i < numOfPushes; i++)
        {
            deque.Push(Random.Shared.Next());
        }

        return deque;
    }

    // DoublyLinkedListQueue tests (end at 470)
    [Test]
    public void Enqueue_WorksCorrectly()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();

        int numberOfElements = Random.Shared.Next(100);
        int[] enqueued = new int[numberOfElements];

        for (int i = 0; i < numberOfElements; i++)
        {
            int randomNumToEnqueue = Random.Shared.Next();
            enqueued[i] = randomNumToEnqueue;
            deque.Enqueue(randomNumToEnqueue);
            Assert.That(deque.Size, Is.EqualTo(i + 1));
        }

        for (int i = 0; i < enqueued.Length; i++)
        {
            Assert.That(enqueued[i], Is.EqualTo(deque.Dequeue()));
        }

        Assert.That(deque.Size, Is.EqualTo(0));
    }

    [Test]
    public void Dequeue_WhenEmpty_Throws()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.That(deque.Size, Is.EqualTo(0));
        Assert.Throws<InvalidOperationException>(() => deque.Dequeue());
    }

    [TestCase(1, 20)]
    [TestCase(2, 0)]
    [TestCase(10, 1)]
    public void Dequeue_WorksCorrectly(int enqueueBeforeTarget, int engueueAfterTarget)
    {
        ArrayDeque<int> deque = GetRandomFilledDequeQueue(enqueueBeforeTarget);

        int randomNumToEnqueue = Random.Shared.Next();
        deque.Enqueue(randomNumToEnqueue);

        for (int i = 0; i < engueueAfterTarget; i++)
        {
            deque.Enqueue(Random.Shared.Next());
        }

        for (int i = 0; i < enqueueBeforeTarget; i++)
        {
            deque.Dequeue();
        }

        Assert.That(deque.Dequeue(), Is.EqualTo(randomNumToEnqueue));
    }

    [Test]
    public void Enqueue_AfterDequeuedToZeroElements_WorksCorrectly()
    {
        int numsToEnqueue = Random.Shared.Next(100);
        ArrayDeque<int> deque = GetRandomFilledDequeQueue(numsToEnqueue);
        for (int i = 0; i < numsToEnqueue; i++)
        {
            deque.Dequeue();
        }

        int numToEnqueueAfterAllDequeued = Random.Shared.Next();
        deque.Enqueue(numToEnqueueAfterAllDequeued);

        Assert.That(deque.Size, Is.EqualTo(1));
        Assert.That(deque.Dequeue(), Is.EqualTo(numToEnqueueAfterAllDequeued));
    }

    [Test]
    public void Peek_WorksCorrectly()
    {
        int firstNumToEnqueue = Random.Shared.Next(100);
        ArrayDeque<int> deque = new ArrayDeque<int>();
        deque.Enqueue(firstNumToEnqueue);

        int numberOfElements = Random.Shared.Next(100);
        for (int i = 0; i < numberOfElements; i++)
        {
            deque.Enqueue(Random.Shared.Next());
        }
        
        int sizePriorToPeek = deque.Size;
        Assert.That(deque.Peek(), Is.EqualTo(firstNumToEnqueue));
        Assert.That(deque.Size, Is.EqualTo(sizePriorToPeek));
    }

    private static ArrayDeque<int> GetRandomFilledDequeQueue(int numberOfElements = 100)
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        for (int i = 0; i < numberOfElements; i++)
        {
            deque.Enqueue(Random.Shared.Next());
        }

        return deque;
    }
}
