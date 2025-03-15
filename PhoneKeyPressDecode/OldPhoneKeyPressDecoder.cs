using System.Text;
using PhoneKeyPressDecode.Utility;

namespace PhoneKeyPressDecode;

public class OldPhoneKeyPressDecoder
{
    public static string OldPhonePad(string input) {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        if (!input.Contains('#'))
        {
            throw new ArgumentException("Input should ends with a #");
        }
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

    private static char GetDecodedCharacter(char digit, int cnt)
    {
        var characters = GetCharactersForDigit(digit);
        var lettersIndex = (cnt - 1) % characters.Length;
        return characters[lettersIndex];
    }

    private static string GetCharactersForDigit(char digit)
    {
        return digit switch
        {
            '0' => " ",
            '1' => "!@#$%^&*-_=+\'\",.;:?<>\\/(){}[]", // Arbitrarily entered from keyboard, sequence might differ from original sequence in old phone.
            '2' => "ABC",
            '3' => "DEF",
            '4' => "GHI",
            '5' => "JKL",
            '6' => "MNO",
            '7' => "PQRS",
            '8' => "TUV",
            '9' => "WXYZ",
            _ => throw new ArgumentException("Digit expected!")
        };
    }
}