namespace CloveceNezlobSe
{
	public class HerniStrategieInteraktivni : HerniStrategie
	{
		private readonly Hra hra;

		public HerniStrategieInteraktivni(Hra hra)
		{
			this.hra = hra;
		}

		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
		{
			// Vykreslí herní plán
			Console.WriteLine($"\n=== Tah hráče {hrac.Jmeno} ===");
			Console.WriteLine($"Hod kostky: {hod}");
			Console.WriteLine("\nAktuální stav herního plánu:");
			hra.HerniPlan.Vykresli();

			// Najde všechny figurky, kterými může hráč táhnout
			var dostupneFigurky = ZjistiDostupneFigurky(hrac, hod);

			if (!dostupneFigurky.Any())
			{
				Console.WriteLine("Nemáte žádnou figurku, kterou byste mohli táhnout.");
				Console.WriteLine("Stiskněte libovolnou klávesu pro pokračování...");
				Console.ReadKey();
				return null;
			}

			// Zobrazí možnosti
			Console.WriteLine("\nMožné tahy:");
			for (int i = 0; i < dostupneFigurky.Count; i++)
			{
				var figurka = dostupneFigurky[i];
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);

				string popis = $"{i + 1}. Figurka {figurka.OznaceniFigurky}";

				if (cilovePolicko != null)
				{
					if (cilovePolicko.JeDomecek)
					{
						popis += " (do domečku)";
					}
					else if (cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
					{
						popis += " (vyhození protihráče)";
					}
				}

				Console.WriteLine(popis);
			}

			// Požádá uživatele o výběr
			int vyber;
			do
			{
				Console.Write($"\nVyberte figurku (1-{dostupneFigurky.Count}): ");
				string? vstup = Console.ReadLine();

				if (int.TryParse(vstup, out vyber) && vyber >= 1 && vyber <= dostupneFigurky.Count)
				{
					break;
				}

				Console.WriteLine("Neplatný výběr, zkuste to znovu.");
			}
			while (true);

			return dostupneFigurky[vyber - 1];
		}

		private List<Figurka> ZjistiDostupneFigurky(Hrac hrac, int hod)
		{
			var dostupneFigurky = new List<Figurka>();

			foreach (var figurka in hrac.Figurky)
			{
				if (hra.HerniPlan.MuzuTahnout(figurka, hod))
				{
					dostupneFigurky.Add(figurka);
				}
			}

			return dostupneFigurky;
		}
	}
}