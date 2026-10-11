from collections import deque

# Definition for singly-linked list.
class ListNode:
    def __init__(self, val=0, next=None):  # type: ignore
        self.val = val
        self.next = next # type: ignore

class Solution:
    def addTwoNumbers(self, l1: ListNode | None, l2: ListNode | None) -> ListNode | None:
        if l1 is None or l2 is None:
            raise Exception("Invalid arguments", "l1 or l2 can not be None")

        digits_deque: deque[int] = deque()
        second_num_length: int = self.traverse_and_collect_digits(l1, l2, digits_deque) 

        second_num: int = int("".join(self.pop_digits(digits_deque, second_num_length)))
        first_num: int = int("".join(self.pop_digits(digits_deque)))
        resulting_num: int = first_num + second_num

        res: ListNode = self.build_res(resulting_num)
        return res;

    def traverse_and_collect_digits(self, l1: ListNode, l2: ListNode, digits: deque[int]) -> int:
        second_num_length: int = 0
        current: ListNode | None = l1

        while current is not None: # type: ignore
            digits.append(f"{current.val}") # type: ignore
            current = current.next # type: ignore

        current = l2
        while current is not None:
            digits.append(f"{current.val}") # type: ignore
            second_num_length += 1
            current = current.next # type: ignore

        return second_num_length

    def pop_digits(self, digits_deque: deque[int], length: int | None = None) -> list[str]:
        number_digits: list[str] = []

        while len(digits_deque) > 0:
            if length is not None and length == 0:
                break
                
            number_digits.append(str(digits_deque.pop()))

            if length is not None:
                length -= 1

        return number_digits

    def build_res(self, resulting_num: int) -> ListNode:
        digits: deque[int] = deque([int(n) for n in str(resulting_num)])

        first: ListNode = ListNode(digits.pop())
        current: ListNode = first
        while len(digits) > 0:
            current.next = ListNode(digits.pop())
            current = current.next

        return first


# l1: ListNode = ListNode(9, ListNode(9, ListNode(9, ListNode(9, ListNode(9, ListNode(9, ListNode(9)))))))
# l2: ListNode = ListNode(9, ListNode(9, ListNode(9, ListNode(9))))

l1: ListNode = ListNode(2, ListNode(4, ListNode(9)))
l2: ListNode = ListNode(5, ListNode(6, ListNode(4, ListNode(9))))

res: ListNode | None = Solution().addTwoNumbers(l1, l2)
if res is not None:
    current: ListNode | None = res
    while current is not None:
        print(current.val) # type: ignore
        current = current.next # type: ignore