using System.Text;

namespace StringLib;

public static class StringMethods
{
    public static string InterleaveStrings(string str1, string str2)
    {
        var result = new StringBuilder();
        int i = 0;

        while (i < str1.Length ||  i < str2.Length)
        {
            if (i < str1.Length) { result.Append(str1[i]); }
            if (i < str2.Length) { result.Append(str2[i]); }
            i++;
        }
        return result.ToString();
    }

    public static bool IsPalindrome(string str)
    {
        for (int i = 0; i < str.Length / 2; i++)
        {
            if (str[i] != str[^(i + 1)]) return false;
        }
        return true;
    }
}
