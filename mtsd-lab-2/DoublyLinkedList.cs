namespace mtsd_lab_2;

public sealed class Node<T>(T value)
{
    public Node<T>? Previous;
    public Node<T>? Next;
    public T Value = value;

    public static void CreateLinkedNode(T value, ref Node<T> previous, ref Node<T> next)
    {
        Node<T> node = new(value)
        {
            Previous = previous,
            Next = next
        };

        previous.Next = node;
        next.Previous = node;
    }
}

public class DoublyLinkedList<T>
{
    internal Node<T>? Head;

    public long Count { get; internal set; }

    // Testing methods

    internal bool TryTraverse(bool forward)
    {
        long nodes = 0;
        Node<T>? node = Head;

        while (node is not null)
        {
            node = forward ? node.Next : node.Previous;
            nodes++;
            if (node == Head)
            {
                break;
            }
        }

        return nodes == Count;
    }

    internal void AssertValid()
    {
        Assert.IsTrue(Count >= 0);

        if (Count == 0)
        {
            Assert.IsTrue(Head is null);
        }
        else if (Count == 1)
        {
            Assert.IsTrue(Head is not null);
            Assert.IsTrue(Head.Previous is null);
            Assert.IsTrue(Head.Next is null);
        }

        Assert.IsTrue(TryTraverse(forward: true));
        Assert.IsTrue(TryTraverse(forward: false));
    }

    // Required methods

    public void Append(T value)
    {
        if (Head is null)
        {
            Head = new(value);
        }
        else
        {
            if (Head.Previous is null)
            {
                Node<T>.CreateLinkedNode(value, ref Head, ref Head);
            }
            else
            {
                Node<T>.CreateLinkedNode(value, ref Head.Previous, ref Head);
            }
        }

        Count++;
    }
}
