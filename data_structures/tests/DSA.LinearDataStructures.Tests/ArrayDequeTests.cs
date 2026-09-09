using DSA.LinearDataStructures.ArrayDeque;
using DSA.LinearDataStructures.Tests.Utils;

namespace DSA.LinearDataStructures.Tests;

[TestFixture]
public class ArrayDequeTests
{
    private ArrayDeque<int> _deque = new ArrayDeque<int>();

    [Test]
    public void Instantiation_WithEmptyCtor_WorksCorrectly()
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        Assert.That(deque.Size, Is.EqualTo(0));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    public void Add_ShouldWorkCorrectly(int numberOfTimesAdding)
    {
        this._deque = new ArrayDeque<int>();
        int[] numbersToAdd = ArrayHelpers.RandomFilledIntArray(numberOfTimesAdding);

        for (int i = 0; i < numbersToAdd.Length; i++)
        {
            _deque.Add(numbersToAdd[i]);
            Assert.That(_deque.Size, Is.EqualTo(i + 1));
        }

        for (int i = 0; i < numbersToAdd.Length; i++)
        {
            Assert.That(_deque.Contains(numbersToAdd[i]));
        }
    }
    
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    public void AddFirst_WorksCorrectly(int numberOfTimesAdding)
    {
        this._deque = new ArrayDeque<int>();
        int[] numbersToAdd = ArrayHelpers.RandomFilledIntArray(numberOfTimesAdding);
        for (int i = 0; i < numberOfTimesAdding; i++)
        {
            _deque.AddFirst(numbersToAdd[i]);
            Assert.That(_deque.Size, Is.EqualTo(i + 1));
        }
        
        for (int i = 0; i < numbersToAdd.Length; i++)
        {
            Assert.That(_deque.Contains(numbersToAdd[i]));
        }
    }
    
    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(10, 11)]
    [TestCase(10, 50)]
    public void Insert_WhenPassedOutOfBoundsIndex_ShouldThrow(int size, int index) 
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size);
        this._deque = GetFilledDeque(size);
        Assert.Throws<IndexOutOfRangeException>(() => _deque.Insert(index, Random.Shared.Next()));
    }
    
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void Insert_WorksCorrectly(int initialSize)
    {
        this._deque = GetFilledDeque(initialSize);
        int[] insertNums = ArrayHelpers.RandomFilledIntArray(
            length: 50,
            randomnessLowerThreshold: 0,
            randomnessUpperThreshold: Math.Max(1, Random.Shared.Next()));
    
        for (int i = 0; i < insertNums.Length; i++)
        {
            this._deque.Insert(
                index: Random.Shared.Next(0, Math.Max(0, initialSize + i - 1)),
                element: insertNums[i]);
            
            Assert.That(_deque.Size, Is.EqualTo(initialSize + i + 1));
        }
    }
    
    [TestCase(-1, 0)]
    [TestCase(-10, 5)]
    [TestCase(10, 5)]
    [TestCase(0, 0)]
    public void RemoveAt_WhenPassedIndexOutOfBounds_ShouldThrow(int index, int size)
    {
        this._deque = GetFilledDeque(size);
        Assert.Throws<IndexOutOfRangeException>(() => _deque.RemoveAt(index));
    }
    
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    public void RemoveAt_WorksCorrectly(int initialSize)
    {
        this._deque = GetFilledDeque(initialSize);
    
        int numberOfRemoves = Random.Shared.Next(1, initialSize);
        for (int i = 0; i < numberOfRemoves; i++)
        {
            int indexToRemoveAt = Random.Shared.Next(0, initialSize - 1 - i);
            _deque.RemoveAt(indexToRemoveAt);
    
            Assert.That(_deque.Size, Is.EqualTo(initialSize - 1 - i));
        }
    }
    
    [Test]
    public void RemoveFirst_WhenDequeEmpty_Throws()
    {
        this._deque = new ArrayDeque<int>();
        Assert.Throws<InvalidOperationException>(() => _deque.RemoveFirst());
    }
    
    [Test]
    public void RemoveFirst_WorksCorrectly()
    {
        int numOfAdditions = Random.Shared.Next(maxValue: 100);
        this._deque = GetFilledDeque(numOfAdditions);
    
        int randomNumToAddFirst = Random.Shared.Next();
        _deque.AddFirst(randomNumToAddFirst);
    
        Assert.That(randomNumToAddFirst, Is.EqualTo(_deque.RemoveFirst()));
        Assert.That(_deque.Size, Is.EqualTo(numOfAdditions));
    }
    
    [Test]
    public void RemoveLast_WhenEmpty_ShouldThrow()
    {
        this._deque = new ArrayDeque<int>();
        Assert.That(_deque.Size, Is.EqualTo(0));
        Assert.Throws<InvalidOperationException>(() => _deque.RemoveLast());
    }
    
    [Test]
    public void RemoveLast_WhenDequeFilledWithAddFirst_ShouldWorkCorrectly()
    {
        int numOfAdditions = Random.Shared.Next(maxValue: 100);
        this._deque = GetFilledDeque(numOfAdditions);
    
        int randomNumAddLast = Random.Shared.Next();
        this._deque.Add(randomNumAddLast);
    
        Assert.That(randomNumAddLast, Is.EqualTo(this._deque.RemoveLast()));
        Assert.That(this._deque.Size, Is.EqualTo(numOfAdditions));
    }
    
    [Test]
    public void IndexOf_WhenElementNotFound_ReturnsMinusOne()
    {
        this._deque = GetFilledDeque(100);
        Assert.That(
            this._deque.IndexOf(Random.Shared.Next(51, int.MaxValue)), Is.EqualTo(-1));
    }
    
    [Test]
    public void IndexOf_WorksCorrectly()
    {
        int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        this._deque = new ArrayDeque<int>();
        for (int i = 0; i < arr.Length; i++)
        {
            this._deque.Add(arr[i]);
        }
    
        Assert.That(_deque.IndexOf(0), Is.EqualTo(0));
        Assert.That(_deque.IndexOf(5), Is.EqualTo(5));
        Assert.That(_deque.IndexOf(9), Is.EqualTo(9));
    }
    
    [Test]
    public void Contains_WhenElementNotFound_ReturnsFalse()
    {
        this._deque = GetFilledDeque(50);
        Assert.That(
            this._deque.Contains(Random.Shared.Next(51, int.MaxValue)), Is.False);
    }
    
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(50)]
    public void Contains_WorksCorrectly(int size)
    {
        this._deque = GetFilledDeque(size);
    
        int numToInsert = Random.Shared.Next();
        this._deque.Insert(Random.Shared.Next(0, size), numToInsert);
    
        Assert.That(_deque.Contains(numToInsert), Is.True);
    }
    
    [Test]
    public void IsEmpty_WhenNotEmpty_ReturnsFalse()
    {
        this._deque = GetFilledDeque();
        Assert.That(_deque.IsEmpty(), Is.False);
    }
    
    [Test]
    public void IsEmpty_WhenEmpty_ReturnsTrue()
    {
        this._deque = new ArrayDeque<int>();
        Assert.That(_deque.IsEmpty(), Is.True);
    }
    
    [Test]
    public void Contains_WhenElementIsntContained_ReturnsFalse()
    {
        this._deque = new ArrayDeque<int>();
        Assert.That(_deque.Contains(Random.Shared.Next()), Is.False);
    }

    [TestCase(1)]
    [TestCase(5)]
    [TestCase(50)]
    public void ToArray_WorksCorrectly(int commonSize)
    {
        this._deque = GetFilledDeque(commonSize);
        int[] arr = this._deque.ToArray<int>();

        for (int i = 0; i < arr.Length; i++)
        {
            Assert.That(arr.Contains(this._deque[i]), Is.True);
        }
    }

    private static ArrayDeque<int> GetFilledDeque(
        int numOfAdditions = 100,
        bool addFromFront = false)
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        for (int i = 0; i < numOfAdditions; i++)
        {
            if (addFromFront == true) deque.AddFirst(Random.Shared.Next());
            else deque.Add(Random.Shared.Next());
        }
        return deque;
    }
}