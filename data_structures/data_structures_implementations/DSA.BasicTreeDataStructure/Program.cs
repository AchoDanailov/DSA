using DSA.BasicTreeDataStructure.Interfaces;

namespace DSA.BasicTreeDataStructure;

public class Program
{
    public static void Main()
    {
        // IBasicTree<int> tree = new BasicTree<int>(7,
        //     new BasicTree<int>(12, 
        //         new BasicTree<int>(18),
        //         new BasicTree<int>(23),
        //         new BasicTree<int>(50)),
        //     new BasicTree<int>(11),
        //     new BasicTree<int>(16,
        //         new BasicTree<int>(88),
        //         new BasicTree<int>(5,
        //             new BasicTree<int>(81))));

        IBasicTree<string> tree = new BasicTree<string>();
        tree.Remove("hello");
    }
}