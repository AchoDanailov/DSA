using DSA.TreeDataStructures.Tests.Utils;

namespace DSA.TreeDataStructures.Tests;

[TestFixture]
public class MaxBinaryHeapTests
{
    private MaxBinaryHeap<int> _maxHeap; 

    [SetUp]
    public void SetUp()
    {
        int[] elements = new int[] { 15, 25, 6, 9, 5, 8, 17, 16 };
        this._maxHeap = new MaxBinaryHeap<int>(elements);
    }

    [Test]
    public void MaxHeapProperty_IsEnsured()
    {
        string heapAsStr = this._maxHeap.ToString()!;
        int[] heapEls = heapAsStr
            .Split(", ")
            .Select(e => int.Parse(e))
            .ToArray();

        // 25 16 17 15 5 6 8 9
        Assert.That(heapEls, 
            Is.EqualTo(new int[] { 25, 16, 17, 15, 5, 6, 8, 9 }));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(20)]
    public void Ctor_WithCapacity_DoesNotBreakFunctionality(int capacity)
    {
        this._maxHeap = new MaxBinaryHeap<int>(capacity);
        Assert.That(this._maxHeap.Capacity, Is.EqualTo(capacity));
        
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: capacity * 5);       
        Array.ForEach(arr, e => this._maxHeap.Insert(e));
        
        int[] res = new int[capacity * 5];
        for (int i = res.Length - 1; i >= 0; i--)
            res[i] = this._maxHeap.RemoveAt(this._maxHeap.Size - 1);
        
        Assert.That(res.All(e => arr.Contains(e)), Is.True);
        Assert.That(this._maxHeap.Size, Is.EqualTo(0));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(20)]
    public void Ctor_WithCollection_DoesNotBreakFunctionality(int size)
    {
        int[] arr = ArrayHelpers.RandomFilledIntArray(size);
        this._maxHeap = new MaxBinaryHeap<int>(arr);
        Assert.That(this._maxHeap.Size, Is.EqualTo(size));
        
        int[] res = new int[arr.Length];
        for (int i = 0; i < arr.Length; i++)
            res[i] = this._maxHeap.RemoveAt(index: 0);
        
        Assert.That(res.All(e => arr.Contains(e)), Is.True);
        Assert.That(this._maxHeap.Size, Is.EqualTo(0));
    }

    [Test]
    public void RemoveAt_WorksCorrectly()
    {
        int[] arr = new int[] { 8, 10, 23, 40, 2, 24, 1 };
        MaxBinaryHeap<int> heap = new MaxBinaryHeap<int>();
        
        Array.ForEach(arr, e => heap.Insert(e));

        int[] expectedAfterInsert = new int[] { 40, 23, 24, 8, 2, 10, 1 };
        int[] actualAfterInsert = heap.ToString()!
            .Split(", ")
            .Select(e => int.Parse(e))
            .ToArray();
        Assert.That(expectedAfterInsert, Is.EquivalentTo(actualAfterInsert));

        heap.RemoveAt(index: 1);

        // 40, 23, 24, 8, 2, 10, 1 => 40, 1, 24, 8, 2, 10 => 40, 8, 24, 1, 2, 10
        int[] expectedAfterRemove = new int[] { 40, 8, 24, 1, 2, 10 };
        int[] actualAfterRemove = heap.ToString()!
            .Split(", ")
            .Select(e => int.Parse(e))
            .ToArray();
        Assert.That(expectedAfterRemove, Is.EquivalentTo(actualAfterRemove));
    }

    [Test]
    public void Insert_AfterAllElementsRemoved_WorksCorrectly()
    {
        int initSize = this._maxHeap.Size;
        for (int i = 0; i < initSize; i++)
            this._maxHeap.RemoveAt(this._maxHeap.Size - 1);
        
        Assert.That(this._maxHeap.Size, Is.EqualTo(0));

        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: Random.Shared.Next(10, 100),
            isReacuranceAllowed: false);
        Array.ForEach(arr, e => this._maxHeap.Insert(e));

        int[] res = this._maxHeap.ToString()!.Split(", ").Select(e => int.Parse(e)).ToArray();
        Assert.That(this._maxHeap.Size, Is.EqualTo(arr.Length));
        Assert.That(res.All(e => arr.Contains(e)), Is.True);
    }
    
    [Test]
    public void Insert_SingleElement_BecomesPeek()
    {
        MaxBinaryHeap<int> heap = new MaxBinaryHeap<int>();
        heap.Insert(13);

        Assert.That(heap.Peek(), Is.EqualTo(13));
    }

    [Test]
    public void Insert_ManyElements_LargestIsPeek()
    {
        Assert.That(this._maxHeap.Peek(), Is.EqualTo(25));
    }

    [Test]
    public void Size_AfterInserts_IsCorrect()
    {
        Assert.That(this._maxHeap.Size, Is.EqualTo(8));
    }

}