using DSA.TreeDataStructures.Common;
using DSA.TreeDataStructures.Enums;
using DSA.TreeDataStructures.Interfaces;

namespace DSA.TreeDataStructures.BinarySearchTree;

public class BinarySearchTree<T> : IBinarySearchTree<T>
    where T : IComparable<T>
{
    private IBSTNode<T>? _root;

    public BinarySearchTree()
    {
    }

    public BinarySearchTree(T rootValue)
        : this(new BSTNode<T>(rootValue))
    {
    }

    public BinarySearchTree(IBSTNode<T> root)
    {
        this._root = root;
        this.Size += 1;
    }

    public int Size { get; private set; }

    public IBSTNode<T>? Search(T element)
    {
        if (this._root == null)
        {
            throw new InvalidOperationException(
                ExceptionMessages.BinarySearchTree.IsEmpty);
        }

        IBSTNode<T>? res = Find(this._root, element);
        return res;
    }

    public void Add(T element)
    {
        BSTNode<T> node = new BSTNode<T>(element);

        if (this._root == null)
        {
            this._root = node;
            this.Size += 1;
            return;
        }
        
        bool isAdded = EvaluateAndAddNode(this._root, node);
        if (isAdded)
        {
            this.Size += 1;
        }
    }

    public IBSTNode<T>? Remove(T element)
    {
        if (this._root == null)
        {
            throw new InvalidOperationException(
                ExceptionMessages.BinarySearchTree.IsEmpty);
        }

        IBSTNode<T>? targetNode = Find(this._root, element);
        if (targetNode == null)
        {
            return null;
        }

        IBSTNode<T>? successor = this.GetNodeSuccessorIfAny(targetNode);
        this.OrphanAndReplaceIfNecessary(targetNode, successor);

        this.Size -= 1;
        return targetNode;
    }

    public bool Contains(T element)
    {
        if (this.Size == 0)
            return false;

        bool isContained = Contains(this._root!, element);
        return isContained;
    }

    public IEnumerable<T> GetAllValueInOrder()
    {
        if (this._root == null)
        {
            throw new InvalidOperationException(
                ExceptionMessages.BinarySearchTree.IsEmpty);
        }

        ICollection<T> resList = new List<T>(capacity: this.Size);
        GetValuesInOrder(this._root, resList);

        return resList;
    }

    private static void GetValuesInOrder(IBSTNode<T> currNode, ICollection<T> resList)
    {
        if (currNode.LeftChild != null)
            GetValuesInOrder(currNode.LeftChild, resList);

        resList.Add(currNode.Value!);

        if (currNode.RightChild != null)
            GetValuesInOrder(currNode.RightChild, resList);
    }

    private static bool Contains(IBSTNode<T> currNode, T searchedElement)
    {
        bool isContained = false;
        if (searchedElement.CompareTo(currNode.Value) == 0)
        {
            isContained = true;
        }
        else if (searchedElement.CompareTo(currNode.Value) < 0)
        {
            if (currNode.LeftChild != null)
                isContained = Contains(currNode.LeftChild, searchedElement);
        }
        else 
        {
            if (currNode.RightChild != null)
                isContained = Contains(currNode.RightChild, searchedElement);
        }

        return isContained;
    }

    private static IBSTNode<T>? Find(IBSTNode<T> currNode, T searchedElement)
    {
        IBSTNode<T>? res = null;
        if (searchedElement.CompareTo(currNode.Value) == 0)
        {
            res = currNode;
        }
        else if (searchedElement.CompareTo(currNode.Value) < 0)
        {
            if (currNode.LeftChild != null)
                res = Find(currNode.LeftChild, searchedElement);
        }
        else 
        {
            if (currNode.RightChild != null)
                res = Find(currNode.RightChild, searchedElement);
        }

        return res;
    }

    private static bool EvaluateAndAddNode(IBSTNode<T> currNode, BSTNode<T> nodeToAdd)
    {
        if (nodeToAdd.Value!.CompareTo(currNode.Value) == 0)
        {
            return false;
        }
        else if (nodeToAdd.Value!.CompareTo(currNode.Value) < 0)
        {
            if (currNode.LeftChild != null)
            {
                return EvaluateAndAddNode(currNode.LeftChild, nodeToAdd);
            }

            currNode.LeftChild = nodeToAdd;
            nodeToAdd.Parent = currNode;
            return true;
        }
        else 
        {
            if (currNode.RightChild != null)
            {
                return EvaluateAndAddNode(currNode.RightChild, nodeToAdd);
            }

            currNode.RightChild = nodeToAdd;
            nodeToAdd.Parent = currNode;
            return true;
        }
    }

    private IBSTNode<T>? GetNodeSuccessorIfAny(IBSTNode<T> targetNode)
    {
        IBSTNode<T>? successor = null;

        if (targetNode.RightChild != null)
        {
            if (targetNode.RightChild.LeftChild == null)
            {
                successor = targetNode.RightChild;
            }
            else
            {
                successor = GetLowestChildInGivenDirection(
                    targetNode.RightChild,
                    Direction.Left);
            }

            this.OrphanAndReplaceIfNecessary(successor, successor.RightChild);
        }
        else if (targetNode.LeftChild != null)
        {
            if (targetNode.LeftChild.RightChild == null)
            {
                successor = targetNode.LeftChild;
            }
            else
            {
                successor = GetLowestChildInGivenDirection(
                    targetNode.LeftChild,
                    Direction.Right);
            }

            this.OrphanAndReplaceIfNecessary(successor, successor.LeftChild);
        }

        return successor;
    }

    private static IBSTNode<T> GetLowestChildInGivenDirection(
        IBSTNode<T> currNode,
        Direction direction)
    {
        if (direction == Direction.Left)
        {
            if (currNode.LeftChild == null)
                return currNode;
            
            return GetLowestChildInGivenDirection(currNode.LeftChild, direction);
        }
        else 
        {
            if (currNode.RightChild == null)
                return currNode;

            return GetLowestChildInGivenDirection(currNode.RightChild, direction);
        }
    }

    private void OrphanAndReplaceIfNecessary(
        IBSTNode<T> targetNode,
        IBSTNode<T>? replacement = null)
    {
        bool willReplace = replacement != null;

        IBSTNode<T>? parent = targetNode.Parent;
        if (parent != null)
        {
            ReassignTargetToParentRelationship(
                parent, targetNode, replacement, willReplace);
        }

        ReassignChildrenIfAny(targetNode, replacement, willReplace);

        this.ReassignRootIfNecessary(targetNode, replacement);
    }

    private void ReassignRootIfNecessary(
        IBSTNode<T> targetNode,
        IBSTNode<T>? replacement)
    {
        if (targetNode == this._root)
        {
            if (replacement != null)
            {
                this._root = replacement;
            }
            else
            {
                this._root = null;
            }
        }
    }

    private static void ReassignChildrenIfAny(
        IBSTNode<T> targetNode,
        IBSTNode<T>? replacement,
        bool willReplace)
    {
        if (targetNode.LeftChild != null)
        {
            if (targetNode.LeftChild.Equals(replacement) == false)
            {
                if (willReplace) 
                {
                    replacement!.LeftChild = targetNode.LeftChild;
                    replacement.LeftChild.Parent = replacement;
                }
                else if (!willReplace)
                {
                    targetNode.LeftChild.Parent = null;
                }
            }

            targetNode.LeftChild = null;
        }

        if (targetNode.RightChild != null)
        {
            if (targetNode.RightChild.Equals(replacement) == false)
            {
                if (willReplace)
                {
                    replacement!.RightChild = targetNode.RightChild;
                    replacement.RightChild.Parent = replacement;
                }
                else if (!willReplace)
                {
                    targetNode.RightChild.Parent = null;
                }
            }

            targetNode.RightChild = null;
        }
    }

    private static void ReassignTargetToParentRelationship(
        IBSTNode<T> parent,
        IBSTNode<T> targetNode,
        IBSTNode<T>? replacement,
        bool willReplace)
    {
        if (parent.LeftChild != null && parent.LeftChild.Equals(targetNode))
        {
            if (willReplace) parent.LeftChild = replacement;
            else parent.LeftChild = null;
        }
        else if (parent.RightChild != null && parent.RightChild.Equals(targetNode))
        {
            if (willReplace) parent.RightChild = replacement;
            else parent.RightChild = null;
        }

        if (willReplace)
            replacement!.Parent = parent;   

        targetNode.Parent = null;
    }
}