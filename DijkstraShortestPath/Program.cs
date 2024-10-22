Dictionary<string, Dictionary<string, int>> cityGraph = new Dictionary<string, Dictionary<string, int>>();

// Načtení CSV dat do grafu
LoadGraphFromCsv("silnicni_vzdalenosti_mesta_reduced.csv");
//LoadGraphFromCsv("silnicni_vzdalenosti_mesta_oneway.csv");
//LoadGraphFromCsv("silnicni_vzdalenosti_mesta_full.csv");

// Zadání měst pro nalezení nejkratší cesty
Console.WriteLine("Zadejte výchozí město:");
string startCity = Console.ReadLine();
if (!cityGraph.ContainsKey(startCity))
{
	Console.WriteLine("Zadané město neexistuje.");
	return;
}

Console.WriteLine("Zadejte cílové město:");
string endCity = Console.ReadLine();
if (!cityGraph.ContainsKey(endCity))
{
	Console.WriteLine("Zadané město neexistuje.");
	return;
}

// Výpočet nejkratší cesty
List<string> shortestPath = Dijkstra(startCity, endCity, out int totalDistance);

// Zobrazení výsledků
if (shortestPath != null)
{
	Console.WriteLine($"Nejkratší cesta z {startCity} do {endCity} je: {string.Join(" -> ", shortestPath)}");
	Console.WriteLine($"Celková vzdálenost: {totalDistance} km");
}
else
{
	Console.WriteLine($"Cesta mezi {startCity} a {endCity} nebyla nalezena.");
}

// Funkce pro načtení CSV souboru a vytvoření grafu							   x;
void LoadGraphFromCsv(string filePath)
{
	var lines = File.ReadAllLines(filePath);
	foreach (var line in lines.Skip(1))
	{
		var parts = line.Split(',');
		string city1 = parts[0].Trim();
		string city2 = parts[1].Trim();
		int distance = int.Parse(parts[2]);

		// Přidání cesty mezi městy do grafu
		if (!cityGraph.ContainsKey(city1))
		{
			cityGraph[city1] = new Dictionary<string, int>();
		}
		if (!cityGraph.ContainsKey(city2))
		{
			cityGraph[city2] = new Dictionary<string, int>();
		}

		cityGraph[city1][city2] = distance;
		cityGraph[city2][city1] = distance; // Graf je neorientovaný (obousměrná cesta)
	}
}

// Dijkstra algoritmus pro nalezení nejkratší cesty
List<string> Dijkstra(string start, string target, out int totalDistance)
{
	var distances = new Dictionary<string, int>(); // Vzdálenost od startu do každého města
	var previous = new Dictionary<string, string>(); // Udržování předchozích uzlů pro rekonstrukci cesty
	var unvisited = new HashSet<string>(); // Neprozkoumaná města
	totalDistance = 0;

	// Inicializace grafu
	foreach (var city in cityGraph)
	{
		distances[city.Key] = int.MaxValue;
		unvisited.Add(city.Key);
	}
	distances[start] = 0;

	// Hledání cesty
	while (unvisited.Count > 0)
	{
		string currentCity = null;

		// Najít neprozkoumané město s nejkratší vzdáleností
		foreach (var city in unvisited)
		{
			if (currentCity == null || distances[city] < distances[currentCity])
			{
				currentCity = city;
			}
		}

		// Pokud jsme dosáhli cílového města, ukončíme algoritmus
		if (currentCity == target)
		{
			totalDistance = distances[currentCity];
			return ReconstructPath(previous, target);
		}

		unvisited.Remove(currentCity);

		// Aktualizace vzdáleností sousedů aktuálního města
		foreach (var neighbor in cityGraph[currentCity])
		{
			int tentativeDistance = distances[currentCity] + neighbor.Value;

			if (tentativeDistance < distances[neighbor.Key])
			{
				distances[neighbor.Key] = tentativeDistance;
				previous[neighbor.Key] = currentCity;
			}
		}
	}

	return null; // Pokud není nalezena cesta
}

// Rekonstrukce cesty z předchozích uzlů
List<string> ReconstructPath(Dictionary<string, string> previous, string target)
{
	var path = new List<string>();
	string currentCity = target;

	while (previous.ContainsKey(currentCity))
	{
		path.Insert(0, currentCity);
		currentCity = previous[currentCity];
	}

	path.Insert(0, currentCity); // Přidání startovního města
	return path;
}
