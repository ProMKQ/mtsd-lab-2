using System.Collections;

namespace mtsd_lab_2;

public sealed class Node<T>
{
    public Node<T>? Previous;
    public Node<T>? Next;
    public T Value;

    internal Node(T value)
    {
        Value = value;
    }

    private Node(T value, Node<T>? previous, Node<T>? next)
    {
        Value = value;
        Previous = previous;
        Next = next;
    }

    internal static Node<T> CreateLinkedNode(T value, Node<T>? previous, Node<T>? next)
    {
        Node<T> node = new(value, previous, next);
        if (previous is not null)
        {
            previous.Next = node;
        }
        if (next is not null)
        {
            next.Previous = node;
        }
        return node;
    }
}

public class DoublyLinkedList<T> : IEnumerable<T>
{
    internal Node<T>? Head;
    internal Node<T>? Tail;

    public long Count { get; internal set; }

    // Internal methods

    internal Node<T> NodeAtIndex(long index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Node<T> node;
        if (index <= Count / 2)
        {
            node = Head!;
            for (long i = 0; i < index; i++)
            {
                node = node.Next!;
            }
        }
        else
        {
            node = Tail!;
            for (long i = Count - 1; i > index; i--)
            {
                node = node.Previous!;
            }
        }

        return node;
    }

    internal T PopNode(Node<T> node)
    {
        T value = node.Value;

        if (node == Head)
        {
            Head = node.Next;
            if (Head is not null)
            {
                Head.Previous = null;
            }
            if (Count == 2)
            {
                Tail = null;
            }
        }
        else if (node == Tail)
        {
            if (Count == 2)
            {
                Tail = null;
                Head!.Next = null;
            }
            else
            {
                Tail = node.Previous;
                Tail!.Next = null;
            }
        }
        else
        {
            node.Previous!.Next = node.Next;
            node.Next!.Previous = node.Previous;
        }

        Count--;
        return value;
    }

    internal bool TryTraverse(bool forward)
    {
        long nodes = 0;
        Node<T>? node = forward ? Head : (Tail ?? Head);

        while (node is not null)
        {
            node = forward ? node.Next : node.Previous;
            nodes++;
        }

        return nodes == Count;
    }

    internal void AssertValid()
    {
        Assert.IsTrue(Count >= 0);
        Assert.IsTrue(TryTraverse(forward: true));
        Assert.IsTrue(TryTraverse(forward: false));
    }

    // Public methods

    public void Add(T value)
    {
        if (Head is null)
        {
            Head = new(value);
        }
        else
        {
            Tail = Node<T>.CreateLinkedNode(value, Tail ?? Head, null);
        }

        Count++;
    }

    public void Insert(T value, long index)
    {
        if (index == Count)
        {
            Add(value);
            return;
        }

        if (index == 0)
        {
            Head = Node<T>.CreateLinkedNode(value, null, Head);
        }
        else
        {
            Node<T> node = NodeAtIndex(index - 1);
            Node<T>.CreateLinkedNode(value, node, node.Next);
        }

        Count++;
    }

    public T Delete(long index)
    {
        return PopNode(NodeAtIndex(index));
    }

    public void DeleteAll(T item)
    {
        Node<T>? node = Head;
        while (node is not null)
        {
            if (EqualityComparer<T>.Default.Equals(node.Value, item))
            {
                PopNode(node);
            }
            node = node.Next;
        }
    }

    public T Get(long index)
    {
        return NodeAtIndex(index).Value;
    }

    public DoublyLinkedList<T> Clone()
    {
        DoublyLinkedList<T> result = [];
        foreach (T item in this)
        {
            result.Add(item);
        }

        return result;
    }

    public void Reverse()
    {
        Node<T>? node = Head;

        while (node is not null)
        {
            (node.Previous, node.Next, node) = (node.Next, node.Previous, node.Next);
        }

        (Head, Tail) = (Tail, Head);
    }

    public long FindFirst(T element)
    {
        foreach ((long index, T item) in this.Index())
        {
            if (EqualityComparer<T>.Default.Equals(item, element))
            {
                return index;
            }
        }

        return -1;
    }

    public long FindLast(T element)
    {
        Node<T>? node = Tail ?? Head;
        for (long index = Count - 1; index >= 0; index--)
        {
            if (EqualityComparer<T>.Default.Equals(node!.Value, element))
            {
                return index;
            }
            node = node!.Previous;
        }

        return -1;
    }

    public void Clear()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }

    public void Extend(IEnumerable<T> list)
    {
        foreach (T item in list)
        {
            Add(item);
        }
    }

    // Interfaces

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? node = Head;
        while (node is not null)
        {
            yield return node.Value;
            node = node.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}