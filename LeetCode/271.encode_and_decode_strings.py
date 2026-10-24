# NOTE: The constraints of the task are crucial for this solution.
# 0 <= strs.length < 100
# 0 <= strs[i].length < 200
# strs[i] contains any possible characters out of 256 valid ASCII characters.

# Goal:
# Goal of encoding is storing metadata that will be needed in the decoding phase (it is expected the operations should be stateless)
# But each metadata can have use diff amount of chars in the end result string of the encoding operation. 
# Thats why each metadata starts the number of chars used to represent the metadata, and the constraints of the task help with keeping all of
# this numbers small.

# Algorithm:
# first_char = the count of the chars holding the number which value means the number of strings in the str (max chars 1)
# next {first_char} number of chars = hold the number of strings in the str (can be 1 char or 2 chars) = n
# next n times: (foreach string that should come out of the str once decoded)
#   char 1 = the count of the chars that hold the the value of how long is the curr string (max chars 1, possible values 1, 2 or 3)
#   the next {char 1} number of times:
#       are the digits of the number holding the value of how long is the current string = x
#   next x chars are the string itself.

# Examples:
# ["Hello", "World"] => "1215Hello15World"
# ["123","456","789","10","11","12","13","14","15","16","17","18","19","20"] => "21413123134561378912101211....."

class Solution:
    def encode(self, strs: list[str]) -> str:
        res_builder: list[str] = list[str]()

        strs_len: int = len(strs)
        strs_len_chars_count: int = len(f"{strs_len}")

        res_builder.append(f"{strs_len_chars_count}")
        res_builder.append(f"{strs_len}")

        for x in strs:
            curr_str_len: int = len(x)
            if curr_str_len <= 9:
                res_builder.append("1")
            elif curr_str_len > 9 and curr_str_len < 99:
                res_builder.append("2")
            else:
                res_builder.append("3")

            res_builder.append(f"{curr_str_len}")
            res_builder.append(x)

        res: str = "".join(res_builder)
        return res
        

    def decode(self, s: str) -> list[str]:
        if len(s) < 3:
            return []

        index: int = 0
        num_of_strings_chars_count: int = int(s[index])
        index += 1

        num_of_strings_as_str: list[str] = list[str]()
        for _ in range(num_of_strings_chars_count):
            num_of_strings_as_str.append(s[index])
            index += 1

        num_of_strings: int = int("".join(num_of_strings_as_str))
            
        res: list[str] = list[str]()
        for _ in range(num_of_strings):
            str_len_number_chars_number: int = int(s[index])
            index += 1

            str_len_chars: list[str] = list[str]()
            for _ in range(str_len_number_chars_number):
                str_len_chars.append(s[index])
                index += 1  

            curr_str_builder: list[str] = list[str]()
            str_len: int = int("".join(str_len_chars))
            for _ in range(str_len):
                curr_str_builder.append(s[index])
                index += 1

            curr_word: str = "".join(curr_str_builder)
            res.append(curr_word)
        
        return res


# strs: list[str] = ["Hello", "World"]
strs=["123","456","789","10","11","12","13","14","15","16","17","18","19","20"]

solution: Solution = Solution();
encoded_string: str = solution.encode(strs);

decoded_strs: list[str] = solution.decode(encoded_string);
print(decoded_strs)