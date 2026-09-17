namespace DSA.BasicTreeDataStructure.Interfaces;

public interface IBasicTree<T>
{
    T? Value { get; }

    IEnumerable<T> GetOrderBFS();
    IEnumerable<T> GetOrderDFS();

    bool Contains(T targetValue);

    bool AddChild(IBasicTree<T> newTree, T targetValue);
    IBasicTree<T> Remove(T targetValue);
}
