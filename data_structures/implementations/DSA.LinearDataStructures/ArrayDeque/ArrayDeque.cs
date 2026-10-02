using System.Collections;
using DSA.LinearDataStructures.Enums;
using DSA.LinearDataStructures.Interfaces;

namespace DSA.LinearDataStructures.ArrayDeque;

public class ArrayDeque<T> : IArrayDeque<T>
{
    private const int INITIAL_CAPACITY = 7;

    private T[] _innerArr;
    private int _head;
    private int _tail;

    public ArrayDeque() 
    {
        this._innerArr = new T[INITIAL_CAPACITY];
        this._head = this._innerArr.Length / 2;
        this._tail = this._head;
        this.Size = 0;
    }

    public T this[int index]
    {
        get 
        {
            if (this._head + index < this._head || this._head + index > this._tail)
                    throw new IndexOutOfRangeException(nameof(index));

            return this._innerArr[this._head + index];
        }
        set 
        {
            if (this._head + index < this._head || this._head + index > this._tail)
                    throw new IndexOutOfRangeException(nameof(index));

            this._innerArr[this._head + index] = value;
        }
    }

    public int Size { get; private set; }

    public bool Add(T element)
    {
        if (this.Size == 0)
        {
            this._innerArr[this._tail] = element;
        }
        else
        {
            if (this._tail == this._innerArr.Length - 1)
            {
                (this._innerArr, this._head, this._tail) 
                    = this.Resize(ResizeDirection.Up);
            }

            this._tail += 1;
            this._innerArr[this._tail] = element;
        }

        this.Size += 1;
        return true;
    }

    public bool AddFirst(T element)
    {
        if (this.Size == 0)
        {
            this._innerArr[this._head] = element;
        }
        else
        {
            if (this._head == 0)
            {
                (this._innerArr, this._head, this._tail) 
                    = this.Resize(ResizeDirection.Up);
            }

            this._head -= 1;
            this._innerArr[this._head] = element;
        }

        this.Size += 1;
        return true;
    }

    public bool Insert(int index, T element)
    {
        if (index < 0 || index > this.Size)
            throw new IndexOutOfRangeException(nameof(index));

        if (index == this.Size || this.Size == 0)
            return this.Add(element);

        if (this._tail == this._innerArr.Length - 1)
        {
            (this._innerArr, this._head, this._tail) 
                = this.Resize(ResizeDirection.Up);
        }

        this._innerArr = this.ShiftElements(index, ShiftDirection.Right);
        
        this._innerArr[this._head + index] = element;
        this._tail += 1;
        this.Size += 1;
        
        return true;
    }

    public T FirstOrDefault(T element)
    {
        for (int i = this._head; i <= this._tail; i++)
        {
            if (this._innerArr[i]!.Equals(element))
                return this._innerArr[i];
        }

        return (T)default!;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= this.Size)
            throw new IndexOutOfRangeException(nameof(index));

        if (this._tail == this._head)
            return this.RemoveLast();

        T removed = this._innerArr[this._head + index];
        this._innerArr[this._head + index] = (T)default!;
        this._innerArr = this.ShiftElements(index, ShiftDirection.Left);

        this._tail -= 1;
        this.Size -= 1;

        if (this.Size <= this._innerArr.Length / 2)
        {
            (this._innerArr, this._head, this._tail)
                = this.Resize(ResizeDirection.Down);
        }

        return removed;
    }

    public T RemoveFirst()
    {
        if (this.Size == 0)
            throw new InvalidOperationException("Deque is empty!");

        T removed = this._innerArr[this._head];
        this._innerArr[this._head] = (T)default!;
        
        if (this._head != this._tail)
            this._head += 1;

        this.Size -= 1;
        return removed;
    }

    public T RemoveLast()
    {
        if (this.Size == 0)
            throw new InvalidOperationException("Deque is empty!");

        T removed = this._innerArr[this._tail];
        this._innerArr[this._tail] = (T)default!;

        if (this._tail != this._head)
            this._tail -= 1;

        this.Size -= 1;
        return removed;
    }

    public bool Contains(T element)
    {
        for (int i = this._head; i <= this._tail; i++)
        {
            if (this._innerArr[i]!.Equals(element))
                return true;
        }

        return false;
    }

    public bool IsEmpty()
    {
        if (this.Size == 0)
            return true;

        return false;
    }
    
    public int IndexOf(T element) 
    {
        for (int i = 0; i < this.Size; i++)
        {
            if (this._innerArr[this._head + i]!.Equals(element))
                return i;
        }
        
        return -1;
    }

    public T[] ToArray()
    {
        T[] temp = new T[this.Size];
        for (int i = 0; i < this.Size; i++)
        {
            temp[i] = this._innerArr[i];
        }

        return temp;
    }

    private ValueTuple<T[], int, int> Resize(ResizeDirection resizeDirection)
    {
        T[] temp;
        if (resizeDirection == ResizeDirection.Up)
        {
            temp = new T[this._innerArr.Length * 2 + 1];
        }
        else 
        {
            temp = new T[this._innerArr.Length / 2 + 1];
        }

        int center = temp.Length / 2;
        int newHead = center - this.Size / 2;
        int newTail = newHead + this.Size - 1;

        int index = 0;
        for (int i = this._head; i <= this._tail; i++)
        {
            temp[newHead + index] = this._innerArr[i];
            index += 1;
        }

        return (temp, newHead, newTail);
    }

    private T[] ShiftElements(int index, ShiftDirection shiftDirection)
    {
        T[] temp = new T[this._innerArr.Length];

        for (int i = 0; i < this.Size; i++) 
        {
            if (i < index)
            {
                temp[this._head + i] 
                    = this._innerArr[this._head + i];
            }
            else 
            {
                if (shiftDirection == ShiftDirection.Right)
                {
                    temp[this._head + i + 1] 
                        = this._innerArr[this._head + i];
                }
                else 
                {
                    if (this._head + i == this._innerArr.Length - 1)
                    {
                        break;
                    }

                    temp[this._head + i]
                        = this._innerArr[this._head + i + 1];
                }
            }
        }

        return temp;
    }

    public IEnumerator<T> GetEnumerator() 
        => new ArrayDeque<T>.DefaultEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    private class DefaultEnumerator : IEnumerator<T>
    {
        private ArrayDeque<T> _collection;
        private int _index;
        
        public DefaultEnumerator(ArrayDeque<T> collection)
        {
            this._index = -1;
            this._collection = collection;
        }

        public bool HasNext => this._index + 1 < this._collection.Size;
        public T Current => this._collection[this._index];
        object? IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            if (!this.HasNext)
                return false;
                
            this._index += 1;
            return true;
        }

        public void Reset()
        {
            this._index = -1;
        }
        
        public void Dispose()
        {
            this.Reset();
            this._collection = null!;
        }
    }
}
