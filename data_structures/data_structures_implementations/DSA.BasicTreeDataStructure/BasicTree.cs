using DSA.BasicTreeDataStructure.Interfaces;

namespace DSA.BasicTreeDataStructure;

public class BasicTree<T> : IBasicTree<T>
{
    private BasicTree<T>? _parent;
    private readonly ICollection<BasicTree<T>> _children;

    public BasicTree(T value, params BasicTree<T>[] children)
    {
        this.Value = value;
        this._children = new List<BasicTree<T>>();

        foreach (BasicTree<T> tree in children)
        {
            this._children.Add(tree);
            tree._parent = this;
        }
    }

    public T Value { get; }

    public void BFS()
    {
        Queue<BasicTree<T>> queue = new Queue<BasicTree<T>>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            BasicTree<T> current = queue.Dequeue();

            // operations can be done here
            Console.WriteLine(current.Value);

            foreach (BasicTree<T> child in current._children)
                queue.Enqueue(child);
        }
    }

    public void DFS()
    {
        this.DoDFS(this);
    }

    private void DoDFS(BasicTree<T> tree)
    {
        foreach(BasicTree<T> child in tree._children)
        {
            this.DoDFS(child);

            // operations can be done here
            Console.WriteLine(child.Value);
        }
    }
}
