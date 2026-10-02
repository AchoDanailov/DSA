namespace DSA.LinearDataStructures.Interfaces;

interface IDoublyLinkedListQueue<T>
{
    int Size { get; }

    bool Enqueue(T value);
    T Dequeue();
    T Peek();
    bool IsEmpty(); 
    bool Contains(T value);
}
