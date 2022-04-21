while (true)
{
	Console.Write("Zadejte výraz k výpočtu: ");
	string input = Console.ReadLine();

	try
	{
		int result = Calculate(input);
		Console.WriteLine($"Výsledek: {result}");
	}
	catch (DivideByZeroException)
	{
		Console.WriteLine("Chyba: Nelze dělit nulou!");
	}
	catch (FormatException)
	{
		Console.WriteLine("Chyba: Nespravný formát vstupu!");
	}
	catch (OverflowException)
	{
		Console.WriteLine("Chyba: Vstup nebo výsledek je mimo rozsah typu Int32!");
	}
	catch (MyMissingOperatorException ex)
	{
		Console.WriteLine($"Chyba: {ex.Message}");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Chyba: {ex.Message}");
	}

	Console.WriteLine();
}

int Calculate(string input)
{
	if (input.Contains("/"))
	{
		string[] parts = input.Split('/');
		int left = int.Parse(parts[0]);
		int right = int.Parse(parts[1]);
		return left / right;
	}
	else if (input.Contains("*"))
	{
		string[] parts = input.Split('*');
		int left = int.Parse(parts[0]);
		int right = int.Parse(parts[1]);
		return left * right;
	}
	else if (input.Contains("+"))
	{
		string[] parts = input.Split('+');
		int left = int.Parse(parts[0]);
		int right = int.Parse(parts[1]);
		return left + right;
	}
	else if (input.Contains("-"))
	{
		string[] parts = input.Split('-');
		int left = int.Parse(parts[0]);
		int right = int.Parse(parts[1]);
		return left - right;
	}

	throw new MyMissingOperatorException("Chybí operátor!");
}

public class MyMissingOperatorException : Exception
{
	public MyMissingOperatorException() : base() { }
	public MyMissingOperatorException(string message) : base(message) { }
}