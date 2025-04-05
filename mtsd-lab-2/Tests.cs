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
}
