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

def top_part(level: int) -> str:
    if (level == 0):
        return ""
    line: str = '\n' + FIRST_CHAR * level;
    return line + top_part(level - 1)

def bot_part(total_levels: int, curr_level: int) -> str:
    if (curr_level > total_levels):
        return ""
    line: str = '\n' + SECOND_CHAR * curr_level
    return line + bot_part(total_levels, curr_level + 1)

def build_res(level: int) -> str:
    if (level <= 0):
        raise Exception("Levels can not be 0 or less", f"value: {level}")
    first_line: str = FIRST_CHAR * level
    top: str = top_part(level=level - 1)
    bot: str = bot_part(level, 1)
    return first_line + top + bot

num: int = int(input())
res: str = build_res(num)
print(res)