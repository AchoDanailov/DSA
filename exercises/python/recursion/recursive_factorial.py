# Write a program that calculates the recursively factorial of a given number. 

def get_factorial(num: int) -> int:
    if (num == 1):
        return 1;
    res: int = num * get_factorial(num - 1);
    return res

num: int = int(input())
res: int = get_factorial(num)
print(res)