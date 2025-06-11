Console.WriteLine("Zadej číslo");
int cislo = Convert.ToInt32(Console.ReadLine());

int delitel = 2;
while (true)
{
	if (cislo % delitel == 0)
	{
		if (cislo == delitel)
		{
			Console.WriteLine("Toto je prvočíslo.");
		}
		if (delitel < cislo)
		{
			Console.WriteLine("Toto není prvočíslo.");
		}
		break;
	}
	delitel = delitel + 1;
}
