using DSA.TreeDataStructures.Interfaces;

namespace DSA.TreeDataStructures;

public class Program
{
    public static void Main()
    {
        MaxBinaryHeapDemo();   
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

    private static void BinaryTreeDemo()
    {
        IBinaryTree<int> tree = GetTree();
        Console.WriteLine(string.Join(", ", tree.PostOrder().Select(e => e.Value)));
    }

    private static IBinaryTree<int> GetTree()
    {
        return new BinaryTree<int>(7,
            new BinaryTree<int>(12,
                new BinaryTree<int>(18),
                new BinaryTree<int>(23)),
            new BinaryTree<int>(11,
                new BinaryTree<int>(88),
                new BinaryTree<int>(5,
                    new BinaryTree<int>(81))));
    }
}