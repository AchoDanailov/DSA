namespace DSA.LinearDataStructures.Interfaces;

public interface IArrayDeque<T> : IEnumerable<T>
{
    T this[int index] { get; set; }
    int Size { get; }

    bool Add(T element);
    bool AddFirst(T element);
    bool Insert(int index, T element);
    
    T FirstOrDefault(T element);
    T RemoveAt(int index);
    T RemoveFirst();
    T RemoveLast();

    bool Contains(T element);
    bool IsEmpty();
    int IndexOf(T element);

    T[] ToArray();
}
