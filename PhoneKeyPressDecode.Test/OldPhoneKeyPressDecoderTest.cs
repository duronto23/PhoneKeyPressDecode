using NUnit.Framework;

namespace PhoneKeyPressDecode.Test;

[TestFixture]
public class OldPhoneKeyPressDecoderTest
{
    [Test]
    [Category("ValidInput")]
    public void PressingSameKeyContinuously_DecodesAs_SingleCharacter()
    {
        //Arrange
        const string input = "33333#";
        const string expectedOutput = "E";
        
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    [Category("ValidInput")]
    public void PressingSameKeyWithPause_DecodesAs_MultipleCharacters()
    {
        //Arrange
        const string input = "3 3333#";
        const string expectedOutput = "DD";
        
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    [Category("ValidInput")]
    public void LongPause_ShouldNot_AffectTheDecodedMessage()
    {
        //Arrange
        const string inputWithRegularPause = "3 33 333#";
        const string inputWithLongPause = "3    33      333#";
        const string expectedOutput = "DEF";
        
        // Act
        var outputForRegularPauseInput = OldPhoneKeyPressDecoder.OldPhonePad(inputWithRegularPause);
        var outputForLongPauseInput = OldPhoneKeyPressDecoder.OldPhonePad(inputWithLongPause);
        
        // Assert
        Assert.That(outputForRegularPauseInput, Is.EqualTo(expectedOutput));
        Assert.That(outputForLongPauseInput, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    [Category("ValidInput")]
    public void LeadingOrTrailingPause_ShouldNot_AffectTheDecodedMessage()
    {
        //Arrange
        const string inputWithRegularPause = "3 33 333#";
        const string inputWithIrregularPause = "      3    33      333   #";
        const string expectedOutput = "DEF";
        
        // Act
        var outputForRegularPauseInput = OldPhoneKeyPressDecoder.OldPhonePad(inputWithRegularPause);
        var outputForIrregularPauseInput = OldPhoneKeyPressDecoder.OldPhonePad(inputWithIrregularPause);
        
        // Assert
        Assert.That(outputForRegularPauseInput, Is.EqualTo(expectedOutput));
        Assert.That(outputForIrregularPauseInput, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    [Category("ValidInput")]
    public void SpecialCharacterAndSpace_Should_DecodedCorrectly()
    {
        //Arrange
        const string input = "44 4441044666902777330999666881111111111111111111#";
        const string expectedOutput = "HI! HOW ARE YOU?";
        
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(output));
    }
    
    [Test]
    [Category("ValidInput")]
    public void Backspaces_Should_HandleProperly()
    {
        //Arrange
        const string input = "227*#";
        const string expectedOutput = "B";
        
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    [Category("ValidInput")]
    [TestCase("3 3#", "DD", TestName = "Decode input with space.")]
    [TestCase(" 4433555 555666 #", "HELLO", TestName = "Decode input with leading and trailing space.")]
    [TestCase("33***#", "", TestName = "Decode input with consecutive backspaces on empty output.")]
    [TestCase("4433555 555666* * * * *#", "", TestName = "Handles backspaces with space.")]
    [TestCase("8 88777444666*664#", "TURING", TestName = "Decode complex pattern with backspace.")]
    [TestCase("8   88  77744*  444  666*664*44*4#", "TURING", TestName = "Decode by handling irregular spacing and backspace.")]
    [TestCase("4433555 555666096667775553#", "HELLO WORLD", TestName = "Decodes message with zero for space.")]
    [TestCase("**4433555 555666096667775553#", "HELLO WORLD", TestName = "Handles leading backspaces correctly.")]
    [TestCase("4433555 5556660966677755531#", "HELLO WORLD!", TestName = "Decodes message with zero for space and one for special character.")]
    public void Passing_ValidInput_DecodeAsExpected(string input, string expectedOutput)
    {
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(expectedOutput));
    }
    
    
    [Test]
    [Category("EdgeCase")]
    public void Pressing_SendButtonOnly_DecodeAsEmptyMessage()
    {
        //Arrange
        const string input = "#";
        
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(string.Empty));
    }
    
    [Test]
    [Category("EdgeCase")]
    public void OnlyBackspaces_ShouldDecodeAs_EmptyMessage()
    {
        //Arrange
        const string input = "****#";
        
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(string.Empty));
    }
    
    [Test]
    [Category("EdgeCase")]
    public void KeyPressesAfterSendButton_ShouldNot_ConsideredAsPartOfMessage()
    {
        //Arrange
        const string input = "6 666777744 44488777#33664887730982334";
        const string expectedOutput = "MOSHIUR";
        
        // Act
        var output = OldPhoneKeyPressDecoder.OldPhonePad(input);
        
        // Assert
        Assert.That(output, Is.EqualTo(expectedOutput));
    }
    
    [Test]
    [Category("InvalidInput")]
    [TestCase("448", "Input should ends with a #", TestName = "Throws exception if not ends with #")]
    [TestCase("44Q8#", "Digit expected!", TestName = "Throws exception if string contains characters other than digits, * and #")]
    public void Passing_InvalidInput_ShouldThrowException(string input, string exceptionMessage)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => OldPhoneKeyPressDecoder.OldPhonePad(input));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex?.Message, Is.Not.Null);
        Assert.That(ex?.Message, Is.EqualTo(exceptionMessage));
    }
}