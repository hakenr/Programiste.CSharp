public class Sperk
{
	public string Nazev { get; set; }
	public int Objem { get; set; }
	public int Cena { get; set; }

	public Sperk(string nazev, int objem, int cena)
	{
		Nazev = nazev;
		Objem = objem;
		Cena = cena;
	}

	public override string ToString()
	{
		return $"{Nazev} (Objem: {Objem}, Cena: {Cena})";
	}
}
