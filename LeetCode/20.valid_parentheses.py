from collections import deque

class Solution:
    _parenthesis_mapping: dict[str, str] = {
        "[": "]",
        "{": "}",
        "(": ")"
    }

    def isValid(self, s: str) -> bool:
        if len(s) % 2 != 0:
            return False

        closing_brackets_queue: deque[str] = deque[str]()
        for ch in s:
            if ch in self._parenthesis_mapping:
                closing_brackets_queue.append(self._parenthesis_mapping[ch])
            else:
                if len(closing_brackets_queue) == 0:
                    return False
                    
                curr_closing_bracket_in_line: str = closing_brackets_queue.pop()
                if ch != curr_closing_bracket_in_line:
                    return False
        
        if len(closing_brackets_queue) > 0:
            return False

        return True

# s: str = "()[]{}"
# s: str = "([])"
# s: str = "}"
s: str = "}("
res: bool = Solution().isValid(s)
print(res)