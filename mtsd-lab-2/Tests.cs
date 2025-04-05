namespace mtsd_lab_2;

[TestClass]
public sealed class Tests
{
    [TestMethod]
    public void Length()
    {
        DoublyLinkedList<char> a = [];
        Assert.AreEqual(0, a.Count);

        a = ['a', 'b', 'c'];
        Assert.AreEqual(3, a.Count);
    }

    [TestMethod]
    public void Append()
    {
        DoublyLinkedList<char> list = [];
        list.Add('a');
        list.Add('b');
        list.Add('c');
        list.Add('d');

        list.AssertValid();
        Assert.IsTrue(Enumerable.SequenceEqual(['a', 'b', 'c', 'd'], list));
    }

    [TestMethod]
    public void Insert()
    {
        DoublyLinkedList<char> list = [];
        list.Insert('b', 0);
        list.Insert('c', 1);
        list.Insert('a', 0);
        list.Insert('d', 3);
        list.Insert('x', 2);

        list.AssertValid();
        Assert.IsTrue(Enumerable.SequenceEqual(['a', 'b', 'x', 'c', 'd'], list));

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert('y', 6));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert('y', -1));
    }

    [TestMethod]
    public void Delete()
    {
        DoublyLinkedList<char> list = ['a', 'b', 'c', 'd'];
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Delete(4));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Delete(-1));

        Assert.AreEqual('a', list.Delete(0));
        Assert.AreEqual('c', list.Delete(1));
        Assert.AreEqual('d', list.Delete(1));
        Assert.AreEqual('b', list.Delete(0));

        list.AssertValid();
        Assert.IsFalse(Enumerable.Any(list));
    }

    [TestMethod]
    public void DeleteAll()
    {
        DoublyLinkedList<char> list = ['x', 'a', 'b', 'x', 'x', 'c', 'x'];
        list.DeleteAll('x');

        list.AssertValid();
        Assert.IsTrue(Enumerable.SequenceEqual(['a', 'b', 'c'], list));

        list.DeleteAll('c');
        list.DeleteAll('a');
        list.DeleteAll('b');

        list.AssertValid();
        Assert.IsFalse(Enumerable.Any(list));
    }

    [TestMethod]
    public void Get()
    {
        DoublyLinkedList<char> list = ['a', 'b', 'c', 'd'];

        Assert.AreEqual('a', list.Get(0));
        Assert.AreEqual('b', list.Get(1));
        Assert.AreEqual('c', list.Get(2));
        Assert.AreEqual('d', list.Get(3));

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Get(4));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Get(-1));
    }

    [TestMethod]
    public void Clone()
    {
        DoublyLinkedList<char> list = ['a', 'b', 'c', 'd'];
        DoublyLinkedList<char> clone = list.Clone();

        clone.AssertValid();
        Assert.AreNotSame(list, clone);
        Assert.IsTrue(Enumerable.SequenceEqual(list, clone));
    }

    [TestMethod]
    public void Reverse()
    {
        DoublyLinkedList<char> list = ['d', 'c', 'b', 'a'];
        list.Reverse();

        list.AssertValid();
        Assert.IsTrue(Enumerable.SequenceEqual(['a', 'b', 'c', 'd'], list));
    }

    [TestMethod]
    public void FindFirst()
    {
        DoublyLinkedList<char> list = ['a', 'a', 'b', 'b', 'c', 'd'];
        Assert.AreEqual(0, list.FindFirst('a'));
        Assert.AreEqual(2, list.FindFirst('b'));
        Assert.AreEqual(4, list.FindFirst('c'));
        Assert.AreEqual(5, list.FindFirst('d'));
        Assert.AreEqual(-1, list.FindFirst('e'));
    }

    [TestMethod]
    public void FindLast()
    {
        DoublyLinkedList<char> list = ['d', 'b', 'a', 'a', 'c', 'c'];
        Assert.AreEqual(3, list.FindLast('a'));
        Assert.AreEqual(1, list.FindLast('b'));
        Assert.AreEqual(5, list.FindLast('c'));
        Assert.AreEqual(0, list.FindLast('d'));
        Assert.AreEqual(-1, list.FindFirst('e'));
    }

    [TestMethod]
    public void Clear()
    {
        DoublyLinkedList<char> list = ['a', 'b', 'c', 'd'];
        list.Clear();

        list.AssertValid();
        Assert.IsFalse(Enumerable.Any(list));
    }

    [TestMethod]
    public void Extend()
    {
        DoublyLinkedList<char> list1 = ['a', 'b'];
        DoublyLinkedList<char> list2 = ['c', 'd', 'e'];
        list1.Extend(list2);
        list2.Extend(list1);

        list1.AssertValid();
        list2.AssertValid();
        Assert.IsTrue(Enumerable.SequenceEqual(['a', 'b', 'c', 'd', 'e'], list1));
        Assert.IsTrue(Enumerable.SequenceEqual(['c', 'd', 'e', 'a', 'b', 'c', 'd', 'e'], list2));
    }
}
