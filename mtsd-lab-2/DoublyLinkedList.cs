using System.Collections;

namespace mtsd_lab_2;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    internal LinkedList<T> list = [];

    public int Count => list.Count;

    // Internal methods

    internal LinkedListNode<T> NodeAtIndex(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        LinkedListNode<T> node;
        if (index <= Count / 2)
        {
            node = list.First!;
            for (int i = 0; i < index; i++)
            {
                node = node.Next!;
            }
        }
        else
        {
            node = list.Last!;
            for (int i = Count - 1; i > index; i--)
            {
                node = node.Previous!;
            }
        }

        return node;
    }

    // Public methods

    public void Add(T value)
    {
        list.AddLast(value);
    }

    public void Insert(T value, int index)
    {
        if (index == Count)
        {
            list.AddLast(value);
        }
        else if (index == 0)
        {
            list.AddFirst(value);
        }
        else
        {
            LinkedListNode<T> node = NodeAtIndex(index - 1);
            list.AddAfter(node, value);
        }
    }

    public T Delete(int index)
    {
        LinkedListNode<T> node = NodeAtIndex(index);
        T value = node.Value;
        list.Remove(node);
        return value;
    }

    public void DeleteAll(T value)
    {
        LinkedListNode<T>? node = list.First;
        while (node is not null)
        {
            LinkedListNode<T>? next = node.Next;
            if (EqualityComparer<T>.Default.Equals(node.Value, value))
            {
                list.Remove(node);
            }
            node = next;
        }
    }

    public T Get(int index)
    {
        return NodeAtIndex(index).Value;
    }

    public DoublyLinkedList<T> Clone()
    {
        DoublyLinkedList<T> result = [];
        result.Extend(list);

        return result;
    }

    public void Reverse()
    {
        LinkedList<T> reversed = [];
        LinkedListNode<T>? node = list.Last;

        while (node is not null)
        {
            reversed.AddLast(node.Value);
            node = node.Previous;
        }

        list = reversed;
    }

    public int FindFirst(T value)
    {
        foreach ((int index, T item) in this.Index())
        {
            if (EqualityComparer<T>.Default.Equals(item, value))
            {
                return index;
            }
        }

        return -1;
    }

    public int FindLast(T value)
    {
        LinkedListNode<T>? node = list.Last ?? list.First;
        for (int index = Count - 1; index >= 0; index--)
        {
            if (EqualityComparer<T>.Default.Equals(node!.Value, value))
            {
                return index;
            }
            node = node!.Previous;
        }

        return -1;
    }

    public void Clear()
    {
        list.Clear();
    }

    public void Extend(IEnumerable<T> enumerable)
    {
        foreach (T item in enumerable)
        {
            list.AddLast(item);
        }
    }

    // Interfaces

    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}