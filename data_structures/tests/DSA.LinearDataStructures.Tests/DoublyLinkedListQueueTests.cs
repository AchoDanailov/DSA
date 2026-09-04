using NUnit.Framework;
using DSA.LinearDataStructures;
using DSA.LinearDataStructures.Queue;

namespace DSA.LinearDataStructures.Tests;

[TestFixture]
public class DoublyLinkedListQueueTests
{
    [Test]
    public void Enqueue_WorksCorrectly()
    {
        DoublyLinkedListQueue<int> queue = new DoublyLinkedListQueue<int>();

        int numberOfElements = Random.Shared.Next(100);
        int[] enqueued = new int[numberOfElements];

        for (int i = 0; i < numberOfElements; i++)
        {
            int randomNumToEnqueue = Random.Shared.Next();
            enqueued[i] = randomNumToEnqueue;
            queue.Enqueue(randomNumToEnqueue);
            Assert.That(queue.Size, Is.EqualTo(i + 1));
        }

        for (int i = 0; i < enqueued.Length; i++)
        {
            Assert.That(enqueued[i], Is.EqualTo(queue.Dequeue()));
        }

        Assert.That(queue.Size, Is.EqualTo(0));
    }

    [Test]
    public void Dequeue_WhenEmpty_Throws()
    {
        DoublyLinkedListQueue<int> queue = new DoublyLinkedListQueue<int>();
        Assert.That(queue.Size, Is.EqualTo(0));
        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    [TestCase(1, 20)]
    [TestCase(2, 0)]
    [TestCase(10, 1)]
    public void Dequeue_WorksCorrectly(int enqueueBeforeTarget, int engueueAfterTarget)
    {
        DoublyLinkedListQueue<int> queue = GetRandomFilledQueue(enqueueBeforeTarget);

        int randomNumToEnqueue = Random.Shared.Next();
        queue.Enqueue(randomNumToEnqueue);

        for (int i = 0; i < engueueAfterTarget; i++)
        {
            queue.Enqueue(Random.Shared.Next());
        }

        for (int i = 0; i < enqueueBeforeTarget; i++)
        {
            queue.Dequeue();
        }

        Assert.That(queue.Dequeue(), Is.EqualTo(randomNumToEnqueue));
    }

    [Test]
    public void Enqueue_AfterDequeuedToZeroElements_WorksCorrectly()
    {
        int numsToEnqueue = Random.Shared.Next(100);
        DoublyLinkedListQueue<int> queue = GetRandomFilledQueue(numsToEnqueue);
        for (int i = 0; i < numsToEnqueue; i++)
        {
            queue.Dequeue();
        }

        int numToEnqueueAfterAllDequeued = Random.Shared.Next();
        queue.Enqueue(numToEnqueueAfterAllDequeued);

        Assert.That(queue.Size, Is.EqualTo(1));
        Assert.That(queue.Dequeue(), Is.EqualTo(numToEnqueueAfterAllDequeued));
    }

    [Test]
    public void Peek_WhenEmpty_Throws()
    {
        DoublyLinkedListQueue<int> queue = new DoublyLinkedListQueue<int>();
        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    [Test]
    public void Peek_WorksCorrectly()
    {
        int firstNumToEnqueue = Random.Shared.Next(100);
        DoublyLinkedListQueue<int> queue = new DoublyLinkedListQueue<int>();
        queue.Enqueue(firstNumToEnqueue);

        int numberOfElements = Random.Shared.Next(100);
        for (int i = 0; i < numberOfElements; i++)
        {
            queue.Enqueue(Random.Shared.Next());
        }
        
        int sizePriorToPeek = queue.Size;
        Assert.That(queue.Peek(), Is.EqualTo(firstNumToEnqueue));
        Assert.That(queue.Size, Is.EqualTo(sizePriorToPeek));
    }

    [Test]
    public void IsEmpty_WhenEmpty_ReturnsTrue()
    {
        DoublyLinkedListQueue<int> queue = new DoublyLinkedListQueue<int>();
        Assert.That(queue.IsEmpty(), Is.True);
    }

    [Test]
    public void IsEmpty_WhenNotEmpty_ReturnsFalse()
    {
        int numOfPushes = Random.Shared.Next(maxValue: 100);
        DoublyLinkedListQueue<int> queue = GetRandomFilledQueue(); 
        Assert.That(queue.IsEmpty(), Is.False);
    }

    [Test]
    public void Contains_WhenElementIsntContained_ReturnsFalse()
    {
        DoublyLinkedListQueue<int> queue = new DoublyLinkedListQueue<int>();
        Assert.That(queue.Contains(Random.Shared.Next()), Is.False);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void Contains_WorksCorrectly(int elementsEnqueued)
    {
        DoublyLinkedListQueue<int> queue = GetRandomFilledQueue(elementsEnqueued);
        int enqueued = Random.Shared.Next();
        queue.Enqueue(enqueued);
        
        for (int i = 0; i < elementsEnqueued; i++)
        {
            queue.Enqueue(Random.Shared.Next());
        }

        Assert.That(queue.Contains(enqueued), Is.True);
    }

    private static DoublyLinkedListQueue<int> GetRandomFilledQueue(int numberOfElements = 100)
    {
        DoublyLinkedListQueue<int> queue = new DoublyLinkedListQueue<int>();
        for (int i = 0; i < numberOfElements; i++)
        {
            queue.Enqueue(Random.Shared.Next());
        }

        return queue;
    }
}
