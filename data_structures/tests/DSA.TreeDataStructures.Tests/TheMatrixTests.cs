using DSA.TreeDataStructures.Exercises.Matrix;

namespace DSA.TreeDataStructures.Tests;

[TestFixture]
public class TheMatrixTests
{
    [Test]
    public void TestZeroTestOne()
    {
        char[][] matrix =
        {
            new[] { 'a', 'a', 'a' },
            new[] { 'a', 'a', 'a' },
            new[] { 'a', 'b', 'a' },
            new[] { 'a', 'b', 'a' },
            new[] { 'a', 'b', 'a' }
        };
        char fillChar = 'x';
        int startRow = 0;
        int startCol = 0;

        var theMatrix = new TheMatrix(matrix, fillChar, startRow, startCol);
        theMatrix.SolveDfs();

        string str = theMatrix.ToOutputString();
        Assert.That(str, Is.EqualTo(
                    "xxx\n" + 
                    "xxx\n" + 
                    "xbx\n" + 
                    "xbx\n" + 
                    "xbx\n"));
    }

    [Test]
    public void TestZeroTestTwo()
    {
        char[][] matrix =
        {
            new[] { 'a', 'a', 'a' },
            new[] { 'a', 'a', 'a' },
            new[] { 'a', 'b', 'a' },
            new[] { 'a', 'b', 'a' },
            new[] { 'a', 'b', 'a' }
        };
        char fillChar = 'x';
        int startRow = 2;
        int startCol = 1;

        var theMatrix = new TheMatrix(matrix, fillChar, startRow, startCol);
        theMatrix.SolveDfs();

        string str = theMatrix.ToOutputString();
        Assert.That(str, Is.EqualTo(
                    "aaa\n" + 
                    "aaa\n" + 
                    "axa\n" + 
                    "axa\n" + 
                    "axa\n"));
    }

    [Test]
    public void TestZeroTestThree()
    {
        char[][] matrix =
        {
            new[] { 'o', 'o', '1', '1', 'o', 'o' },
            new[] { 'o', '1', 'o', 'o', '1', 'o' },
            new[] { '1', 'o', 'o', 'o', 'o', '1' },
            new[] { 'o', '1', 'o', 'o', '1', 'o' },
            new[] { 'o', 'o', '1', '1', 'o', 'o' }
        };
        char fillChar = '3';
        int startRow = 2;
        int startCol = 1;

        var theMatrix = new TheMatrix(matrix, fillChar, startRow, startCol);
        theMatrix.SolveDfs();

        string str = theMatrix.ToOutputString();
        Assert.That(str, Is.EqualTo(
                    "oo11oo\n" + 
                    "o1331o\n" + 
                    "133331\n" + 
                    "o1331o\n" + 
                    "oo11oo\n"));
    }

    [Test]
    public void TestZeroTestFour()
    {
        char[][] matrix =
        {
            new[] { 'o', 'o', 'o', 'o', 'o', 'o' },
            new[] { 'o', 'o', 'o', '1', 'o', 'o' },
            new[] { 'o', 'o', '1', 'o', '1', '1' },
            new[] { 'o', '1', '1', 'w', '1', 'o' },
            new[] { '1', 'o', 'o', 'o', 'o', 'o' }
        };
        char fillChar = 'z';
        int startRow = 4;
        int startCol = 1;

        var theMatrix = new TheMatrix(matrix, fillChar, startRow, startCol);
        theMatrix.SolveDfs();

        string str = theMatrix.ToOutputString();
        Assert.That(str, Is.EqualTo(
                    "oooooo\n" + 
                    "ooo1oo\n" +
                    "oo1o11\n" +
                    "o11w1z\n" +
                    "1zzzzz\n"));
    }

    [Test]
    public void TestZeroTestFive()
    {
        char[][] matrix =
        {
            new[] { 'o', '1', 'o', 'o', '1', 'o' },
            new[] { 'o', '1', 'o', 'o', '1', 'o' },
            new[] { 'o', '1', '1', '1', '1', 'o' },
            new[] { 'o', '1', 'o', 'w', '1', 'o' },
            new[] { 'o', 'o', 'o', 'o', 'o', 'o' }
        };
        char fillChar = 'z';
        int startRow = 4;
        int startCol = 0;

        var theMatrix = new TheMatrix(matrix, fillChar, startRow, startCol);
        theMatrix.SolveDfs();

        string str = theMatrix.ToOutputString();
        Assert.That(str, Is.EqualTo(
                    "z1oo1z\n" + 
                    "z1oo1z\n" + 
                    "z1111z\n" + 
                    "z1zw1z\n" + 
                    "zzzzzz\n"));
    }
}