namespace DSA.BasicTreeDataStructure.Interfaces;

public interface IBasicTree<T>
{
    T Value { get; }
    void BFS();
    void DFS();
}
