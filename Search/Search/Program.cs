var data = new int[20];
var random = new Random();

// generování vstupního pole
for (int i = 0; i < data.Length; i++)
{
	data[i] = random.Next(i * 10, i * 10 + 10);
	Console.WriteLine(data[i]);
}


// hledání v poli
while (true)
{
	Console.WriteLine("Jakou hodnotu mám v poli hledat?");
	var x = Convert.ToInt32(Console.ReadLine());
	var found = false;

	for (int i = 0; i < data.Length; i++)
	{
		if (data[i] == x)
		{
			Console.WriteLine("Je tam!");
			found = true;
		}
	}
	if (!found)
	{
		Console.WriteLine("Není tam!");
	}
}