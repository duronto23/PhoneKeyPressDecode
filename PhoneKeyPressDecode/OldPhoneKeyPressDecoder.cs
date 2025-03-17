using System.Text;
using PhoneKeyPressDecode.Utility;

namespace PhoneKeyPressDecode;

public class OldPhoneKeyPressDecoder
{
    private static readonly Dictionary<char, string> DigitToCharacters = new Dictionary<char, string>
    {
        { '0', " " },
        { '1', "!@#$%^&*-_=+\'\",.;:?<>\\/(){}[]" }, // Arbitrarily entered from keyboard, sequence might differ from original sequence in old phone.
        { '2', "ABC" },
        { '3', "DEF" },
        { '4', "GHI" },
        { '5', "JKL" },
        { '6', "MNO" },
        { '7', "PQRS" },
        { '8', "TUV" },
        { '9', "WXYZ" }
    };
    
    public static string OldPhonePad(string input) {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        if (!input.Contains('#'))
        {
            throw new ArgumentException("Input should ends with a #");
        }
        
        return GetDecodedMessage(input);
    }

    private static string GetDecodedMessage(string input)
    {
        var stringBuilder = new StringBuilder();
        int start = 0, end = 0;
        while (!input[start].Equals('#'))
        {
            if (char.IsWhiteSpace(input[start]))
            {
                end = ++start;
            }
            else if (input[start].Equals('*'))
            {
                stringBuilder.SafelyRemoveLast();
                end = ++start;
            }
            else if (!input[start].Equals(input[end]))
            {
                var nextCharacter = GetDecodedCharacter(input[start], end - start);
                stringBuilder.Append(nextCharacter);
                start = end;
            }
            else
            {
                ++end;
            }
        }
        return stringBuilder.ToString();
    }

    private static char GetDecodedCharacter(char digit, int count)
    {
        if (!DigitToCharacters.TryGetValue(digit, out var characters))
        {
            throw new ArgumentException("Digit expected!");
        }
        return characters[(count - 1) % characters.Length];
    }
}