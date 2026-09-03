using DSA.LinearDataStructures.Interfaces;
using DSA.LinearDataStructures.Nodes;

namespace DSA.LinearDataStructures.Stack;

public class SinglyListStack<T> : ISinglyListStack<T>
{
    private ISinglyNode<T>? _head; 

    public int Size { get; private set; }
    
    // O(1)
    public bool Push(T value)
    {
        ISinglyNode<T> node = new SinglyNode<T>(value);

        if (this._head == null) 
        {
            this._head = node;
        }
        else
        {
            node.Next = this._head;
            this._head = node;
        }

        this.Size += 1;
        return true;
    }

    // O(1)
    public T Pop()
    {
        if (this._head == null)
            throw new InvalidOperationException("Stack is empty!");

        T removing = this._head.Value;

        this._head = this._head.Next;
        this.Size -= 1;

        return removing;
    }

    // O(1)
    public T Peek()
    {
        if (this._head == null)
            throw new InvalidOperationException("Stack is empty!");

        return this._head!.Value;
    }

    // O(1)
    public bool IsEmpty()
    {
        if (this._head == null)
            return true;

        return false;
    }

    // O(n)
    public bool Contains(T value) 
    {
        if(this._head == null)
            return false;

        ISinglyNode<T>? current = this._head!;
        while (current != null)
        {
            if (current.Value!.Equals(value))
                return true;

            current = current.Next;
        }

        return false;
    }
}