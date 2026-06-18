using Xunit;
using ChessExample;


namespace TestProject1;


public class CheckerBoardPositionTests
{
    [Fact]
    public void Constructor_WithValidCoordinates_SetsXAndY()
    {
        var position = new CheckerBoardPosition(3, 5);

        Assert.Equal(3, position.X);
        Assert.Equal(5, position.Y);
    }

    [Fact]
    public void Constructor_WithXZero_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new CheckerBoardPosition(0, 1));
    }

    [Fact]
    public void Constructor_WithXGreaterThanEight_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new CheckerBoardPosition(9, 1));
    }

    [Fact]
    public void Constructor_WithYZero_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new CheckerBoardPosition(1, 0));
    }

    [Fact]
    public void Constructor_WithYGreaterThanEight_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new CheckerBoardPosition(1, 9));
    }

    [Theory]
    [InlineData(1, 'A')]
    [InlineData(2, 'B')]
    [InlineData(8, 'H')]
    public void XLetter_ReturnsCorrectLetter(byte x, char expectedLetter)
    {
        var position = new CheckerBoardPosition(x, 1);

        Assert.Equal(expectedLetter, position.XLetter);
    }

    [Theory]
    [InlineData(1, 1, "A1")]
    [InlineData(8, 8, "H8")]
    [InlineData(4, 6, "D6")]
    public void ToString_ReturnsChessNotation(byte x, byte y, string expected)
    {
        var position = new CheckerBoardPosition(x, y);

        Assert.Equal(expected, position.ToString());
    }

    [Theory]
    [InlineData("A1", 1, 1)]
    [InlineData("H8", 8, 8)]
    [InlineData("D5", 4, 5)]
    public void TryParse_WithValidString_ReturnsTrueAndPosition(
        string input,
        byte expectedX,
        byte expectedY)
    {
        var result = CheckerBoardPosition.TryParse(input, null, out var position);

        Assert.True(result);
        Assert.NotNull(position);
        Assert.Equal(expectedX, position.X);
        Assert.Equal(expectedY, position.Y);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("A9")]
    [InlineData("I1")]
    [InlineData("a1")]
    [InlineData("A10")]
    [InlineData("11")]
    public void TryParse_WithInvalidString_ReturnsFalse(string? input)
    {
        var result = CheckerBoardPosition.TryParse(input, null, out var position);

        Assert.False(result);
        Assert.Null(position);
    }

    [Fact]
    public void Parse_WithValidString_ReturnsPosition()
    {
        var position = CheckerBoardPosition.Parse("B3", null);

        Assert.Equal(2, position.X);
        Assert.Equal(3, position.Y);
    }

    [Fact]
    public void Parse_WithInvalidString_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() =>
            CheckerBoardPosition.Parse("Z9", null));
    }
}