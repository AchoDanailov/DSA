using DSA.LinearDataStructures.Interfaces;

namespace DSA.LinearDataStructures.ArrayDeque;

public class ArrayDeque<T> : IArrayDeque<T>
{
    public ArrayDeque() { }
    public ArrayDeque(IEnumerable<T> collection) { }

    public T this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public int Size { get; private set; }

    public bool Add(T element)
    {
        throw new NotImplementedException();
    }

    public bool AddFirst(T element)
    {
        throw new NotImplementedException();
    }

    public bool Insert(int index, T element)
    {
        throw new NotImplementedException();
    }

    public T FirstOrDefault(T element)
    {
        throw new NotImplementedException();
    }

    public T RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    public T RemoveFirst()
    {
        throw new NotImplementedException();
    }

    public T RemoveLast()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T element)
    {
        throw new NotImplementedException();
    }

    public bool IsEmpty()
    {
        throw new NotImplementedException();
    }
    
    public int IndexOf(T element) 
    {
        throw new NotImplementedException();
    }

    public T[] ToArray()
    {
        throw new NotImplementedException();
    }
}
