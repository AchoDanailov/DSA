# Write a program that draws the figure below depending on n. 
# ```
# ***
# **
# *
# #
# ##
# ###
# ```

FIRST_CHAR: str = '*'
SECOND_CHAR: str = '#'

def build_res(n: int, res: list[str]) -> None:
    if n == 0:
        return

    res.append(FIRST_CHAR * n)
    build_res(n - 1, res)
    res.append(SECOND_CHAR * n)

res: list[str] = list[str]()
num: int = int(input())
build_res(num, res)
print("\n".join(res))

