namespace DSA.TreeDataStructures.Common;

internal static class ExceptionMessages
{
    internal static class Common
    {
        internal const string ValueCanNotBeLessThanZero = "Value can not be less than 0.";
    }

    internal static class BasicTree
    {
        internal const string TreeNodeCanNotBeEmpty = "Tree node can not be empty";
    }

    internal static class BinaryTree
    {
        internal const string TargetNotFound = "Node with the value {0} was not found.";
        internal const string RootDoesNotHaveAncestors = "Root does not have ancestors.";
    }

    internal static class Heap
    {
        internal const string HeapIsEmpty = "Heap is empty.";
    }

    internal static class BinarySearchTree
    {
        internal const string IsEmpty = "BinarySearchTree is empty";
    }
}