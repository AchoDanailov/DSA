using DSA.LinearDataStructures.Interfaces;
using DSA.LinearDataStructures.Nodes;

namespace DSA.LinearDataStructures.Queue;

public class DoublyLinkedListQueue<T> : IDoublyLinkedListQueue<T>
{
    private IDoublyNode<T>? _head; 
    private IDoublyNode<T>? _tail;

    public int Size { get; private set; }

    // O(1)
    public bool Enqueue(T value)
    {
        IDoublyNode<T> node = new DoublyNode<T>(value);

        if (this._head == null)
        {
            this._head = node;
            this._tail = node;
        }
        else
        {
            IDoublyNode<T> current = this._tail!;
            current.Next = node;
            node.Previous = current;
            this._tail = node;
        }
        
        this.Size += 1;
        return true;
    }

    // O(1)
    public T Dequeue()
    {
        if (this._head == null)
            throw new InvalidOperationException("Queue is empty!");	    

        T res = this._head.Value;
        
        if (this._head.Next == null)
        {
            this._head = null;
            this._tail!.Previous = null;
            this._tail = null;
        }
        else 
        {
            this._head = this._head.Next;
            this._head.Previous = null;
        }

        this.Size -= 1;
        return res;
    }

    // O(1)
    public T Peek()
    {
        if (this._head == null)
            throw new InvalidOperationException("Queue is empty!");	    

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
        if (this._head == null)
            return false;

        IDoublyNode<T>? current = this._tail!;
        while (current != null)
        {
            if (current!.Value!.Equals(value))
                return true;

            current = current.Previous; 
        }

        return false;
    }
}
