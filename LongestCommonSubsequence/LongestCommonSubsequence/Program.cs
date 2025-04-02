var linesA = """
	Start
	Console.WriteLine("Hello");
	Console.WriteLine("World");
	Console.WriteLine("!");
	End
	""";
var linesB = """
	Init
	Console.WriteLine("World");
	Console.WriteLine("Hello");
	Console.WriteLine("!");
	Finish
	""";

var lcs = GetLcs(linesA.Split(Environment.NewLine), linesB.Split(Environment.NewLine));

foreach (var line in lcs)
{
	Console.WriteLine(line);
}

List<string> GetLcs(string[] a, string[] b)
{
	int n = a.Length;
	int m = b.Length;
	int[,] dp = new int[n + 1, m + 1];

	// Vyplnění tabulky LCS
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < m; j++)
		{
			if (a[i] == b[j])
				dp[i + 1, j + 1] = dp[i, j] + 1;
			else
				dp[i + 1, j + 1] = Math.Max(dp[i + 1, j], dp[i, j + 1]);
		}
	}

	// Rekonstrukce výsledku
	List<string> result = new();
	int x = n, y = m;

	while (x > 0 && y > 0)
	{
		if (a[x - 1] == b[y - 1])
		{
			result.Add(a[x - 1]);
			x--;
			y--;
		}
		else if (dp[x - 1, y] > dp[x, y - 1])
		{
			x--;
		}
		else
		{
			y--;
		}
	}

	result.Reverse();
	return result;
}