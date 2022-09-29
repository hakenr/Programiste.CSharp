namespace CloveceNezlobSe
{
	public class Hra
	{
		public HerniPlan HerniPlan { get; private set; }
		List<Hrac> hraci = new();

		public Hra(HerniPlan herniPlan)
		{
			this.HerniPlan = herniPlan;
		}

		public void PridejHrace(Hrac hrac)
		{
			if (hraci.Count == HerniPlan.MaximalniPocetHracu)
			{
				throw new Exception("Hra je plná, maximální počet hráčů herního plánu byl překročen.");
			}

			hraci.Add(hrac);
		}

		public void Start()
		{
			// TODO Kontrola vstupních podmínek pro zahájení hry.
			foreach (var hrac in hraci)
			{
				foreach (var figurka in hrac.Figurky)
				{
					HerniPlan.DejFigurkuNaStartovniPolicko(figurka);
				}
			}

			Kostka kostka = new Kostka(pocetSten: 6);
			List<Hrac> hraciKtereMajiVsechnyFigurkyVDomecku = new();

			while (true)
			{
				foreach (var hrac in hraci)
				{
					if (hraciKtereMajiVsechnyFigurkyVDomecku.Contains(hrac))
					{
						continue;
					}

					HerniPlan.Vykresli();

					Console.WriteLine($"Hraje hráč {hrac.Jmeno}.");

					var hod = kostka.Hod();

					var figurka = hrac.DejFigurkuKterouHrat(hod);
					if (figurka == null)
					{
						Console.WriteLine($"Hráč {hrac.Jmeno} nemůže tahnout.");
						continue;
					}

					HerniPlan.PosunFigurku(figurka, hod);

					if (figurka.Policko.JeDomecek)
					{
						Console.WriteLine($"Figurka {figurka.OznaceniFigurky} hráče {figurka.Hrac.Jmeno} došla do cíle.");

						if (hrac.MaVsechnyFigurkyVDomecku())
						{
							hraciKtereMajiVsechnyFigurkyVDomecku.Add(hrac);
						}
					}
				}
				
				if (hraciKtereMajiVsechnyFigurkyVDomecku.Count == hraci.Count)
				{
					Console.WriteLine("Hra skončila.");
					break;
				}
			}

			Console.WriteLine("Výsledky hry:");
			for (int i = 0; i < hraciKtereMajiVsechnyFigurkyVDomecku.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {hraciKtereMajiVsechnyFigurkyVDomecku[i].Jmeno}");
			}
		}

	}
}
