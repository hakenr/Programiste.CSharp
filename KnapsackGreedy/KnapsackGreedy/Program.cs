Console.WriteLine("Zadej maximální objem tašky: ");
double maxObjemTasky = double.Parse(Console.ReadLine());

Console.WriteLine("Zadej počet šperků, které se mají generovat: ");
int pocetSperku = int.Parse(Console.ReadLine());

List<Sperk> sperky = GenerujSperky(pocetSperku);

Console.WriteLine("\nSeznam vygenerovaných šperků:");
foreach (var sperk in sperky)
{
	Console.WriteLine(sperk);
}

List<Sperk> vybraneSperky = VyberSperky(sperky, maxObjemTasky);

if (vybraneSperky.Count > 0)
{
	Console.WriteLine("\nZloděj vybral následující šperky:");
	double celkovyObjem = vybraneSperky.Sum(s => s.Objem);
	double celkovaCena = vybraneSperky.Sum(s => s.Cena);

	foreach (var sperk in vybraneSperky)
	{
		Console.WriteLine(sperk);
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
	List<Sperk> sperky = new List<Sperk>();

	for (int i = 0; i < pocet; i++)
	{
		string nazev = $"Šperk {i + 1}";
		double objem = Random.Shared.Next(10, 50);
		double cena = Random.Shared.Next(1000, 10000);

		sperky.Add(new Sperk(nazev, objem, cena));
	}

	return sperky;
}

List<Sperk> VyberSperky(List<Sperk> sperky, double maxObjem)
{
	// Řadíme šperky podle poměru cena/objem (největší poměr první)
	var serazeneSperky = sperky.OrderByDescending(s => s.Cena / s.Objem);
	List<Sperk> vybraneSperky = new List<Sperk>();
	double aktualniObjem = 0;

	foreach (var sperk in serazeneSperky)
	{
		if (aktualniObjem + sperk.Objem <= maxObjem)
		{
			vybraneSperky.Add(sperk);
			aktualniObjem += sperk.Objem;
		}
	}

	return vybraneSperky;
}