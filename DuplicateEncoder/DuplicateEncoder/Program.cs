// https://www.codewars.com/kata/54b42f9314d9229fd6000d9c

while (true)
{
	Console.WriteLine("Your input?");
	var input = Console.ReadLine();
	
	var encoder = new DuplicateEncoder(input);
	Console.WriteLine(encoder.Encode());
	Console.WriteLine(encoder.Encode('_', '+'));
	Console.WriteLine(encoder.Encode('_', '!', 3));
	encoder.PrintStatistics();
}

public class DuplicateEncoder
{
	private string normalizedInput;
	private Dictionary<char, int> statistics;

	public DuplicateEncoder(string input)
	{
		this.normalizedInput = input.ToLower();
		BuildStatistics();
	}

	private void BuildStatistics()
	{
		statistics = new Dictionary<char, int>();
		foreach (var character in normalizedInput)
		{
			if (statistics.TryGetValue(character, out int count))
			{
				statistics[character] = count + 1;
			}
			else
			{
				statistics[character] = 1;
			}
		}
	}

	public string Encode(char uniqueSymbol = '(', char duplicateSymbol = ')', int duplicateThreshold = 2)
	{
		string encodedResult = null;
		foreach (var character in normalizedInput)
		{
			if (statistics.TryGetValue(character, out int count) && (count >= duplicateThreshold))
			{
				encodedResult = encodedResult + duplicateSymbol;
			}
			else
			{
				encodedResult = encodedResult + uniqueSymbol;
			}
		}
		return encodedResult;
	}

	public void PrintStatistics()
	{
		Console.WriteLine("Statistics:");
		foreach (var dictionaryItem in statistics)
		{
			Console.WriteLine($"{dictionaryItem.Key}: {dictionaryItem.Value}");
		}
	}
}