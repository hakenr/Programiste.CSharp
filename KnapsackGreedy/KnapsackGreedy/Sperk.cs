public class Sperk
{
	public string Nazev { get; set; }
	public double Objem { get; set; }
	public double Cena { get; set; }

	public Sperk(string nazev, double objem, double cena)
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
