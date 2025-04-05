using System.Collections;

namespace mtsd_lab_2;

public sealed class Node<T>
{
    public Node<T> Previous;
    public Node<T> Next;
    public T Value;

    internal Node(T value)
    {
        Value = value;
        Previous = this;
        Next = this;
    }

    internal Node(T value, Node<T> previous, Node<T> next)
    {
        Value = value;
        Previous = previous;
        Next = next;
    }

    internal static Node<T> CreateLinkedNode(T value, Node<T> previous, Node<T> next)
    {
        Node<T> node = new(value, previous, next);
        previous.Next = node;
        next.Previous = node;
        return node;
    }
}

public class DoublyLinkedList<T> : IEnumerable<T>
{
    internal Node<T>? Head;

    public long Count { get; internal set; }

    // Internal methods

    internal Node<T> NodeAtIndex(long index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Node<T> node = Head!;
        if (index <= Count / 2)
        {
            for (long i = 0; i < index; i++)
            {
                node = node.Next;
            }
        }
        else
        {
            for (long i = Count; i > index; i--)
            {
                node = node.Previous;
            }
        }

        return node;
    }

    internal bool TryTraverse(bool forward)
    {
        if (Head is null)
        {
            return Count == 0;
        }

        long nodes = 0;
        Node<T> node = Head;
        do
        {
            node = forward ? node.Next : node.Previous;
            nodes++;
        } while (node != Head && nodes <= Count);

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
            Node<T>.CreateLinkedNode(value, Head.Previous, Head);
        }

        Count++;
    }

    public void Insert(T value, long index)
    {
        if (Head is null)
        {
            Head = new(value);
        }
        else if (index == 0)
        {
            Head = Node<T>.CreateLinkedNode(value, Head.Previous, Head);
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
        throw new NotImplementedException();
    }

    public void DeleteAll(T element)
    {
        throw new NotImplementedException();
    }

    public T Get(long index)
    {
        return NodeAtIndex(index).Value;
    }

    public DoublyLinkedList<T> Clone()
    {
        DoublyLinkedList<T> result = [];
        if (Head is null)
        {
            return result;
        }

        Node<T> node = Head;
        do
        {
            result.Add(node.Value);
            node = node.Next;
        } while (node != Head);

        return result;
    }

    public void Reverse()
    {
        if (Head is null || Count == 1)
        {
            return;
        }

        Head = Head.Previous;

        Node<T> node = Head;
        do
        {
            (node.Previous, node.Next) = (node.Next, node.Previous);
            node = node.Previous;
        } while (node != Head);
    }

    public long FindFirst(T element)
    {
        throw new NotImplementedException();
    }

    public long FindLast(T element)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        Head = null;
        Count = 0;
    }

    public void Extend(DoublyLinkedList<T> list)
    {
        throw new NotImplementedException();
    }

    // Interfaces

    public IEnumerator<T> GetEnumerator()
    {
        if (Head is null)
        {
            yield break;
        }

        Node<T> node = Head;
        do
        {
            yield return node.Value;
            node = node.Next;
        } while (node != Head);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
