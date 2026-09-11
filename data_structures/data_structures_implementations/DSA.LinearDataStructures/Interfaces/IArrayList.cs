namespace DSA.LinearDataStructures.Interfaces;

public interface IArrayList<T> : IEnumerable<T>
{
    int Size { get; }
    int Capacity { get; set; }
    T this[int index] { get; set; }

    bool Add(T element);
    bool Insert(int index, T element);
    T Remove(int index);
    int IndexOf(T element);
    bool Contains(T element);
    bool IsEmpty();
}
