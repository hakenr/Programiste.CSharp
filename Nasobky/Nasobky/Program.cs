Console.WriteLine($"Vypíšu násobky A, které jsou zároveň násobky B.");
Console.WriteLine("Zadejte celé číslo A:");
int a = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Zadejte celé číslo B:");
int b = Convert.ToInt32(Console.ReadLine());

Console.WriteLine($"Násobky {a}, které jsou zároveň násobky {b}:");
int nasobek = 0;
for (int poradi = 0; poradi < 10; poradi++)
{
	do
	{
		nasobek = nasobek + a;
	}
	while ((nasobek % b) != 0); // % vypočte zbytek po dělení
	Console.WriteLine(nasobek);
}