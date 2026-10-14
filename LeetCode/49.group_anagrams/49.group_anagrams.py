# k = each str len
# O(n * k * log(k))
class Solution:
    def groupAnagrams(self, strs: list[str]) -> list[list[str]]:
        sorted_to_orig_map: dict[str, list[str]] = dict[str, list[str]]()
        while len(strs) > 0:
            curr: str = strs.pop()
            sorted_str: str = "".join(sorted(curr))

            if sorted_str not in sorted_to_orig_map:
                sorted_to_orig_map[sorted_str] = list[str]()

            sorted_to_orig_map[sorted_str].append(curr)

        res: list[list[str]] = list[list[str]]()
        for key in sorted_to_orig_map:
            res.append(sorted_to_orig_map[key])

        return res


str_arr: list[str] = ["eat","tea","tan","ate","nat","bat"]
# str_arr: list[str] = [""]

res: list[list[str]] = Solution().groupAnagrams(str_arr)
print(res)

# ---

# A working but really slow solution
# k = each str len
# O(n*n*k)

# from collections import deque

# class Solution:
#     def groupAnagrams(self, strs: list[str]) -> list[list[str]]:
#         queue: deque[str] = deque(strs)
#         res: list[list[str]] = list[list[str]]()

#         while len(queue) > 0:
#             curr_str: str = queue.pop()
#             curr_word_chars_to_count_map: dict[str, int] = self._get_chars_to_count_map(curr_str)

#             processed: set[str] = set()
#             curr_list: list[str] = [ curr_str ]
#             length: int = len(queue)
#             while length > 0:
#                 next_str: str = queue.pop()
#                 if next_str in processed:
#                     queue.appendleft(next_str)
#                     length -= 1
#                     continue

#                 chars_to_count_map: dict[str, int] = self._get_chars_to_count_map(next_str)

#                 are_anagrams: bool = self._maps_equal(curr_word_chars_to_count_map, chars_to_count_map)
#                 if are_anagrams:
#                     curr_list.append(next_str)
#                 else:
#                     queue.appendleft(next_str)

#                 processed.add(next_str)
#                 length -= 1
            
#             processed.clear()
#             res.append(curr_list)

#         return res


#     def _get_chars_to_count_map(self, curr_str: str) -> dict[str, int]:
#         chars_to_count_map: dict[str, int] = dict()
#         for x in curr_str:
#             if x not in chars_to_count_map:
#                 chars_to_count_map[x] = 0

#             chars_to_count_map[x] += 1

#         return chars_to_count_map

#     def _maps_equal(self, first: dict[str, int], second: dict[str, int]) -> bool:
#         if len(first) != len(second):
#             return False

#         for x in first:
#             if x in second and first[x] == second[x]:
#                 continue
#             return False

#         return True