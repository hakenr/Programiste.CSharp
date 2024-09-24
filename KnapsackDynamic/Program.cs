Console.WriteLine("Zadej maximální objem tašky (v litrech): ");
int maxObjemTasky = int.Parse(Console.ReadLine());

Console.WriteLine("Zadej počet šperků, které se mají generovat: ");
int pocetSperku = int.Parse(Console.ReadLine());

List<Sperk> sperky = GenerujSperky(pocetSperku);

Console.WriteLine("\nSeznam vygenerovaných šperků:");
foreach (var sperk in sperky)
{
	Console.WriteLine(sperk);
}

List<Sperk> vybraneSperky = VyberSperkyDynamickeProgramovani(sperky, maxObjemTasky);

if (vybraneSperky.Count > 0)
{
	Console.WriteLine("\nZloděj vybral následující šperky:");
	int celkovyObjem = 0;
	int celkovaCena = 0;

	foreach (var sperk in vybraneSperky)
	{
		Console.WriteLine(sperk);
		celkovyObjem += sperk.Objem;
		celkovaCena += sperk.Cena;
	}

	Console.WriteLine($"\nCelkový objem šperků: {celkovyObjem}");
	Console.WriteLine($"Celková cena šperků: {celkovaCena}");
}
else
{
	Console.WriteLine("Taška je příliš malá na jakýkoliv šperk.");
}

List<Sperk> GenerujSperky(int pocet)
{
	Random rnd = new Random();
	List<Sperk> sperky = new List<Sperk>();

	for (int i = 0; i < pocet; i++)
	{
		string nazev = $"Šperk {i + 1}";
		int objem = rnd.Next(1, 50);
		int cena = rnd.Next(1000, 10000);

		sperky.Add(new Sperk(nazev, objem, cena));
	}

	return sperky;
}

List<Sperk> VyberSperkyDynamickeProgramovani(List<Sperk> sperky, int maxObjem)
{
	var dp = new int[sperky.Count + 1, maxObjem + 1];

	// Naplníme DP tabulku
	for (int i = 1; i <= sperky.Count; i++)
	{
		for (int w = 0; w <= maxObjem; w++)
		{
			if (sperky[i - 1].Objem <= w)
			{
				dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - sperky[i - 1].Objem] + sperky[i - 1].Cena);
			}
			else
			{
				dp[i, w] = dp[i - 1, w];
			}
		}
	}

	// Rekonstrukce řešení
	List<Sperk> vybraneSperky = new List<Sperk>();
	int zbyvajiciObjem = maxObjem;
	for (int i = sperky.Count; i > 0 && zbyvajiciObjem > 0; i--)
	{
		if (dp[i, zbyvajiciObjem] != dp[i - 1, zbyvajiciObjem])
		{
			vybraneSperky.Add(sperky[i - 1]);
			zbyvajiciObjem -= sperky[i - 1].Objem;
		}
	}

	return vybraneSperky;
}