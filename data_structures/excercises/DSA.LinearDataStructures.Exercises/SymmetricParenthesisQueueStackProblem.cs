namespace DSA.LinearDataStructures.Exercises;

/*
*/
/// <summary>
/// This class solves the following problem trough calling the <see cref="Solution"/> method.
/// A sequence of parentheses is balanced if every open parenthesis can be paired uniquely with a closed parenthesis that occurs after the former.
/// Also, the interval between them must be balanced. You will be given three types of parentheses: (, {, and [.
/// {[()]} - This is a balanced parenthesis.
/// {[(])} - This is not a balanced parenthesis.
/// </summary>
internal static class SymmetricParenthesisQueueStackProblem
{
    private static readonly Dictionary<char, char> ParenthesisMapping = new Dictionary<char, char>()
    {
        { '[', ']' },
        { '{', '}' },
        { '(', ')' }
    };
    
    private static readonly char[] ValidChars = new char[] { '(', '[', '{', ')', ']', '}', ' ' };

    internal static void Solution()
    {
        string input = PromptForParenthesis().TrimEnd();
        while (InputIsNotValid(input))
        {
            Console.WriteLine();
            Console.WriteLine("Invalid input");
            input = PromptForParenthesis().TrimEnd();
        }
        
        char[] parenthesis = input.ToCharArray();

        Queue<char> queue = new Queue<char>();
        Stack<char> stack = new Stack<char>();
        for (int i = 0; i < parenthesis.Length; i++)
        {
            if (parenthesis[i] == ' ')
                continue;
            
            if (i < parenthesis.Length / 2)
                queue.Enqueue(parenthesis[i]);
            else
                stack.Push(parenthesis[i]);
        }

        bool areParenthesisSymmetric = true;
        while (queue.Count > 0 && stack.Count > 0)
        {
            char leftParenthesis = queue.Dequeue();
            char rightParenthesis = stack.Pop();
            if (rightParenthesis != ParenthesisMapping[leftParenthesis])
            {
                areParenthesisSymmetric = false;
                break;
            }
        }
        
        // handles: ()), (() etc.
        if (stack.Count > 0 || queue.Count > 0)
            areParenthesisSymmetric = false;

        Console.WriteLine(Result(areParenthesisSymmetric));
    }

    private static bool InputIsNotValid(string input)
    {
        return string.IsNullOrWhiteSpace(input) ||
               input.Any(c => !CharIsValid(c)) ||
               ContainsInvalidWhiteSpace(input) ||
               input.Count(c => !char.IsWhiteSpace(c)) % 2 != 0;
    }

    private static string Result(bool areParenthesisSymmetric)
    {
        if (areParenthesisSymmetric) return "Parenthesis are symmetric";
        else return "Parenthesis are not symmetric";
    }

    private static bool CharIsValid(char c)
    {
        return ValidChars.Contains(c);
    }
    
    // Only place white space is allowed is in middle.
    private static bool ContainsInvalidWhiteSpace(string input)
    {
        return input
            .Select((c, idx) => (c, idx))
            .Where(x => x.c == ' ')
            .Any(x => x.idx != input.Length / 2);
    }
    
    private static string PromptForParenthesis()
    {
        Console.WriteLine("Enter a sequence of parenthesis and see if they are symmetric. Example: \"[{}]\"");
        Console.WriteLine("Only place where white space is allowed is in middle. Example: \"[{ }]\"");
        return Console.ReadLine()!;
    }
}