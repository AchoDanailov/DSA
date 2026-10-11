# NOTE: An anagram is a word or phrase formed by rearranging the letters of a
#different word or phrase, using all the original letters exactly once.
class Solution:
    def isAnagram(self, s: str, t: str) -> bool:
        if len(s) != len(t):
            return False

        char_by_count_map: dict[str, int] = dict()
        for x in s:
            if x not in char_by_count_map:
                char_by_count_map[x] = 1
            else:
                char_by_count_map[x] += 1

        for x in t:
            if x in char_by_count_map and char_by_count_map[x] > 0:
                char_by_count_map[x] -= 1
                continue

            return False

        return True


(s1, s2) = ("ab", "a")
# (s1, s2) = ("anagram", "nagaram")
# (s1, s2) = ("rat", "car")
res: bool = Solution().isAnagram(s1, s2)
print(res)