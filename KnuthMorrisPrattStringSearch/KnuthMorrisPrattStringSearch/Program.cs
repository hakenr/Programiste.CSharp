// Vytvoření prefixové tabulky LPS
int[] ComputeLPS(string pattern)
{
	int m = pattern.Length;
	int[] lps = new int[m];
	int len = 0; // délka předchozího nejdelšího prefixu

	int i = 1;
	while (i < m)
	{
		if (pattern[i] == pattern[len])
		{
			len++;
			lps[i] = len;
			i++;
		}
		else
		{
			if (len != 0)
			{
				len = lps[len - 1];
			}
			else
			{
				lps[i] = 0;
				i++;
			}
		}
	}
	return lps;
}

// Vyhledávání vzoru v textu pomocí KMP algoritmu
List<int> KMPSearch(string text, string pattern)
{
	int n = text.Length;
	int m = pattern.Length;
	List<int> result = new List<int>();

	int[] lps = ComputeLPS(pattern);

	int i = 0; // index pro text
	int j = 0; // index pro pattern

	while (i < n)
	{
		if (text[i] == pattern[j])
		{
			i++;
			j++;
		}

		if (j == m)
		{
			result.Add(i - j);
			j = lps[j - 1];
		}
		else if (i < n && text[i] != pattern[j])
		{
			if (j != 0)
			{
				j = lps[j - 1];
			}
			else
			{
				i++;
			}
		}
	}

	return result;
}

// Testování implementace
string text = "ABABDABACDABABCABAB";
string pattern = "ABABCABAB";

List<int> matches = KMPSearch(text, pattern);

Console.WriteLine("Nalezené pozice vzoru v textu:");
Console.WriteLine(string.Join(", ", matches));
