using System.Text;
using DSA.TreeDataStructures.Interfaces;

namespace DSA.TreeDataStructures;

public class MaxBinaryHeap<T> : IMaxBinaryHeap<T>
    where T : IComparable<T>
{
    private const int INITIAL_CAPACITY = 8;

    private T[] _innerArr;

    public MaxBinaryHeap() : this(INITIAL_CAPACITY) { }

    public MaxBinaryHeap(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));
        
        this._innerArr = new T[capacity];
    }

    public MaxBinaryHeap(IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));
        
        T[] arr = collection.ToArray();
        this._innerArr = new T[arr.Length];
        Array.ForEach(arr, e => this.Insert(e));
    }
    
    public int Size { get; private set; }
    public int Capacity => this._innerArr.Length;

    public T Peek()
    {
        if (this.Size <= 0)
        {
            throw new InvalidOperationException(
                ExceptionMessages.Heap.HeapIsEmpty);
        }

        return this._innerArr[0];
    }

    public void Insert(T element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        if (this.Size >= this._innerArr.Length)
            this._innerArr = this.Grow();

        this.Size += 1;
        this._innerArr[this.Size - 1] = element;
        this.SiftUpIfNecessary(this.Size - 1);
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= this.Size)
            throw new ArgumentOutOfRangeException(nameof(index));

        T removing = this._innerArr[index];

        // NOTE: if index == this.Size - 1 (last element): no need to reassign
        if (index < this.Size - 1)
            this._innerArr[index] = this._innerArr[this.Size - 1];
        
        this._innerArr[this.Size - 1] = (T)default!;

        this.Size -= 1;

        // NOTE: if index == this.Size: means we removed the element at last position
        // in no case removing the element at the last positon
        // leaves a state that violates the heap property thus no Heapifying is required
        if (index < this.Size)
        {
            this.SiftUpIfNecessary(index);
            this.SiftDownIfNecessary(index);
        }

        if (this.Size < this._innerArr.Length / 2)
            this._innerArr = this.Shrink();

        return removing;
    }

    public override string? ToString()
    {
        if (this.Size == 0)
            return base.ToString();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < this.Size - 1; i++)
        {
            sb.Append(this._innerArr[i].ToString());
            sb.Append(", ");
        }
        sb.Append(this._innerArr[this.Size - 1].ToString());

        return sb.ToString();
    }

    private void SiftUpIfNecessary(int currIndex)
    {
        T element = this._innerArr[currIndex];
        int parentIndex = GetParentIndex(currIndex);
        if (currIndex == 0 || parentIndex == -1)
        {
            return;
        }
        
        while (currIndex > 0 && element.CompareTo(this._innerArr[parentIndex]) > 0)
        {
            if (parentIndex == -1)
                break;

            this._innerArr[currIndex] = this._innerArr[parentIndex];
            this._innerArr[parentIndex] = element;

            currIndex = parentIndex;
            parentIndex = GetParentIndex(currIndex);
        }
    }

    private void SiftDownIfNecessary(int index)
    {
        int biggerChildIndex = this.GetBiggerChildIndex(index);
        if (biggerChildIndex == -1)
        {
            return;
        }

        T currElement = this._innerArr[index];
        T biggerChild = this._innerArr[biggerChildIndex];
        
        while (currElement.CompareTo(biggerChild) < 0)
        {
            this._innerArr[index] = biggerChild;
            this._innerArr[biggerChildIndex] = currElement;

            index = biggerChildIndex;
            biggerChildIndex = this.GetBiggerChildIndex(index);
            if (biggerChildIndex == -1)
            {
                break;
            }

            currElement = this._innerArr[index];
            biggerChild = this._innerArr[biggerChildIndex];
        }
    }

    private int GetBiggerChildIndex(int index)
    {
        int biggerChildIndex; 
        
        int leftIndex = this.GetLeftChildIndex(index);
        int rightIndex = this.GetRightChildIndex(index);
        if (leftIndex == -1 && rightIndex == -1)
        {
            return -1;
        }

        if (leftIndex != -1 && rightIndex != -1)
        {
            T leftChild = this._innerArr[leftIndex];
            T rightChild = this._innerArr[rightIndex];
            biggerChildIndex = leftChild.CompareTo(rightChild) > 0 
                ? leftIndex 
                : rightIndex; 
        }
        else
        {
            if (leftIndex >= 0 && leftIndex < this.Size)
                biggerChildIndex = leftIndex;
            else if (rightIndex >= 0 && rightIndex < this.Size)
                biggerChildIndex = rightIndex;
            else
                biggerChildIndex = -1;
        }

        return biggerChildIndex;
    }

    private T[] Grow()
    {
        T[] temp = new T[this._innerArr.Length * 2];
        for (int i = 0; i < this._innerArr.Length; i++)
            temp[i] = this._innerArr[i];

        return temp;
    }

    private T[] Shrink()
    {
        int newCapacity = Math.Max(this.Size, this._innerArr.Length / 2);
        T[] temp = new T[newCapacity];
        for (int i = 0; i < this.Size; i++)
        {
            temp[i] = this._innerArr[i];
        }

        return temp;
    }

    private int GetParentIndex(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (index == 0)
        {
            // no parent
            return -1;
        }

        // NOTE: Heap property formula for getting parent index.
        return (index - 1) / 2;
    }

    private int GetLeftChildIndex(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        // NOTE: Heap property formula for getting left child index.
        int leftChildIndex = index * 2 + 1;
        if (leftChildIndex >= this.Size)
        {
            return -1;
        }

        return leftChildIndex;
    }

    private int GetRightChildIndex(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        // NOTE: Heap property formula for getting right child index.
        int rightChildIndex = index * 2 + 2;
        if (rightChildIndex >= this.Size)
        {
            return -1;
        }

        return rightChildIndex;
    }
}