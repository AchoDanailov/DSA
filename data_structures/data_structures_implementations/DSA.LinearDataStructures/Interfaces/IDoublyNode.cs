namespace DSA.LinearDataStructures.Interfaces;

public interface IDoublyNode<T> 
{
    T Value { get; set; }
    IDoublyNode<T>? Previous { get; set; }
    IDoublyNode<T>? Next { get; set; }
}
