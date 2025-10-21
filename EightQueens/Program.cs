const int N = 8;

int[] sloupce = new int[N];
Solve(sloupce, 0);

static bool Solve(int[] sloupce, int radek)
{
	if (radek == N)
	{
		VypisReseni(sloupce);
		return true; // nalezeno jedno řešení
	}

	for (int sloupec = 0; sloupec < N; sloupec++)
	{
		if (IsSafe(sloupce, radek, sloupec))
		{
			sloupce[radek] = sloupec;
			if (Solve(sloupce, radek + 1))
			{
				return true; // najdi první řešení
			}
		}
	}

	return false; // žádné řešení v tomto větvení
}

static bool IsSafe(int[] sloupce, int radek, int sloupec)
{
	for (int i = 0; i < radek; i++)
	{
		int j = sloupce[i];
		if (j == sloupec)
		{
			return false; // stejný sloupec
		}
		if (Math.Abs(i - radek) == Math.Abs(j - sloupec))
		{
			return false; // diagonála
		}
	}
	return true;
}

static void VypisReseni(int[] sloupce)
{
	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < N; j++)
		{
			Console.Write(sloupce[i] == j ? "Q " : ". ");
		}
		Console.WriteLine();
	}
}
