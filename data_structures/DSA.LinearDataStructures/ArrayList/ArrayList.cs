using DSA.LinearDataStructures.Enums;
using DSA.LinearDataStructures.Interfaces;

namespace DSA.LinearDataStructures.ArrayList;

public class ArrayList<T> : IArrayList<T>
{
    private const int DEFAULT_CAPACITY = 8;

    private T[] _innerArr;
    private int _size;

    public ArrayList() : this(DEFAULT_CAPACITY) { }

    public ArrayList(int capacity)
    {
        // opt1:
        if (capacity < this.Size)
            throw new IndexOutOfRangeException(nameof(this.Capacity));
        
        // opt2:
        // this.Capacity = capacity;
        
        this._innerArr = new T[capacity];
    }

    public ArrayList(IEnumerable<T> collection)
    {
        this._innerArr = collection.ToArray();
        this.Size = this._innerArr.Length;

        if (this.Size > DEFAULT_CAPACITY) 
            this._innerArr = this.Grow();
        else 
            this.Capacity = DEFAULT_CAPACITY;
    }

    public int Size 
    { 
        get => this._size;
        private set 
        { 
            if (value < 0 || value > this._innerArr.Length)
                throw new IndexOutOfRangeException(nameof(this.Size));

            this._size = value; 
        }
    }
	
    public int Capacity 
    {
        get => this._innerArr.Length;
        set 
        { 
            if (value < this.Size)
                throw new IndexOutOfRangeException(nameof(this.Capacity));

            T[] temp = new T[value];
            for (int i = 0; i < this.Size; i++)
            {
                temp[i] = this._innerArr[i];
            }
			
            this._innerArr = temp;
        }
    }
	
    public T this[int index] 
    { 
        get 
        {
            if (index < 0 || index >= this.Size) 
                throw new IndexOutOfRangeException(nameof(index));

            return this._innerArr[index];
        }
        set 
        {
            if (index < 0 || index > this.Size)
                throw new IndexOutOfRangeException(nameof(index));

            this._innerArr[index] = value;
        }
    }

    // O(1) amortized
    public bool Add(T element)
    {
        if (this.IsGrowthNeeded()) 
            this._innerArr = this.Grow();
        
        this._innerArr[this.Size] = element;
        this.Size += 1;
		
        return true;
    }

    // O(n)
    public bool Insert(int index, T element)
    {
        if (index < 0 || index > this.Size) 
            throw new IndexOutOfRangeException(nameof(index));

        if (index == this.Size)
            return this.Add(element);

        if (this.IsGrowthNeeded()) 
            this._innerArr = this.Grow();
        
        this._innerArr = this.ShiftElements(index, Direction.Right);
        this._innerArr[index] = element;
        this.Size += 1;
        return true;
    }

    // O(n)
    public T Remove(int index)
    {
        if (index < 0 || index >= this.Size) 
            throw new IndexOutOfRangeException(nameof(index));

        T elementToRemove = this._innerArr[index];
        this._innerArr = this.ShiftElements(index, Direction.Left);
        this.Size -= 1;

        if (this.IsShrinkNeeded())
            this._innerArr = this.Shrink();
		
        return elementToRemove;
    }

    // O(n)
    public int IndexOf(T element)
    {
        for (int i = 0; i < this.Size; i++) 
        {
            if (this._innerArr[i]!.Equals(element))
                return i;
        }

        return -1;
    }

    // O(n)
    public bool Contains(T element)
    {
        for (int i = 0; i < this.Size; i++) 
        {
            if (this._innerArr[i]!.Equals(element))
                return true;
        }

        return false;
    }

    // O(1)
    public bool IsEmpty()
    {
        if (this.Size > 0)
            return false;

        return true;
    }

    private bool IsGrowthNeeded() 
    {
        if (this.Size >= 0.66 * this.Capacity)
            return true;

        return false;
    }

    private T[] Grow(int? capacity = null)
    {
        T[] temp;
        if (capacity != null && capacity > this.Capacity) 
        {
            temp = new T[capacity.Value];
        }
        else
        {
            temp = new T[this.Capacity * 2];
        }

        this.Capacity = temp.Length;

        for (int i = 0; i < this.Size; i++)
        {
            temp[i] = this._innerArr[i];
        }

        return temp;
    }

    private T[] Shrink()
    {
        int newCapacity = (int)(0.66 * this.Capacity);
        T[] temp = new T[newCapacity];

        for (int i = 0; i < newCapacity; i++)
        {
            temp[i] = this._innerArr[i];
        }

        return temp;
    }

    private bool IsShrinkNeeded()
    {
        if (this.Size < (33.3 * this.Capacity) / 100)
            return true;

        return false;
    }
	
    private T[] ShiftElements(int index, Direction direction)
    {
        T[] temp = new T[this.Capacity];
        if (direction == Direction.Left)
        {
            for (int i = 0; i < this.Size; i++) 
            {
                if (i < index)
                {
                    temp[i] = this._innerArr[i];
                }
                else
                {
                    temp[i] = this._innerArr[i + 1];
                }
            }
        }
        else 
        {
            for (int i = 0; i < this.Size; i++) 
            {
                if (i < index)
                {
                    temp[i] = this._innerArr[i];
                }
                else
                {
                    temp[i + 1] = this._innerArr[i];
                }
            }
        }

        return temp;
    }
}