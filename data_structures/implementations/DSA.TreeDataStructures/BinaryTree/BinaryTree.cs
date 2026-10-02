using System.Text;

using DSA.TreeDataStructures.Common;
using DSA.TreeDataStructures.Interfaces;

namespace DSA.TreeDataStructures;

public class BinaryTree<T> : IBinaryTree<T>
{
    private BinaryTree<T>? _parent;

    public BinaryTree(
        T value,
        BinaryTree<T>? left = null,
        BinaryTree<T>? right = null)
    {
        this.Value = value;
        this.Left = left;
        this.Right = right;

        (this.Left as BinaryTree<T>)?._parent = this;
        (this.Right as BinaryTree<T>)?._parent = this;
    }

    public T Value { get; }
    public IBinaryTree<T>? Left { get; }
    public IBinaryTree<T>? Right { get; }

    public string AsIndentedPreOrder(int indent)
    {
        if (indent < 0)
        {
            throw new ArgumentException(
                ExceptionMessages.Common.ValueCanNotBeLessThanZero,
                nameof(indent));
        }
        
        StringBuilder resultSb = new StringBuilder();
        TraverseAndBuildAStringPreOrder(this, indent, resultSb);
        return resultSb.ToString().TrimEnd();
    }

    public IEnumerable<IBinaryTree<T>> PreOrder()
    {
        ICollection<IBinaryTree<T>> resultList 
            = new List<IBinaryTree<T>>();

        OrderPreOrder(this, resultList);

        return resultList;
    }

    public IEnumerable<IBinaryTree<T>> InOrder()
    {
        ICollection<IBinaryTree<T>> resultList 
            = new List<IBinaryTree<T>>();

        OrderInOrder(this, resultList);

        return resultList;
    }

    public IEnumerable<IBinaryTree<T>> PostOrder()
    {
        ICollection<IBinaryTree<T>> resultList 
            = new List<IBinaryTree<T>>();

        OrderPostOrder(this, resultList);

        return resultList;
    }

    public void ForEachInOrder(Action<T> action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        TraverseAndExecuteActionInOrder(this, action);
    }

    public IBinaryTree<T> FindLowestCommonAncestor(T target1, T target2)
    {
        List<IBinaryTree<T>> node1Path = new List<IBinaryTree<T>>();
        List<IBinaryTree<T>> node2Path = new List<IBinaryTree<T>>();

        IBinaryTree<T>? node1 = Find(this, target1, node1Path);
        if (node1 == null)
        {
            throw new ArgumentException(string.Format(
                ExceptionMessages.BinaryTree.TargetNotFound, target1));
        }
        if (node1.Equals(this))
        {
            throw new ArgumentException(
                ExceptionMessages.BinaryTree.RootDoesNotHaveAncestors);
        }

        IBinaryTree<T>? node2 = Find(this, target2, node2Path);
        if (node2 == null)
        {
            throw new ArgumentException(string.Format(
                ExceptionMessages.BinaryTree.TargetNotFound, target2));
        }
        if (node2.Equals(this))
        {
            throw new ArgumentException(
                ExceptionMessages.BinaryTree.RootDoesNotHaveAncestors);
        }

        if (node1.Equals(node2))
            return (node1 as BinaryTree<T>)!._parent!;

        IBinaryTree<T> res = FindLowestCommonAncestor(node1Path, node2Path);
        return res;
    }

    private static IBinaryTree<T>? Find(
        IBinaryTree<T> currNode,
        T target,
        List<IBinaryTree<T>> path)
    {
        IBinaryTree<T>? res = null;
        if (currNode.Value!.Equals(target))
            return currNode;
        
        if (currNode.Left != null)
        {
            res = Find(currNode.Left, target, path);
            if (res != null && res.Value!.Equals(target))
            {
                path.Add(currNode);
                return res;
            }
        }

        if (path.Count > 0)
            path.RemoveAt(path.Count - 1);

        if (currNode.Right != null)
        {
            res = Find(currNode.Right, target, path);
            if (res != null && res.Value!.Equals(target))
            {
                path.Add(currNode);
                return res;
            }
        }
        
        if (path.Count > 0)
            path.RemoveAt(path.Count - 1);
        
        return res;
    }

    private static IBinaryTree<T> FindLowestCommonAncestor(
        ICollection<IBinaryTree<T>> node1Path,
        ICollection<IBinaryTree<T>> node2Path)
    {
        node1Path = node1Path.Reverse().ToArray();
        node2Path = node2Path.Reverse().ToArray();
        
        int smallerOrEqualPathLength = Math.Min(node1Path.Count, node2Path.Count);
        
        int i = 0;
        for ( ; i < smallerOrEqualPathLength; i++)
        {
            if (node1Path.ElementAt(i).Equals(node2Path.ElementAt(i)) == false)
                break;
        }

        return node1Path.ElementAt(i - 1);
    }

    private static void TraverseAndExecuteActionInOrder(
        IBinaryTree<T> currNode,
        Action<T> action)
    {
        if (currNode.Left != null)
            TraverseAndExecuteActionInOrder(currNode.Left, action);

        action(currNode.Value);

        if (currNode.Right != null)
            TraverseAndExecuteActionInOrder(currNode.Right, action);
    }

    private static void TraverseAndBuildAStringPreOrder(
        IBinaryTree<T> currNode,
        int indent,
        StringBuilder resultSb)
    {
        resultSb.Append(' ', indent);
        resultSb.AppendLine(currNode.Value!.ToString());

        if (currNode.Left != null)
        {
            TraverseAndBuildAStringPreOrder(
                currNode.Left,
                indent + 2,
                resultSb);
        }

        if (currNode.Right != null)
        {
            TraverseAndBuildAStringPreOrder(
                currNode.Right,
                indent + 2,
                resultSb);
        }
    }

    private void OrderPreOrder(
        IBinaryTree<T> currNode,
        ICollection<IBinaryTree<T>> resultList)
    {
        resultList.Add(currNode);

        if (currNode.Left != null)
            OrderPreOrder(currNode.Left, resultList);

        if (currNode.Right != null)
            OrderPreOrder(currNode.Right, resultList);
    }

    private void OrderInOrder(
        IBinaryTree<T> currNode,
        ICollection<IBinaryTree<T>> resultList)
    {
        if (currNode.Left != null)
            OrderInOrder(currNode.Left, resultList);

        resultList.Add(currNode);

        if (currNode.Right != null)
            OrderInOrder(currNode.Right, resultList);
    }

    private void OrderPostOrder(
        IBinaryTree<T> currNode,
        ICollection<IBinaryTree<T>> resultList)
    {
        if (currNode.Left != null)
            OrderPostOrder(currNode.Left, resultList);

        if (currNode.Right != null)
            OrderPostOrder(currNode.Right, resultList);

        resultList.Add(currNode);
    }
}