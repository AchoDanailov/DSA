using DSA.TreeDataStructures.Exercises.Matrix;
using DSA.TreeDataStructures.Exercises.TreeEachLevelOfDepthIndented;

namespace DSA.TreeDataStructures.Exercises;

public class Program
{
    public static void Main()
    {
        // TreeWithIdentationsOnEachDepthLevel();
        TheMatrix();
    }

    private static void TreeWithIdentationsOnEachDepthLevel()
    {
        Tree<int> tree = new Tree<int>(7,
            new Tree<int>(12, 
                new Tree<int>(18),
                new Tree<int>(23),
                new Tree<int>(50)),
            new Tree<int>(11),
            new Tree<int>(16,
                new Tree<int>(88),
                new Tree<int>(5,
                    new Tree<int>(81))));

        Console.WriteLine(tree.GetTreeAsStringWithIdentation());
    }

    // Example TheMatrix input (more in './Matrix/task.md'):
    // 5 3  
    // a a a  
    // a a a  
    // a b a  
    // a b a  
    // a b a  
    // x  
    // 0 0 
    private static void TheMatrix()
    {
        TheMatrix theMatrix = BuildTheMatrix();
        SolveDfs(theMatrix);
        // SolveBfs(theMatrix);
        Console.WriteLine(theMatrix.ToOutputString());
    }

    private static void SolveDfs(TheMatrix theMatrix) => theMatrix.SolveDfs();
    private static void SolveBfs(TheMatrix theMatrix) => theMatrix.SolveBfs();

    private static TheMatrix BuildTheMatrix()
    {
        (int rows, int cols) = ParseInputWithTwoNums(Console.ReadLine());
        if (rows <= 0 || cols <= 0)
            throw new ArgumentException("Invalid number of rows or cols.");
        
        char[][] matrix = BuildMatrix(rows, cols);

        bool isParsed = char.TryParse(Console.ReadLine(), out char fillChar);
        if (!isParsed)
        {
            throw new ArgumentException(
                "Invalid fillChar. Expecting single alphanumeric char.");
        }

        (int startRow, int startCol) = ParseInputWithTwoNums(Console.ReadLine());
        if (startRow < 0 || startRow >= matrix.Length
                         || startCol < 0 || startCol >= matrix[0].Length)
        {
            throw new ArgumentException("Invalid startRow or startCol.");
        }

        return new TheMatrix(matrix, fillChar, startRow, startCol);
    }

    private static char[][] BuildMatrix(int rows, int cols)
    {
        char[][] matrix = new char[rows][];
        for (int i = 0; i < rows; i++)
        {
            bool isValidRow = TryParseRow(Console.ReadLine(),
                cols, out string currRow);
            if (!isValidRow)
            {
                throw new ArgumentException($"Invalid row: {currRow}.");
            }
            
            matrix[i] = new char[cols];
            for (int j = 0; j < cols; j++)
            {
                matrix[i][j] = currRow[j];
            }
        }

        return matrix;
    }

    private static ValueTuple<int, int> ParseInputWithTwoNums(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input can not be empty");

        string[] tokens = input.TrimEnd().Split(' ');
        if (tokens.Length != 2)
        {
            throw new ArgumentException(
                "Invalid arguments length.", nameof(input));
        }

        int[] intTokens = new int[2];
        for (int i = 0; i < tokens.Length; i++)
        {
            bool res = int.TryParse(tokens[i], out intTokens[i]);
            if (res == false)
            {
                throw new ArgumentException(
                    "Invalid arguments format.", nameof(input));
            }
        }

        return (intTokens[0], intTokens[1]);
    }

    private static bool TryParseRow(string? input, int cols, out string row)
    {
        row = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        string parsedRowContent 
            = string.Join("", input.Split(" ", StringSplitOptions.RemoveEmptyEntries)).Trim();
        if (parsedRowContent.Length != cols)
        {
            return false;
        }

        row = parsedRowContent;
        return true;
    }
}