namespace mtsd_lab_2;

[TestClass]
public sealed class Tests
{
    [TestMethod]
    public void Append()
    {
        DoublyLinkedList<char> list = new();
        list.Append('a');
        list.Append('b');
        list.Append('c');
        list.Append('d');

        list.AssertValid();
        Assert.AreEqual(4, list.Count);
    }
}
