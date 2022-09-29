namespace CloveceNezlobSe
{
	/// <summary>
	/// Herní plán je tvořen rovnou řadou políček. Všichni hráči začínají na stejném startovním políčku.
	/// </summary>
	public class LinearniHerniPlan : HerniPlan
	{
		List<Policko> policka;

		public override int MaximalniPocetHracu => int.MaxValue;

		public LinearniHerniPlan(int pocetPolicek)
		{
			policka = new();

			// startovní políčko
			policka.Add(new Policko(dovolitViceFigurek: true));

			// ostatní políčka
			for (int i = 1; i < pocetPolicek - 1; i++)
			{
				policka.Add(new Policko());
			}

			// cílové políčko
			policka.Add(new Policko(jeDomecek: true, dovolitViceFigurek: true));
		}


		public override void DejFigurkuNaStartovniPolicko(Figurka figurka)
		{
			policka[0].PolozFigurku(figurka);
		}

		public override void PosunFigurku(Figurka figurka, int pocetPolicek)
		{
			Policko stavajiciPolicko = figurka.Policko;

			int indexStavajicihoPolicka = policka.IndexOf(stavajiciPolicko);
			int indexCile = (indexStavajicihoPolicka + pocetPolicek);

			if (indexCile < 0)
			{
				// figurka se vrací na začátek
				indexCile = 0;
			}
			
			if (indexCile >= policka.Count)
			{
				Console.WriteLine($"Figurka {figurka.OznaceniFigurky} vyjela z herní plochy, cíl je potřeba trefit přesně.");
				return;
			}
			Policko cilovePolicko = policka[indexCile];

			// posun figurky na novou pozici
			stavajiciPolicko.ZvedniFigurku(figurka);
			Console.WriteLine($"Posouvám figurku {figurka.OznaceniFigurky} z pozice {indexStavajicihoPolicka} na pozici {indexCile}.");
			if (cilovePolicko.JeObsazeno())
			{
				Figurka vyhozenaFigurka = cilovePolicko.ZvedniJedinouFigurku();
				Console.WriteLine($"Vyhazuji figurku {vyhozenaFigurka.OznaceniFigurky} hráče: {vyhozenaFigurka.Hrac.Jmeno}");
				policka[0].PolozFigurku(vyhozenaFigurka);
			}
			cilovePolicko.PolozFigurku(figurka);
		}

		public override Policko? ZjistiCilovePolicko(Figurka figurka, int hod)
		{
			if (figurka.Policko == null)
			{
				return null; // figurka není na herní ploše
			}

			int indexStavajicihoPolicka = policka.IndexOf(figurka.Policko);
			int indexCile = (indexStavajicihoPolicka + hod);

			if (indexCile < 0)
			{
				// figurka se vrací na začátek
				indexCile = 0;
			}

			if (indexCile >= policka.Count)
			{
				return null;
			}

			return policka[indexCile];
		}

		public override bool MuzuTahnout(Figurka figurka, int hod)
		{
			if (figurka.Policko == null)
			{
				return false; // figurka není na herní ploše
			}
			
			int indexStavajicihoPolicka = policka.IndexOf(figurka.Policko);
			if (indexStavajicihoPolicka > policka.Count - hod - 1)
			{
				return false; // figurka by vyjela z herní plochy
			}
			return true;
		}

		public override void Vykresli()
		{
			foreach (var policko in policka)
			{
				policko.Vykresli();
			}
			Console.WriteLine();
		}
	}
}
