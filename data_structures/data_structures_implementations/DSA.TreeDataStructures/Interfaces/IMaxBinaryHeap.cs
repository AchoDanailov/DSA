namespace DSA.TreeDataStructures.Interfaces;

public interface IMaxBinaryHeap<T>
    where T : IComparable<T>
{
    int Size { get; }
    int Capacity { get; }

    T Peek();
    void Insert(T element);
    T RemoveAt(int index);
}
