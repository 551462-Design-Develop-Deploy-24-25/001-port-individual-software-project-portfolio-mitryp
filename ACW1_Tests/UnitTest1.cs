using ACW1;

namespace ACW1_Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That(Program.Add(1, 2), Is.EqualTo(1 + 2));
    }
}
