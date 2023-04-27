// Vstupní data
(double x, double y)[] uciciVzorek = { (1, 1), (2, 2), (3, 3), (4, 4), (5, 5) };

// Výpočet koeficientů
(double alpha, double beta) = CalculateCoefficients(uciciVzorek);

// Výpis výsledků
Console.WriteLine($"Nalezená aproximační lineární funkce: y = {alpha} + {beta} * x");

// Testování aproximace
Console.WriteLine("Aproximace pro jednotlivé body učícího vzorku:");
for (int i = 0; i < uciciVzorek.Length; i++)
{
	double approximatedY = alpha + beta * uciciVzorek[i].x;
	Console.WriteLine($"x = {uciciVzorek[i].x}, y (skutečné) = {uciciVzorek[i].y}, y (aproximované) = {approximatedY}");
}

// Predikce
Console.WriteLine("\nPredikce hodnot:");
while (true)
{
	Console.Write("> ");
	if (double.TryParse(Console.ReadLine(), out var x))
	{
		double y = alpha + beta * x;
		Console.WriteLine($"Pro x = {x} predikuji y = {y}.\n");
	}
	else
	{
		break;
	}
}

// Metoda pro výpočet koeficientů alfa a beta
static (double alpha, double beta) CalculateCoefficients((double x, double y)[] values)
{
	int n = values.Length;
	double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;

	for (int i = 0; i < n; i++)
	{
		sumX += values[i].x;
		sumY += values[i].y;
		sumXY += values[i].x * values[i].y;
		sumX2 += values[i].x * values[i].x;
	}

	double beta = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
	double alpha = (sumY - beta * sumX) / n;

	return (alpha, beta);
}