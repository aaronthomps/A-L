// See https://aka.ms/new-console-template for more information

using StringLib;

Console.WriteLine("Enter text into palindrome checker");

while (true)
{
    string input = Console.ReadLine()??string.Empty;

    if(StringMethods.IsPalindrome(input))
    {
        Console.WriteLine("Palindrome");
    }
    else
    {
        Console.WriteLine("Not Palindrome");
    }
}
