namespace CloveceNezlobSe
{
	public class Hrac
	{
		public string Jmeno { get; private set; }
		public List<Figurka> Figurky { get; }

		private HerniStrategie herniStrategie;

		public Hrac(string jmeno, HerniStrategie herniStrategie)
		{
			this.Jmeno = jmeno;
			this.herniStrategie = herniStrategie;

			Figurky = new();
			for (int i = 0; i < 4; i++)
			{
				Figurky.Add(new Figurka(this, $"{this.Jmeno.Substring(0,1)}{(i + 1)}"));
			}
		}

		public bool MaVsechnyFigurkyVDomecku()
		{
			return Figurky.All(figurka => figurka.JeVDomecku());
		}

		public Figurka? DejFigurkuKterouHrat(int hod)
		{
			return herniStrategie.DejFigurkuKterouHrat(this, hod);
		}
	}
}
