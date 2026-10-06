def two_sum_complexity_n_square(nums: list[int], target: int) -> list[int]:
    if len(nums) < 2:
        return []

    for i in range(len(nums) - 1):
        for j in range(i + 1, len(nums)):
            if nums[i] + nums[j] == target:
                return [i, j]

    return []

# NOTE: I got spoiled there is a O(n) solution with HashMap still wrote it myself
def two_sum_complexity_n(nums: list[int], target: int) -> list[int]:
    if len(nums) < 2:
        return []

    res: list[int] = []
    value_index_map: dict[int, int] = { n: i for i, n in enumerate(nums) }

    for idx, num in enumerate(nums):
        value_to_reach_target: int = target - num
        if value_to_reach_target in value_index_map and value_index_map[value_to_reach_target] != idx:
            res.append(idx)
            res.append(value_index_map[value_to_reach_target])
            break

    return res

arr: list[int] = [ 5, 3, 7, 8, 9, 4, 2 ] 
target: int = 6
# print(two_sum_complexity_n_square(arr, target));
print(two_sum_complexity_n(arr, target))