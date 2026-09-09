using DSA.LinearDataStructures.ArrayDeque;

namespace DSA.LinearDataStructures;

internal class Program
{
    internal static void Main(string[] args)
    {
        ArrayDeque<int> deque = new ArrayDeque<int>();
        for (int i = 0; i < 10; i++)
            deque.Add(i);

        foreach (int item in deque)
            PrintLine(item);
    }

    private static void PrintLine(object? content = null, params object[] parameters)
    {
        if (content is string str) Console.WriteLine(string.Format(str ?? "", parameters));
        else Console.WriteLine(content?.ToString() ?? "");
        Console.WriteLine();
    }
}