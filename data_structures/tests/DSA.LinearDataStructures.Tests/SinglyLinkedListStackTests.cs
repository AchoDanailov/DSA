using NUnit.Framework;
using DSA.LinearDataStructures.Stack;

namespace DSA.LinearDataStructures.Tests;

[TestFixture]
public class SinglyLinkedListStackTests 
{
    [Test]
    public void Push_WorksCorrectly()
    {
        SinglyLinkedListStack<int> stack = new SinglyLinkedListStack<int>();

        for (int i = 0; i < Random.Shared.Next(100); i++)
        {
            stack.Push(Random.Shared.Next());
            Assert.That(stack.Size, Is.EqualTo(i + 1));
        }
    }

    [Test]
    public void Pop_WhenEmpty_ShouldThrow()
    {
        SinglyLinkedListStack<int> stack = new SinglyLinkedListStack<int>();
        Assert.Throws<InvalidOperationException>(() => stack.Pop());
    }
    
    [Test]
    public void Pop_WorksCorrectly()
    {
        int numOfPushes = Random.Shared.Next(maxValue: 100);
        SinglyLinkedListStack<int> stack = GetRandomFilledStack(numOfPushes);

        int randomNumToPush = Random.Shared.Next();
        stack.Push(randomNumToPush);
        
        Assert.That(randomNumToPush, Is.EqualTo(stack.Pop()));
        Assert.That(stack.Size, Is.EqualTo(numOfPushes));
    }

    [Test]
    public void Peek_WhenEmpty_Throws()
    {
        int numOfPushes = Random.Shared.Next(maxValue: 100);
        SinglyLinkedListStack<int> stack = GetRandomFilledStack(numOfPushes);

        int randomNumToPush = Random.Shared.Next();
        stack.Push(randomNumToPush);

        Assert.That(randomNumToPush, Is.EqualTo(stack.Peek()));
        Assert.That(stack.Size, Is.EqualTo(numOfPushes + 1));
    }

    [Test]
    public void IsEmpty_WhenEmpty_ReturnsTrue()
    {
        SinglyLinkedListStack<int> stack = new SinglyLinkedListStack<int>();
        Assert.That(stack.IsEmpty(), Is.True);
    }

    [Test]
    public void IsEmpty_WhenNotEmpty_ReturnsFalse()
    {
        int numOfPushes = Random.Shared.Next(maxValue: 100);
        SinglyLinkedListStack<int> stack = GetRandomFilledStack(numOfPushes);
        Assert.That(stack.IsEmpty(), Is.False);
    }

    [Test]
    public void Contains_WhenElementIsntContained_ReturnsFalse()
    {
        SinglyLinkedListStack<int> stack = new SinglyLinkedListStack<int>();
        Assert.That(stack.Contains(Random.Shared.Next()), Is.False);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void Contains_WorksCorrectly(int pushesAfterPushingTarget)
    {
        SinglyLinkedListStack<int> stack = GetRandomFilledStack(Random.Shared.Next(100));
        int pushed = Random.Shared.Next();
        stack.Push(pushed);
        
        for (int i = 0; i < pushesAfterPushingTarget; i++)
        {
            stack.Push(Random.Shared.Next());
        }

        Assert.That(stack.Contains(pushed), Is.True);
    }

    private static SinglyLinkedListStack<int> GetRandomFilledStack(int numOfPushes = 100)
    {
        SinglyLinkedListStack<int> stack = new SinglyLinkedListStack<int>();
        for (int i = 0; i < numOfPushes; i++)
        {
            stack.Push(Random.Shared.Next());
        }

        return stack;
    }
}