Console.Write("Zadej hausnumero:");
var vstup = Console.ReadLine();

int[] cetnostCislic = new int[10];

foreach (var prvek in vstup)
{
	var cislice = Convert.ToInt32(prvek.ToString());
	cetnostCislic[cislice] = cetnostCislic[cislice] + 1;
}

for (int i = 0; i < cetnostCislic.Length; i++)
{
	Console.WriteLine($"Číslice {i} je tam {cetnostCislic[i]}x.");
}
