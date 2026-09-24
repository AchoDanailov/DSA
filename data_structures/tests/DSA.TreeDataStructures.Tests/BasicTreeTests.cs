using DSA.TreeDataStructures;
using DSA.TreeDataStructures.Interfaces;
using DSA.TreeDataStructures.Tests.Utils;

namespace DSA.TreeDataStructures.Tests;

[TestFixture]
public class BasicTreeTests
{
    [TestCase (0)]
    [TestCase (1)]
    [TestCase (555)]
    public void Ctor_WithOneNode_WorksCorrectly(int num)
    {
        // Arrange & Act
        BasicTree<int> tree = new BasicTree<int>(num);
        
        // Assert
        Assert.That(tree.GetOrderBFS(), Does.Contain(num));
    }

    [Test]
    public void Ctor_WithMultipleTreeNodes_WorksCorrectly()
    {
        // Arrange & Act
        (int[] initialNums, BasicTree<int> tree) = SetUpTreeWithFiveNodes();
        int[] numsFromBfs = tree.GetOrderBFS().ToArray();
        
        // Assert
        Assert.That(numsFromBfs[0], Is.EqualTo(initialNums[0]));
        Assert.That(numsFromBfs[1], Is.EqualTo(initialNums[1]));
        Assert.That(numsFromBfs[2], Is.EqualTo(initialNums[4]));
        Assert.That(numsFromBfs[3], Is.EqualTo(initialNums[2]));
        Assert.That(numsFromBfs[4], Is.EqualTo(initialNums[3]));
    }

    [Test]
    public void Dfs_WorksCorrectly()
    {
        // Arrange
        (int[] initialNums, BasicTree<int> tree) = SetUpTreeWithFiveNodes();
        
        // Act
        int[] numsFromDfs = tree.GetOrderDFS().ToArray();
        
        // Assert
        Assert.That(numsFromDfs[0], Is.EqualTo(initialNums[0]));
        Assert.That(numsFromDfs[1], Is.EqualTo(initialNums[1]));
        Assert.That(numsFromDfs[2], Is.EqualTo(initialNums[2]));
        Assert.That(numsFromDfs[3], Is.EqualTo(initialNums[3]));
        Assert.That(numsFromDfs[4], Is.EqualTo(initialNums[4]));
    }

    [Test]
    public void AddChild_WhenTargetNotFound_ShouldReturnFalse()
    {
        // Arrange
        int largestPossibleNumContainedInTree = 50;
        (_, BasicTree<int> tree) = SetUpTreeWithFiveNodes(largestPossibleNumContainedInTree);
        BasicTree<int> treeToAdd = new BasicTree<int>(Random.Shared.Next());
        
        // Act
        bool addRes = tree.AddChild(
            newTree: treeToAdd,
            targetValue: Random.Shared.Next(minValue: largestPossibleNumContainedInTree + 1, maxValue: int.MaxValue)); 

        // Assert
        Assert.That(addRes, Is.False);
    }

    [Test]
    public void AddChild_ShoudldWorkCorrectly()
    {
        // Arrange
        int[] initialNums = new int[] { 7, 10, 15, 11, 20 };
        BasicTree<int> tree = new BasicTree<int>(initialNums[0], 
            new BasicTree<int>(initialNums[1],
                new BasicTree<int>(initialNums[2],
                    new BasicTree<int>(initialNums[3]))),
            new BasicTree<int>(initialNums[4]));

        int valueToInsert = Random.Shared.Next();
        BasicTree<int> treeNodeToInsert = new BasicTree<int>(valueToInsert);

        // Act & Assert
        Assert.That(tree.AddChild(treeNodeToInsert, 10), Is.True);
        Assert.That(tree.GetOrderBFS().ToArray()[4], Is.EqualTo(valueToInsert));
    }

