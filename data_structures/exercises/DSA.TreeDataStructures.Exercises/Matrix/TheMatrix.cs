using System.Text;

namespace DSA.TreeDataStructures.Exercises.Matrix;

enum Direction
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3
}

public class TheMatrix
{
    private readonly char[][] _matrix;
    private readonly int _startRow;
    private readonly int _startCol;
    private readonly char _fillChar;
    private readonly char _startChar;
    
    public TheMatrix(char[][] matrix, char fillChar, int startRow, int startCol)
    {
        if (fillChar == matrix[startRow][startCol])
        {
            throw new ArgumentException(
                "Character 'fillChar' can not be the same as the character at position matrix[startRow][startCol].");
        }
        
        this._matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
        this._startRow = startRow;
        this._startCol = startCol;
        this._fillChar = fillChar;
        this._startChar = this._matrix[this._startRow][this._startCol];
    }

    public void SolveDfs()
    {
        ValueTuple<int, int> currPosition 
            = new ValueTuple<int, int>(this._startRow, this._startCol);
        this.EvaluateAndFillIfNecessaryNextPositon(currPosition);
    }

    private void EvaluateAndFillIfNecessaryNextPositon((int rowIndex, int colIndex) position)
    {
        if (!this.IsInBounds(position) || !IsValid(this.CharAt(position)))
            return;

        this._matrix[position.rowIndex][position.colIndex] = this._fillChar;

        this.EvaluateAndFillIfNecessaryNextPositon(NextIndex(Direction.Up, position));
        this.EvaluateAndFillIfNecessaryNextPositon(NextIndex(Direction.Left, position));
        this.EvaluateAndFillIfNecessaryNextPositon(NextIndex(Direction.Down, position));
        this.EvaluateAndFillIfNecessaryNextPositon(NextIndex(Direction.Right, position));
    }

    //NOTE: Valid positions are filled on Enqueue not on Dequeue.
    //The point of this is to prevent indices from getting Enqueued more than once (debugger or diagram needed to see/visualize this).
    public void SolveBfs()
    {
        Queue<(int rowIndex, int colIndex)> queue = new Queue<(int rowIndex, int colIndex)>(
                capacity: this._matrix.Length * this._matrix[0].Length);
        
        queue.Enqueue((this._startRow, this._startCol));
        this._matrix[this._startRow][this._startCol] = this._fillChar;
        
        while (queue.Count > 0)
        {
            (int currRow, int currCol) currPosition = queue.Dequeue();
            this.TraverseNextPositions(currPosition, queue);
        }
    }
    
    public string ToOutputString()
    {
        StringBuilder sb = new StringBuilder();
        int height = _matrix.Length;
        int width = _matrix[0].Length;
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                sb.Append(this._matrix[i][j]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private void TraverseNextPositions(
        (int currRow, int currCol) position,
        Queue<(int rowIndex, int colIndex)> queue)
    {
        (int nextRow, int nextCol) nextIndices = NextIndex(Direction.Up, position);
        if (this.IsInBounds(nextIndices))
        {
            if (this.IsValid(this.CharAt(nextIndices)))
            {
                this._matrix[nextIndices.nextRow][nextIndices.nextCol] = this._fillChar;
                queue.Enqueue(nextIndices);
            }
        }

        nextIndices = NextIndex(Direction.Left, position);
        if (this.IsInBounds(nextIndices))
        {
            if (this.IsValid(this.CharAt(nextIndices)))
            {
                this._matrix[nextIndices.nextRow][nextIndices.nextCol] = this._fillChar;
                queue.Enqueue(nextIndices);
            }
        }
        
        nextIndices = NextIndex(Direction.Down, position);
        if (this.IsInBounds(nextIndices))
        {
            if (this.IsValid(this.CharAt(nextIndices)))
            {
                this._matrix[nextIndices.nextRow][nextIndices.nextCol] = this._fillChar;
                queue.Enqueue(nextIndices);
            }
        }
        
        nextIndices = NextIndex(Direction.Right, position);
        if (this.IsInBounds(nextIndices))
        {
            if (this.IsValid(this.CharAt(nextIndices)))
            {
                this._matrix[nextIndices.nextRow][nextIndices.nextCol] = this._fillChar;
                queue.Enqueue(nextIndices);
            }
        }
    }

    private bool IsValid(char symbol)
    {
        return symbol == this._startChar;
    }
    
    private bool IsInBounds((int rowIndex, int colIndex) position)
    {
        return position.rowIndex >= 0 &&
               position.colIndex >= 0 &&
               position.rowIndex < this._matrix.Length &&
               position.colIndex < this._matrix[0].Length;
    }

    private char CharAt((int rowIndex, int colIndex) position)
    {
        return this._matrix[position.rowIndex][position.colIndex];
    }
    
    private static ValueTuple<int, int> NextIndex(
        Direction direction,
        (int currRow, int currCol) currPosition)
    {
        return direction switch
        {
            Direction.Up => (currPosition.currRow - 1, currPosition.currCol),
            Direction.Left => (currPosition.currRow, currPosition.currCol - 1),
            Direction.Down => (currPosition.currRow + 1, currPosition.currCol),
            Direction.Right => (currPosition.currRow, currPosition.currCol + 1),
            _ => throw new ArgumentException(nameof(direction))
        };
    }
}