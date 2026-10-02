using DSA.TreeDataStructures.Interfaces;
using DSA.TreeDataStructures.BinarySearchTree;
using DSA.TreeDataStructures.Heap;

namespace DSA.TreeDataStructures;

public class Program
{
    public static void Main()
    {
        BinaryTreeDemo();
    }

    private static void BinaryTreeDemo()
    {
        IBinaryTree<int> tree = GetBinaryTree();
        IBinaryTree<int> res = tree.FindLowestCommonAncestor(69, 48);
        Console.WriteLine(res!.Value.ToString());
    }

    private static IBinaryTree<int> GetBinaryTree()
    {
        BinaryTree<int> tree = new BinaryTree<int>(51,
            new BinaryTree<int>(36,
                new BinaryTree<int>(69, 
                    new BinaryTree<int>(89),
                    new BinaryTree<int>(48)),
                new BinaryTree<int>(50,
                    null,
                    new BinaryTree<int>(58))),
            new BinaryTree<int>(35,
                new BinaryTree<int>(61),
                new BinaryTree<int>(10)));

        return tree;
    }

    private static void BinarySearchTreeDemo()
    {
        IBinarySearchTree<int> bst = new BinarySearchTree<int>();
        int[] arr = new int[]{ 50, 30, 70, 20, 40, 60, 80, 75 };
        Array.ForEach(arr, e => bst.Add(e));

        bst.Remove(50);

        IEnumerable<int> nums = bst.GetAllValueInOrder();
        Console.WriteLine(string.Join(", ", nums.ToArray()));
    }

    private static void MaxBinaryHeapDemo()
    {
        int[] elements = new int[] { 15, 2, 3 };
        MaxBinaryHeap<int> heap = new MaxBinaryHeap<int>(elements);
        for (int i = 0; i < elements.Length; i++)
        {
            Console.WriteLine(heap.RemoveAt(heap.Size - 1));
        }

        Console.WriteLine(heap.ToString());
    }
}