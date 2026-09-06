namespace DSA.LinearDataStructures.Interfaces;

public interface IArrayDeque<T>
{
    T this[int index] { get; set; }
    int Size { get; }
    int Capacity { get; set; }

    bool Add(T element);
    bool Enqueue(T element);
    bool AddFirst(T element);
    bool AddLast(T element);
    bool Push(T element);
    bool Insert(int index, T element);
    
    T Dequeue();
    T Pop();
    T FirstOrDefault(T element);
    T Remove(int index);
    T RemoveFirst();
    T RemoveLast();

    bool Contains(T element);
    bool IsEmpty();
    T Peek();
    int IndexOf(T element);

    T[] ToArray();
}
