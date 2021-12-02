using System;

Console.WriteLine("Zadejte vstupní text, najdu v něm palindromy.");
var input = Console.ReadLine();

Console.WriteLine("\nNalezené palindromy:");
var words = input.Split(' ');
foreach (var word in words)
{
	string wordTrimmed = word.Trim(',', '.', '?', '!', ':');
	char[] chars = wordTrimmed.ToCharArray();
	Array.Reverse(chars);
	string reversedWord = new string(chars);	
	if (String.Equals(wordTrimmed, reversedWord, StringComparison.OrdinalIgnoreCase))
	{
		Console.WriteLine(wordTrimmed);
	}
}
