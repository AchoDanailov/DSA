# Write a program that finds the sum of all elements in an integer array. Use recursion. 

def get_sum(arr: list[int], index: int) -> int:
    if index == len(arr):
        return 0
    return arr[index] + get_sum(arr, index + 1);

arr: list[int] = [int(x) for x in input().split()]
recurs_sum: int = get_sum(arr, 0)
print(recurs_sum)