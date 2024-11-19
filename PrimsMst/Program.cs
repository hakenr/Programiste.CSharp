using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
	static void Main()
	{
		var sw = System.Diagnostics.Stopwatch.StartNew();

		// 1. Načtení dat z CSV
		var cities = LoadCitiesFromCsv("worldcities.csv", count: 7000);

		// 2. Výpočet minimální silniční sítě
		var result = FindMinimumRoadNetwork(cities);

		// 3. Výstup výsledků
		PrintResult(result);

		Console.WriteLine($"Čas výpočtu: {sw.ElapsedMilliseconds} ms");
	}

	// Načte města ze CSV souboru
	static Dictionary<string, (double Lat, double Lon)> LoadCitiesFromCsv(string filePath, int count)
	{
		var cities = new Dictionary<string, (double Lat, double Lon)>();

		var lines = File.ReadAllLines(filePath).Skip(1).Take(count); // Přeskočí hlavičku
		foreach (var line in lines)
		{
			var parts = line.Split(',');
			var city = parts[0].Trim('"');
			var lat = double.Parse(parts[2].Trim('"'), CultureInfo.InvariantCulture);
			var lon = double.Parse(parts[3].Trim('"'), CultureInfo.InvariantCulture);
			cities[city] = (lat, lon);
		}

		return cities;
	}

	// Haversinova formule pro výpočet vzdálenosti mezi dvěma body
	static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
	{
		const double EarthRadius = 6371.0; // Poloměr Země v km
		double dLat = DegreesToRadians(lat2 - lat1);
		double dLon = DegreesToRadians(lon2 - lon1);

		double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
				   Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
				   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
		double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

		return EarthRadius * c;
	}

	static double DegreesToRadians(double degrees)
	{
		return degrees * Math.PI / 180.0;
	}

	// Implementace Primova algoritmu
	static List<(string City1, string City2, double Distance)> FindMinimumRoadNetwork(Dictionary<string, (double Lat, double Lon)> cities)
	{
		var visited = new HashSet<string>();
		var edges = new List<(string City1, string City2, double Distance)>();

		// Zvolíme náhodné počáteční město
		var startCity = cities.Keys.First();
		visited.Add(startCity);

		var priorityQueue = new SortedSet<(double Distance, string City1, string City2)>();

		// Přidáme všechny hrany z počátečního města
		foreach (var city in cities)
		{
			if (city.Key != startCity)
			{
				double distance = CalculateDistance(cities[startCity].Lat, cities[startCity].Lon, city.Value.Lat, city.Value.Lon);
				priorityQueue.Add((distance, startCity, city.Key));
			}
		}

		// Hledáme minimální kostru
		while (visited.Count < cities.Count)
		{
			var edge = priorityQueue.First();
			priorityQueue.Remove(edge);

			if (!visited.Contains(edge.City2))
			{
				visited.Add(edge.City2);
				Console.WriteLine($"Přidávám hranu {visited.Count}: {edge.City1} - {edge.City2} ({edge.Distance:N2} km)");
				edges.Add((edge.City1, edge.City2, edge.Distance));

				// Přidáme nové hrany z právě přidaného města
				foreach (var city in cities)
				{
					if (!visited.Contains(city.Key))
					{
						double distance = CalculateDistance(cities[edge.City2].Lat, cities[edge.City2].Lon, city.Value.Lat, city.Value.Lon);
						priorityQueue.Add((distance, edge.City2, city.Key));
					}
				}
			}
		}

		return edges;
	}

	// Výstup výsledků
	static void PrintResult(List<(string City1, string City2, double Distance)> result)
	{
		Console.WriteLine("Minimální silniční síť:");
		double totalDistance = 0;

		foreach (var edge in result)
		{
			//Console.WriteLine($"{edge.City1} - {edge.City2} ({edge.Distance:F2} km)");
			totalDistance += edge.Distance;
		}

		Console.WriteLine($"Celková délka: {totalDistance:N2} km");
	}
}
