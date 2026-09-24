namespace DSA.TreeDataStructures.Interfaces;

public interface IBSTNode<T>
    where T : IComparable<T>
{
    IBSTNode<T>? Parent { get; set; }
    T? Value { get; set; }
    IBSTNode<T>? LeftChild { get; set; }
    IBSTNode<T>? RightChild { get; set; }
}
