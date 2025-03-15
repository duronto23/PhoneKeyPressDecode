using NUnit.Framework;

namespace PhoneKeyPressDecode.Test;

[TestFixture]
public class OldPhoneKeyPressDecoderTest
{
    [Test]
    [TestCase("#", "", TestName = "Sending empty message.")]
    [TestCase("33#", "E", TestName = "Decode single character.")]
    [TestCase("3 3#", "DD", TestName = "Decode input with space.")]
    [TestCase(" 4433555 555666 #", "HELLO", TestName = "Decode input with leading and trailing space.")]
    [TestCase("227*#", "B", TestName = "Decode input with leading and trailing backspace.")]
    [TestCase("33***#", "", TestName = "Decode input with consecutive backspaces on empty output.")]
    [TestCase("***#", "", TestName = "Decode input with only backspace.")]
    [TestCase("4433555 555666* * * * *#", "", TestName = "Handles backspaces with space.")]
    [TestCase("8 88777444666*664#", "TURING", TestName = "Decode complex pattern with backspace.")]
    [TestCase("8   88  77744*  444  666*664*44*4#", "TURING", TestName = "Decode by handling irregular spacing and backspace.")]
    [TestCase("4433555 555666096667775553#", "HELLO WORLD", TestName = "Decodes message with zero for space.")]
    [TestCase("**4433555 555666096667775553#", "HELLO WORLD", TestName = "Handles leading backspaces correctly.")]
    [TestCase("4433555 5556660966677755531#", "HELLO WORLD!", TestName = "Decodes message with zero for space and one for special character.")]
    public async Task OldPhonePadTest_ValidCases(string input, string expectedOutput)
    {
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    [TestCase("448", "Input should ends with a #", TestName = "Throws exception if not ends with #")]
    [TestCase("44Q8#", "Digit expected!", TestName = "Throws exception if string contains characters other than digits, * and #")]
    public async Task OldPhonePadTest_InvalidCases(string input, string exceptionMessage)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => OldPhoneKeyPressDecoder.OldPhonePad(input));
        Assert.That(ex != null);
        Assert.That(!string.IsNullOrEmpty(ex?.Message));
        Assert.That(ex?.Message, Is.EqualTo(exceptionMessage));
    }
}