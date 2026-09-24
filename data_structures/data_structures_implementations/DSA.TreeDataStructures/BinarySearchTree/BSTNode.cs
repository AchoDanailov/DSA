using DSA.TreeDataStructures.Interfaces;

namespace DSA.TreeDataStructures.BinarySearchTree;

public class BSTNode<T> : IBSTNode<T>
    where T : IComparable<T>
{
    public BSTNode(T value)
    {
        this.Value = value;
    }

    public IBSTNode<T>? Parent { get; set; }
    public T? Value { get; set; }
    public IBSTNode<T>? LeftChild { get; set; }
    public IBSTNode<T>? RightChild { get; set; }
}
