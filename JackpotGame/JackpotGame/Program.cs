Console.Clear();
int score = 0;
int pokus = 0;

while (true)
{
	int cislo1 = new Random().Next(10);
	Console.SetCursorPosition(10, 10);
	Console.Write(cislo1);

	int cislo2 = new Random().Next(10);
	Console.SetCursorPosition(20, 10);
	Console.Write(cislo2);

	int cislo3 = new Random().Next(10);
	Console.SetCursorPosition(30, 10);
	Console.Write(cislo3);

	if (Console.KeyAvailable)
	{
		Console.ReadKey();

		pokus = pokus + 1;

		if (cislo1 == cislo2)
		{
			score = score + 1;
		}
		if (cislo2 == cislo3)
		{
			score = score + 1;
		}
		if (cislo1 == cislo3)
		{
			score = score + 1;
		}

		Console.SetCursorPosition(1, 1);
		Console.Write($"{score}/{pokus}");
		Console.Beep();
		Thread.Sleep(1000); // 1s
	}
}
