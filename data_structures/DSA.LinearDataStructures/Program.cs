using DSA.LinearDataStructures.ArrayList;
using DSA.LinearDataStructures.Interfaces;

namespace DSA.LinearDataStructures;

internal class Program
{
    internal static void Main(string[] args)
    {
        PrintLine();
    }

    private static void ArrayListEnumeratorTest()
    {
        IArrayList<int> arrayList = new ArrayList<int>() { 1, 2, 3, 4 };
        foreach (int el in arrayList)
        {
            PrintLine(el);
        }
    }

    private static void PrintLine(object? content = null, params object[] parameters)
    {
        if (content is string str) Console.WriteLine(string.Format(str ?? "", parameters));
        else Console.WriteLine(content?.ToString() ?? "");
        Console.WriteLine();
    }
}