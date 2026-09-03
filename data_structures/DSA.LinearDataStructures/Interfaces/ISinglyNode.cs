namespace DSA.LinearDataStructures.Interfaces;

public interface ISinglyNode<T>
{
    T Value { get; set; }
    ISinglyNode<T>? Next { get; set; }
}