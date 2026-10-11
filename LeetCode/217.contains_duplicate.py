class Solution:
    def containsDuplicate(self, nums: list[int]) -> bool:
        passed: set[int] = set()
        for x in nums:
            if x in passed: 
                return True

            passed.add(x)

        return False

nums: list[int] = [1,2,3,1]
# nums: list[int] = [1,2,3,4]
# nums: list[int] = [1,1,1,3,3,4,3,2,4,2]
res: bool = Solution().containsDuplicate(nums)
print(res)