using System.Text;

namespace DSA.TreeDataStructures.Exercises.TreeEachLevelOfDepthIndented;

public class Tree<T> 
{
    private Tree<T>? _parent;
    private readonly ICollection<Tree<T>> _children;

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this._children = new List<Tree<T>>();

        foreach (Tree<T> tree in children)
        {
            this._children.Add(tree);
            tree._parent = this;
        }
    }

    public T Value { get; private set; }

    public string GetTreeAsStringWithIdentation()
    {
        int identationLevel = 0;
        StringBuilder resultSb = new StringBuilder();
        this.TraverseAndBuildTreeAsString(this, identationLevel, resultSb);
        return resultSb.ToString().TrimEnd();
    }

    private void TraverseAndBuildTreeAsString(
        Tree<T> currNode,
        int identationLevel,
        StringBuilder resultSb)
    {
        for (int i = 0; i < identationLevel; i++)
            resultSb.Append(" ");

        resultSb.AppendLine(currNode.Value!.ToString());

        foreach (Tree<T> child in currNode._children)
            this.TraverseAndBuildTreeAsString(child, identationLevel + 2, resultSb);
    }
}
