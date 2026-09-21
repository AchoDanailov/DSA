namespace DSA.TreeDataStructures.Interfaces;

public interface IBinaryTree<T>
{
    T Value { get; }
    IBinaryTree<T>? Left { get; }
    IBinaryTree<T>? Right { get; }

    string AsIndentedPreOrder(int indent);

    IEnumerable<IBinaryTree<T>> PreOrder();
    IEnumerable<IBinaryTree<T>> InOrder();
    IEnumerable<IBinaryTree<T>> PostOrder();

    void ForEachInOrder(Action<T> action);
}
