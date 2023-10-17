int horniMez = 1000;
Console.WriteLine($"Myslím si číslo od 0 do {horniMez}." +
	$" Uhádneš, které to je?");

int cislo = new Random().Next(horniMez + 1);
int tipovaneCislo;
int pocetTipu = 0;

do
{
	tipovaneCislo = Convert.ToInt32(Console.ReadLine());
	pocetTipu = pocetTipu + 1;
	if (tipovaneCislo > cislo)
	{
		Console.WriteLine("To není ono! Zkus menší číslo...");
	}
	if (tipovaneCislo < cislo)
	{
		Console.WriteLine("To není ono! Zkus větší číslo...");
	}
}
while (tipovaneCislo != cislo);

Console.WriteLine($"To je ono! Gratuluji," +
	$" uhádl(a) si číslo na {pocetTipu}. pokus.");
