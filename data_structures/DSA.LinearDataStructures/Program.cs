namespace DSA.LinearDataStructures;

internal class Program
{
    internal static void Main(string[] args)
    {
        PrintLine();
    }

    private static void PrintLine(string? content = null, params object[] parameters) 
    {
        Console.WriteLine(string.Format(content ?? "", parameters));
        Console.WriteLine();
    }
}