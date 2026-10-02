using DSA.LinearDataStructures.Interfaces;

namespace DSA.LinearDataStructures.Nodes;

public class SinglyNode<T> : ISinglyNode<T>
{
    public SinglyNode(T value) => this.Value = value;

    public T Value { get; set; }
    public ISinglyNode<T>? Next { get; set; }

    public override string ToString() 
        => this.Value!.ToString()!;
}