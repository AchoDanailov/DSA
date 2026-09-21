using DSA.TreeDataStructures.Interfaces;

namespace DSA.TreeDataStructures;

public class Program
{
    public static void Main()
    {
        IBinaryTree<int> tree = new BinaryTree<int>(7,
            new BinaryTree<int>(12,
                new BinaryTree<int>(18),
                new BinaryTree<int>(23)),
            new BinaryTree<int>(11,
                new BinaryTree<int>(88),
                new BinaryTree<int>(5,
                    new BinaryTree<int>(81))));

        tree.ForEachInOrder(v => Console.WriteLine(v.ToString()));
    }
}