# Phone Key-Press Decode

---

### Description

According to the [requirements](Requirement.pdf), the main task is to **implement** the mentioned **static method** `OldPhonePad`. This method takes a `string` input representing a sequence of key presses on an old mobile keypad (where a space indicates a **pause**) and returns the corresponding typed message.

---

## Implementation Details

---

The method is implemented in the class `OldPhoneKeyPressDecoder` inside the project `PhoneKeyPressDecode`.
This is a static method as mentioned in the requirement document, takes a string parameter and returns the decoded message as string.

### Solution approach:
I solved the problem by iterating through the given sequence of key presses until a `#` character is found. I used two pointers(`start` and `end`) to track consecutive presses of the same key and determined the corresponding character by counting the number of presses.

#### **Detailed explanation of the approach**, while iterating through the key press sequence:
Initially, both the `start` and `end` pointers are set to the first index of the sequence.

1. **Handling a pause in the key-press sequence(`space` character):**
   If `start` points to a space (`' '`), move both `start` and `end` to the next index since an independent pause does not affect the message.

2. **Handling a backspace(`*` key):** If `start` points to `'*'`, it indicates a backspace. If the typed message (stored in a `StringBuilder`) is not empty, remove the last character.

3. **Tracking consecutive key presses:** If `start` points to a digit and `end` also points to the same digit (either at the same or a different index), increment `end` by one to track repeated presses.

4. **Processing a pressed sequence:** If `start` points to a digit and `end` points a different character (`space`, `*`, `#`, or another digit), it means a continuous press sequence has ended at the previous index of the `end` pointer. Extract the corresponding character based on the number of presses and append it to the typed message (a `StringBuilder` in here).

5. **Handling the end of input (`#` key):** If `start` points to `'#'`, stop processing as it indicates the end of input (in old phones, pressing `#` sends the message).

* Return whatever characters are appended to the string builder.

#### **Helper Methods in the `OldPhoneKeyPressDecoder` Class:**

1. **`GetDecodedCharacter`**:
    - Takes a digit and the number of consecutive occurrences as parameters.
    - Returns the corresponding character based on the number of presses.

2. **`GetCharactersForDigit`**:
    - Takes a digit as a parameter and returns the set of characters assigned to that key on an old mobile keypad.
    - Special characters for key `1` were added **arbitrarily**, as the exact sequence may vary across different old phone models.

#### **Extension Methods in the `Extension` Class:**

- **`SafelyRemoveLast`** (for `StringBuilder`):
    - Removes the last added character from the `StringBuilder`, but only if it contains at least one character to avoid errors.

### **Time Complexity:**
The algorithm processes the key press sequence in a **single pass** using two pointers, resulting in an upper bound time complexity of **O(n)**.

### **Space Complexity:**
Apart from the output string, no additional variable-length data structures are used, making the space complexity **O(1)** (excluding the output).

---

## Testing

---

The `OldPhoneKeyPressDecoderTest` class includes test cases to validate the correctness of the key-press decoding methods. The test suite covers:

- **Valid cases**: Ensuring the expected message is correctly decoded from various key-press sequences.
- **Invalid cases**: Handling edge cases where an exception is expected due to incorrect input.

---
## How to run

---
1. **Cloning the repo**: Clone the repo with the following command.
    ```shell
      git clone https://github.com/duronto23/PhoneKeyPressDecode.git
    ```
2. **Building the solution**: Navigate to the `/PhoneKeyPressDecode` folder and run the following command.
    ```shell
      dotnet build
    ```
3. **Testing**: After the build being successful, run the following command to run tests.
    ```shell
       dotnet test
      ```
   
---

## How to use in another Project

---

1. **Adding a Project Reference:**
   - Reference the cloned project `PhoneKeyPressDecode.csproj` in the destination project to access its functionality.

2. **Creating and Using a NuGet Package:**
   - Navigate to the `/PhoneKeyPressDecode` directory and run the following commands:
     ```shell
     dotnet build
     dotnet pack
     ```  
   - This will generate a NuGet package inside `/PhoneKeyPressDecode/bin/Release/`.
   - Install this package in the destination project as a dependency.