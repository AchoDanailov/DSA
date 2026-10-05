# Task description

You are given a matrix (2D array) of lowercase alphanumeric characters (a-z,
0-9), a starting position – defined by a start row startRow and a start column
startCol – and a filling symbol fillChar. Let’s call the symbol originally at
startRow and startCol the startChar. Write a program, which, starting from the
symbol at startRow and startCol, changes to fillChar every symbol in the matrix
which: 
- is equal to startChar AND 
- can be reached from startChar by going up (row – 1), down (row + 1), left
(col – 1) and right (col + 1) and “stepping” ONLY on symbols equal
startChar  

So, you basically start from startRow and startCol and can move either by
changing the row OR column (not both at once, i.e. you can’t go diagonally) by
1, and can only go to positions which have the startChar written on them. Once
you find all those positions, you change them to fillChar.  

In other words, you need to implement something like the Fill tool in MS Paint,
but for a 2D char array instead of a bitmap. 

Study the code inside TheMatrix class and the tests. Implement the two methods:
solve () and toOutputString ().  

---

## Input 

There are two classes for this problem TheMatrix and TheMatrixTest .You have to
study the code the input is passed upon creation of TheMatrix object inside the
tests. 

Two integers will be entered – the number R of rows and number C of columns. 

On each of the next R lines, C characters separated by single spaces will be
entered – the symbols of the Rth row of the matrix, starting from the 0th
column and ending at the C-1 column. 

On the next line, a single character – the fillChar – will be entered. 

On the last line, two integers – startRow and startCol – separated by a single
space, will be entered. 

---

## Output 

Implement the toOutputString (). 

The output should consist of R lines, each consisting of exactly C characters,
NOT SEPARATED by spaces, representing the matrix after the fill operation has
been finished. 

---

## Constraints 

0 < R, C < 20  0 <= startRow < R  0 <= startCol < C 

All symbols in the input matrix will be lowercase alphanumerics (a-z, 0-9). The
fillChar will also be alphanumeric and lowercase. 

---

## Examples 

| # | Input | Output |
|---|-------|--------|
| 1 | `5 3`<br>`a a a`<br>`a a a`<br>`a b a`<br>`a b a`<br>`a b a`<br>`x`<br>`0 0` | `xxx`<br>`xxx`<br>`xbx`<br>`xbx`<br>`xbx` |
| 2 | `5 3`<br>`a a a`<br>`a a a`<br>`a b a`<br>`a b a`<br>`a b a`<br>`x`<br>`2 1` | `aaa`<br>`aaa`<br>`axa`<br>`axa`<br>`axa` |
| 3 | `5 6`<br>`o o 1 1 o o`<br>`o 1 o o 1 o`<br>`1 o o o o 1`<br>`o 1 o o 1 o`<br>`o o 1 1 o o`<br>`3`<br>`2 1` | `oo11oo`<br>`o1331o`<br>`133331`<br>`o1331o`<br>`oo11oo` |
| 4 | `5 6`<br>`o o o o o o`<br>`o o o 1 o o`<br>`o o 1 o 1 1`<br>`o 1 1 w 1 o`<br>`1 o o o o o`<br>`z`<br>`4 1` | `oooooo`<br>`ooo1oo`<br>`oo1o11`<br>`o11w1z`<br>`1zzzzz` |
| 5 | `5 6`<br>`o 1 o o 1 o`<br>`o 1 o o 1 o`<br>`o 1 1 1 1 o`<br>`o 1 o w 1 o`<br>`o o o o o o`<br>`z`<br>`4 0` | `z1oo1z`<br>`z1oo1z`<br>`z1111z`<br>`z1zw1z`<br>`zzzzzz` |