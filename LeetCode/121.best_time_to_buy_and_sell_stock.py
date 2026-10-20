import heapq

# NOTE: There is an easier solution to this..
class Solution:
    def maxProfit(self, prices: list[int]) -> int:
        if len(prices) <= 1:
            return 0

        closed_cycles_max_heap: list[int] = list[int]()
        processing_max_heap: list[int] = list[int]()
        curr_cycle_buy_point: int = prices[0]

        for i, x in enumerate(prices):
            if i == 0:
                heapq.heappush(processing_max_heap, x * -1)
                continue

            if x >= curr_cycle_buy_point:
                heapq.heappush(processing_max_heap, x * -1)
                continue

            # note if we get to here in the loop it means x < curr_cycle_buy_point and we need to start a new cycle
            last_cycle_result: int = heapq.heappop(processing_max_heap) * -1 - curr_cycle_buy_point
            processing_max_heap.clear()
            
            curr_cycle_buy_point = x
            heapq.heappush(closed_cycles_max_heap, last_cycle_result * -1)
            heapq.heappush(processing_max_heap, x * -1)
                
        highest_price_in_curr_cycle: int = heapq.heappop(processing_max_heap) * -1
        current_cycle_result: int = highest_price_in_curr_cycle - curr_cycle_buy_point
        heapq.heappush(closed_cycles_max_heap, current_cycle_result * -1)

        result: int = heapq.heappop(closed_cycles_max_heap) * -1
        return result


# prices: list[int] = [2,4,1]
# prices: list[int] = [7, 1, 5, 3, 6, 4]
# prices: list[int] = [1, 2]
prices: list[int] = [3,2,6,5,0,3]
result: int = Solution().maxProfit(prices)
print(result)