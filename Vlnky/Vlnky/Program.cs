int posun = 1;
int smer = 1;
while (true)
{
	if ((posun > 0) && (posun < 11))
	{
		posun = posun + smer;
	}
	for (int i = 0; i < posun; i++)
	{
		Console.Write(" ");
	}
	Console.WriteLine("O");
	Thread.Sleep(100);

	if (posun == 10)
	{
		smer = -1;
	}
	if (posun == 1)
	{
		smer = 1;
	}
}