    [Test]
    public void AddChild_WithTreeWithChildren_ShouldWorkCorrectly()
    {
        // Arrange
        int[] initialNums = new int[] { 7, 10, 15, 11, 20 };
        BasicTree<int> tree = new BasicTree<int>(initialNums[0], 
            new BasicTree<int>(initialNums[1],
                new BasicTree<int>(initialNums[2],
                    new BasicTree<int>(initialNums[3]))),
            new BasicTree<int>(initialNums[4]));

        int[] numsToAdd = new int[] { 5, 58, 88, 8 };
        BasicTree<int> treeToAdd = new BasicTree<int>(numsToAdd[0], 
            new BasicTree<int>(numsToAdd[1],
                new BasicTree<int>(numsToAdd[2],
                    new BasicTree<int>(numsToAdd[3]))));

        int treeNodeUnderWhichToAddNewTreeNode 
            = Random.Shared.Next(minValue: 0, maxValue: initialNums.Length - 1);
        int nodeThatHasBeenAdded 
            = Random.Shared.Next(minValue: 0, maxValue: numsToAdd.Length - 1);

        // Act & Assert
        Assert.That(tree.AddChild(treeToAdd, initialNums[treeNodeUnderWhichToAddNewTreeNode]), Is.True);
        Assert.That(tree.Contains(numsToAdd[nodeThatHasBeenAdded]), Is.True);
    }

    [Test]
    public void Contains_WhenTargetNotFound_ShouldReturnFalse()
    {
        // Arrange
        int largestPossibleNumContainedInTree = 50;
        (_, BasicTree<int> tree) = SetUpTreeWithFiveNodes(largestPossibleNumContainedInTree);

        // Act
        bool contains = tree.Contains(targetValue: Random.Shared.Next(
            minValue: largestPossibleNumContainedInTree + 1,
            maxValue: int.MaxValue));
        
        // Assert
        Assert.That(contains, Is.False);
    }

    [Test]
    public void Remove_WhenTargetNotFound_ShouldReturnEmpty()
    {
        // Arrange
        int largestPossibleNumContainedInTree = 50;
        (_, BasicTree<int> tree) = SetUpTreeWithFiveNodes(largestPossibleNumContainedInTree);

        // Act
        IBasicTree<int> returned = tree.Remove(targetValue: Random.Shared.Next(
            minValue: largestPossibleNumContainedInTree + 1,
            maxValue: int.MaxValue));
        
        // Assert
        Assert.That(returned.Value, Is.EqualTo((int) default!));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void Remove_WorksCorrectly(int targetIndex)
    {
        // Arrange
        (int[] initialNums, BasicTree<int> tree) = SetUpTreeWithFiveNodes();

        // Act
        IBasicTree<int> removed = tree.Remove(initialNums[targetIndex]);

        // Assert
        if (targetIndex != 0)
        {
            Assert.That(removed.Value, Is.EqualTo(initialNums[targetIndex]));
            Assert.That(tree.Contains(removed.Value), Is.False);
        }
        else
        {
            // NOTE: Can not have count = 0 since the last node left is the root tree object, all we can do is make Value nullable, and ensure it doesnt break all other operations!
            Assert.That(tree.GetOrderBFS().Count(), Is.EqualTo(1));
        }
    }

    [Test]
    public void Remove_BiggerTreeNode_WorksCorrectly()
    {
        // Arrange
        (int[] initialNums, BasicTree<int> tree) = SetUpTreeWithFiveNodes();
        
        // Act
        IBasicTree<int> removed = tree.Remove(initialNums[1]);
        
        // Assert
        int[] targetWithChildren = removed.GetOrderDFS().ToArray();
        
        Assert.That(targetWithChildren.Length, Is.Not.EqualTo(0));
        Assert.That(tree.Contains(removed.Value), Is.False);
        Assert.That(removed.Value, Is.EqualTo(initialNums[1]));
        Assert.That(targetWithChildren[1], Is.EqualTo(initialNums[2]));
        Assert.That(targetWithChildren[2], Is.EqualTo(initialNums[3]));
    }

    private static ValueTuple<int[], BasicTree<int>> SetUpTreeWithFiveNodes(
        int randomnessUpperThreshold = 50)
    {
        int length = 5;
        int[] nums = ArrayHelpers.RandomFilledIntArray(
            length: length,
            isReacuranceAllowed: false,
            randomnessLowerThreshold: 1,
            randomnessUpperThreshold: 50);

        BasicTree<int> tree = new BasicTree<int>(nums[0], 
            new BasicTree<int>(nums[1],
                new BasicTree<int>(nums[2],
                    new BasicTree<int>(nums[3]))),
            new BasicTree<int>(nums[4]));

        return (nums, tree);
    }
}