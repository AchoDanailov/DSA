namespace DSA.LinearDataStructures.Interfaces;

public interface ISinglyListStack<T>
{
    bool Push(T value);
    T Pop();
    T Peek();
    bool IsEmpty(); 
    bool Contains(T value);
}