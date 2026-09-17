using DSA.BasicTreeDataStructure.Interfaces;

namespace DSA.BasicTreeDataStructure;

public class BasicTree<T> : IBasicTree<T>
{
    private BasicTree<T>? _parent;
    private readonly ICollection<BasicTree<T>> _children;

    public BasicTree()
    {
        this._children = new List<BasicTree<T>>();
    }

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

    public T? Value { get; private set; }

    public IEnumerable<T> GetOrderBFS()
    {
        ICollection<T> orderBfs = new List<T>();

        Queue<BasicTree<T>> queue = new Queue<BasicTree<T>>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            BasicTree<T> current = queue.Dequeue();
            if (current.Value == null)
                throw new InvalidOperationException(ExceptionMessages.TreeNodeCanNotBeEmpty);
            
            orderBfs.Add(current.Value);

            foreach (BasicTree<T> child in current._children)
                queue.Enqueue(child);
        }
        
        return orderBfs;
    }

    public IEnumerable<T> GetOrderDFS()
    {
        ICollection<T> orderDfs = new List<T>();
        DoDfs(this, orderDfs);
        return orderDfs;
    }

    // O(n)
    public bool Contains(T targetValue)
    {
        Queue<BasicTree<T>> queue = new Queue<BasicTree<T>>();
        queue.Enqueue(this);
        
        while (queue.Count > 0)
        {
            BasicTree<T> current = queue.Dequeue();
            if (current.Value == null)
                throw new InvalidOperationException(ExceptionMessages.TreeNodeCanNotBeEmpty);
            
            bool targetNodeFound = current.Value!.Equals(targetValue);
            if (targetNodeFound)
                return true;
            
            foreach (BasicTree<T> child in current._children)
                queue.Enqueue(child);
        }

        return false;
    }
   
    // O(n)
    public bool AddChild(IBasicTree<T> newTree, T targetValue)
    {
        Queue<BasicTree<T>> queue = new Queue<BasicTree<T>>();
        queue.Enqueue(this);
        
        while (queue.Count > 0)
        {
            BasicTree<T> current = queue.Dequeue();
            if (current.Value == null)
                throw new InvalidOperationException(ExceptionMessages.TreeNodeCanNotBeEmpty);
            
            bool targetNodeFound = current.Value.Equals(targetValue);
            if (targetNodeFound)
            {
                BasicTree<T> newTreeAsImpl = (newTree as BasicTree<T>)!;
                current._children.Add(newTreeAsImpl);
                newTreeAsImpl._parent = current;
                return true;
            }
            
            foreach (BasicTree<T> child in current._children)
                queue.Enqueue(child);
        }

        return false;
    }

    // O(n)
    public IBasicTree<T> Remove(T targetValue)
    {
        IBasicTree<T> result = FindAndRemoveTargetDfs(this, targetValue);
        return result;
    }

    private static IBasicTree<T> FindAndRemoveTargetDfs(BasicTree<T> current, T targetValue)
    {
        if (current.Value == null)
            throw new InvalidOperationException(ExceptionMessages.TreeNodeCanNotBeEmpty);
        
        if (current.Value.Equals(targetValue))
        {
            if (current._parent != null)
            {
                current._parent._children.Remove(current);
                current._parent = null;
            }
            else 
            {
                current.Value = (T)default!;
                current._children.Clear();
            }
            
            return current;
        }

        foreach (BasicTree<T> child in current._children)
        {
            IBasicTree<T> result = FindAndRemoveTargetDfs(child, targetValue);
            if (result.Value!.Equals(targetValue))
            {
                return result;
            }
        }

        return new BasicTree<T>();
    }

    private static void DoDfs(BasicTree<T> tree, ICollection<T> result)
    {
        if (tree.Value == null)
            throw new InvalidOperationException(ExceptionMessages.TreeNodeCanNotBeEmpty);
        
        result.Add(tree.Value);
        
        foreach(BasicTree<T> child in tree._children)
            DoDfs(child, result);
    }
}