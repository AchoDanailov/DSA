using DSA.TreeDataStructures.Exercises.Matrix;

namespace DSA.TreeDataStructures.Exercises;

public class Program
{
    public static void Main()
    {
        TheMatrix();
    }

    private static void TheMatrix()
    {
        TheMatrix theMatrix = BuildTheMatrix();
        theMatrix.SolveBfs();
        Console.WriteLine(theMatrix.ToOutputString());
    }

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

        int[] tokens = input.TrimEnd()
            .Split(' ')
            .Select(e => int.Parse(e))
            .ToArray();
        if (tokens.Length != 2)
        {
            throw new ArgumentException(
                "Invalid arguments length.", nameof(input));
        }

        return (tokens[0], tokens[1]);
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