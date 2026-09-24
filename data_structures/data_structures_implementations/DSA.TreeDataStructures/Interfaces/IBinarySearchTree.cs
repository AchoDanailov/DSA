namespace DSA.TreeDataStructures.Interfaces;

public interface IBinarySearchTree<T>
    where T : IComparable<T>
{
    int Size { get; }

    bool Contains(T element);
    IBSTNode<T>? Search(T element);

    void Add(T element);

    IBSTNode<T>? Remove(T element);

    IEnumerable<T> GetAllValueInOrder();
}
