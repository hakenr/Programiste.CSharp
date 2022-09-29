namespace CloveceNezlobSe
{
	public class HerniStrategieTahniPrvniMoznouFigurkou : HerniStrategie
	{
		protected readonly Hra hra;

		public HerniStrategieTahniPrvniMoznouFigurkou(Hra hra)
		{
			this.hra = hra;
		}

		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
		{
			var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
			var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
			if (figurkyKtereMuzuHrat.Any())
			{
				return figurkyKtereMuzuHrat.First();
			}

			return null;
		}
	}
}
