using DSA.TreeDataStructures.BinarySearchTree;
using DSA.TreeDataStructures.Tests.Utils;
using DSA.TreeDataStructures.Interfaces;

namespace DSA.TreeDataStructures.Tests;

[TestFixture]
public class BinarySearchTreeTests
{
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(15)]
    public void Ctor_WithRootValue_WorksCorrectly(int num)
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>(num);
        int sizeAfterAddingElFromCtor = bst.Size;

        int[] arr = ArrayHelpers
            .RandomFilledIntArray(length: num, isReacuranceAllowed: false)
            .Where(n => n != num)
            .ToArray();

        // Act
        Array.ForEach(arr, e => bst.Add(e));

        // Arrange
        Assert.That(sizeAfterAddingElFromCtor, Is.EqualTo(1));
        Assert.That(bst.Size, Is.EqualTo(arr.Length + 1));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(40)]
    public void Ctor_WithRootNode_WorksCorrectly(int num)
    {
        // Arrange
        IBSTNode<int> node = new BSTNode<int>(num);
        BinarySearchTree<int> bst = new BinarySearchTree<int>(node);
        int sizeAfterAddingElFromCtor = bst.Size;

        int[] arr = ArrayHelpers
            .RandomFilledIntArray(length: num, isReacuranceAllowed: false)
            .Where(n => n != num)
            .ToArray();

        // Act
        Array.ForEach(arr, e => bst.Add(e));

        // Assert
        Assert.That(bst.Size, Is.EqualTo(arr.Length + 1));
        Assert.That(sizeAfterAddingElFromCtor, Is.EqualTo(1));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(40)]
    public void Add_WorksCorrectly(int size)
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        int[] arr = ArrayHelpers.RandomFilledIntArray(length: size, isReacuranceAllowed: false);

        // Act
        Array.ForEach(arr, e => bst.Add(e));

        // Assert
        Assert.That(bst.Size, Is.EqualTo(arr.Length));
    }

    [Test]
    public void Search_WhenEmptyTree_ShouldThrow()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        Assert.Throws<InvalidOperationException>(() => bst.Search(Random.Shared.Next()));
    }

    [Test]
    public void Search_WhenElementIsNotContained_ShouldReturnNull()
    {
        // Arrange
        (_, BinarySearchTree<int> bst) = GetAFilledTree();

        // Act
        IBSTNode<int>? searchRes = bst.Search(Random.Shared.Next(minValue: 51, maxValue: int.MaxValue));

        // Assert
        Assert.That(searchRes, Is.Null);
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    [TestCase(40)]
    public void Search_ShouldWorkCorrectly(int treeSize)
    {
        // Arrange
        (int[] arr, BinarySearchTree<int> bst) = GetAFilledTree(treeSize);
        int targetIndex = Random.Shared.Next(minValue: 0, maxValue: treeSize);
        int targetNum = arr[targetIndex];

        // Act
        IBSTNode<int>? searchRes = bst.Search(targetNum);

        // Assert
        Assert.That(bst.Size, Is.EqualTo(treeSize));
        Assert.That(searchRes, Is.Not.Null);
        Assert.That(searchRes.Value, Is.EqualTo(targetNum));
    }

    [Test]
    public void Remove_WhenTreeIsEmpty_ShouldThrow()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        Assert.Throws<InvalidOperationException>(() => bst.Remove(Random.Shared.Next()));
    }

    [Test]
    public void Remove_WhenNotFound_ShouldReturnNull()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: 50,
            isReacuranceAllowed: false,
            randomnessLowerThreshold: 1,
            randomnessUpperThreshold: 100);
        Array.ForEach(arr, e => bst.Add(e));

        // Act
        IBSTNode<int>? removeRes = bst
            .Remove(Random.Shared.Next(minValue: 101, maxValue: int.MaxValue));

        // Assert
        Assert.That(removeRes, Is.Null);
    }

    [Test]
    public void Remove_ShouldWorkCorrectly()
    {
        // Arrange
        (int[] arr, BinarySearchTree<int> tree) = GetAFilledTree(50);

        int targetIndex = Random.Shared.Next(minValue: 0, maxValue: arr.Length);
        int targetElement = arr[targetIndex];
        bool isTargetContainedPriorToCallingRemove = tree.Contains(targetElement);

        // Act
        IBSTNode<int>? target = tree.Remove(targetElement);
        
        // Assert
        Assert.That(isTargetContainedPriorToCallingRemove, Is.True);
        Assert.That(target, Is.Not.Null);
        Assert.That(tree.Search(targetElement), Is.Null);

        bool allElsArePreservedAfterTargetRemoval = arr
            .Where(e => e != targetElement)
            .All(e => tree.Contains(e));
        Assert.That(allElsArePreservedAfterTargetRemoval, Is.True);
    }

    [TestCase(50)]
    [TestCase(30)]
    [TestCase(20)]
    [TestCase(70)]
    public void Remove_FixedTree_KeepsOtherElements(int target)
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        int[] values = new int[] { 50, 30, 70, 20, 40, 60, 80, 75 };
        Array.ForEach(values, e => bst.Add(e));

        // Act
        bst.Remove(target);

        // Assert
        Assert.That(bst.Contains(target), Is.False);
        Assert.That(bst.Size, Is.EqualTo(values.Length - 1));
        Assert.That(values.Where(v => v != target).All(bst.Contains), Is.True);
    }

    [Test]
    public void Remove_TwiceInRow_KeepsTreeValid()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        int[] values = new int[] { 50, 30, 70, 20, 40, 60, 80, 65 };
        Array.ForEach(values, bst.Add);

        // Act
        bst.Remove(50);
        bst.Remove(65);

        // Assert
        Assert.That(bst.Contains(65), Is.False);
        Assert.That(bst.Size, Is.EqualTo(6));
        int[] expectedInOrderRes = new int[] { 20, 30, 40, 60, 70, 80 };
        Assert.That(bst.GetAllValueInOrder(), Is.EqualTo(expectedInOrderRes));
    }

    [Test]
    public void Contains_WhenElementNotContained_ReturnsFalse()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        Assert.That(bst.Contains(Random.Shared.Next()), Is.False);
    }

    [Test]
    public void Contains_WhenElementIsContained_ReturnsTrue()
    {
        // Arrange
        (int[] arr, BinarySearchTree<int> tree) = GetAFilledTree(50);
        int targetIndex = Random.Shared.Next(minValue: 0, maxValue: arr.Length);
        int targetElement = arr[targetIndex];

        // Act
        bool res = tree.Contains(targetElement);

        // Assert
        Assert.That(res, Is.True);
    }

    private ValueTuple<int[], BinarySearchTree<int>> GetAFilledTree(int size = 50)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        int[] arr = ArrayHelpers.RandomFilledIntArray(
            length: size,
            isReacuranceAllowed: false,
            randomnessLowerThreshold: 1,
            randomnessUpperThreshold: 100);
        Array.ForEach(arr, e => bst.Add(e));

        return (arr, bst);
    }
}