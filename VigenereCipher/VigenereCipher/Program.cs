Console.WriteLine("Zadejte zprávu:");
var vstup = Console.ReadLine();

Console.WriteLine("Zadejte heslo:");
var heslo = Console.ReadLine();

var encrypted = Vigenere(vstup, heslo, true);    // false = decrypt
Console.WriteLine("Zašifrovaná zpráva: " + encrypted);

var message = Vigenere(encrypted, heslo, false);    // false = decrypt
Console.WriteLine("Rozšifrovaná zpráva: " + message);



string Vigenere(string vstup, string heslo, bool encrypt)
{
	char[] vystup = new char[vstup.Length];

	for (int i = 0; i < vstup.Length; i++)
	{
		int posun = heslo[i % heslo.Length] - 'A';

		if (!encrypt)
		{
			posun = -posun;
		}

		int znak = (vstup[i] - 'A' + posun) % 26;

		if (znak < 0) // decrypt
		{
			znak = znak + 26;
		}

		vystup[i] = (char)('A' + znak);
	}
	return new string(vystup);
}
