using DSA.LinearDataStructures.Interfaces;

namespace DSA.LinearDataStructures.Nodes;

public class DoublyNode<T> : IDoublyNode<T>
{
    public DoublyNode(T value) => this.Value = value;

    public T Value { get; set; }
    public IDoublyNode<T>? Previous { get; set; } 
    public IDoublyNode<T>? Next { get; set; } 
}
