import heapq
from collections import Counter

class Solution:
    # NOTE: 
    # 1. Counter is a items frequency counter => item to count hashmap 
    # 2. counter.most_common(n) internals:
    # ```
    # import heapq
    # return heapq.nlargest(n, self.items(), key=_itemgetter(1))
    # ```
    # 3. Time complexity is O(n log(k))
    def topKFrequent(self, nums: list[int], k: int) -> list[int]:
        counter: Counter[int] = Counter[int](nums)
        res: list[int] = [x for (x, _) in counter.most_common(k)]
        return res

    # NOTE:
    # 1. Tuples comparing sequence:
    # Compare first elements in each tuple;
    #     if not equal return result from 1
    #     if equal
    #         compare second elements in each tuple and return that result
    #
    # 2. Time complexity is O(n log(k)):
    # - Mapping num to freq - O(n).
    # - heap_push, heap_pop - O(log k) Because we keep the heap at no more than k elements,
    # the heapify(sift) operation used inside push and pop finishes in at most log(k) steps.
    def topKFrequent_manualHeapQSolution(self, nums: list[int], k: int) -> list[int]:
        num_by_count_map: dict[int, int] = dict[int, int]()
        for x in nums:
            if x not in num_by_count_map:
                num_by_count_map[x] = 0
            num_by_count_map[x] += 1

        min_heap: list[tuple[int, int]] = list[tuple[int, int]]()
        for num in num_by_count_map:
            heapq.heappush(min_heap, (num_by_count_map[num], num))
            if len(min_heap) > k:
                heapq.heappop(min_heap)

        res: list[int] = [num for _, num in min_heap]
        return res

# ---

nums: list[int] = [1, 1, 1, 2, 2, 3]
k: int = 2

result: list[int] = Solution().topKFrequent_manualHeapQSolution(nums, k)
print(result)
