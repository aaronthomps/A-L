namespace StringLibTests;

public class UnitTests
{
    [Fact]
    public void InterleaveStrings_BothSameLength()
    {
        string str1 = "abc";
        string str2 = "123";

        string result = StringLib.StringMethods.InterleaveStrings(str1, str2);

        Assert.Equal("a1b2c3", result);
    }

    [Fact]
    public void InterleaveStrings_FirstLonger()
    {
        string str1 = "abcd";
        string str2 = "12";

        string result = StringLib.StringMethods.InterleaveStrings(str1, str2);
        Assert.Equal("a1b2cd", result);
    }

    [Fact]
    public void InterleaveStrings_SecondLonger()
    {
        string str1 = "ab";
        string str2 = "1234";

        string result = StringLib.StringMethods.InterleaveStrings(str1, str2);
        Assert.Equal("a1b234", result);
    }

    [Theory]
    [InlineData("hello", false)]
    [InlineData("racecar", true)]
    [InlineData("123321", true)]
    public void IsPalindromeTest(string str1, bool expected)
    {
        var result = StringLib.StringMethods.IsPalindrome(str1);
        Assert.Equal(expected, result);
    }
}