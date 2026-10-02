namespace DSA.LinearDataStructures.Interfaces;

public interface ISinglyLinkedListStack<T>
{
    int Size { get; }

    bool Push(T value);
    T Pop();
    T Peek();
    bool IsEmpty(); 
    bool Contains(T value);
}