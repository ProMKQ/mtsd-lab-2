namespace mtsd_lab_2;

[TestClass]
public sealed class Tests
{
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

        list.AssertValid();
        Assert.IsTrue(Enumerable.SequenceEqual(['b', 'c'], list));
    }

    [TestMethod]
    public void Clone()
    {
        DoublyLinkedList<char> list = ['a', 'b', 'c', 'd'];
        DoublyLinkedList<char> clone = list.Clone();

        clone.AssertValid();
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
