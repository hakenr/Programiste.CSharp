using System;
using System.Collections.Generic;

public class MaxFlowEdmondsKarp
{
	/// <summary>
	/// Načte vstup: N, M, S, T, poté M hran (from, to, capacity).
	/// Následně spočítá maximální tok ze S do T a vypíše výsledek.
	/// </summary>
	public static void Main()
	{
		// Načtení počtu vrcholů (N) a počtu hran (M)
		string[] line = Console.ReadLine().Split();
		int N = int.Parse(line[0]);
		int M = int.Parse(line[1]);

		// Načtení S a T
		line = Console.ReadLine().Split();
		int S = int.Parse(line[0]);
		int T = int.Parse(line[1]);

		// capacity[u][v] bude uchovávat kapacitu hrany u -> v
		int[,] capacity = new int[N, N];

		// Načtení M hran a uložení do matice kapacit
		for (int i = 0; i < M; i++)
		{
			line = Console.ReadLine().Split();
			int from = int.Parse(line[0]);
			int to = int.Parse(line[1]);
			int cap = int.Parse(line[2]);
			capacity[from, to] += cap;
			// Pokud se ve vstupu opakují stejné hrany (from->to), 
			// sčítáme kapacity (proto +=).
		}

		// Výpočet max flow přes Edmonds-Karp
		int maxFlow = EdmondsKarp(capacity, S, T);

		// Výsledek
		Console.WriteLine(maxFlow);
	}

	/// <summary>
	/// Implementace Edmonds-Karp algoritmu.
	/// Vrátí maximální možný tok z S do T.
	/// </summary>
	private static int EdmondsKarp(int[,] capacity, int s, int t)
	{
		int N = capacity.GetLength(0);

		// Reziduální kapacita (zpočátku shodná s původní kapacitou)
		int[,] residual = new int[N, N];
		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < N; j++)
			{
				residual[i, j] = capacity[i, j];
			}
		}

		int maxFlow = 0;

		// parent[v] si budeme pamatovat, odkud jsme do vrcholu v při BFS přišli
		int[] parent = new int[N];

		// Dokud existuje augmentační cesta z s do t, zvětšujeme flow
		while (BfsFindPath(residual, s, t, parent))
		{
			// Najdeme minimální zbývající kapacitu na nalezené cestě
			int pathFlow = int.MaxValue;
			int v = t;
			while (v != s)
			{
				int u = parent[v];
				pathFlow = Math.Min(pathFlow, residual[u, v]);
				v = u;
			}

			// Aktualizujeme reziduální graf
			v = t;
			while (v != s)
			{
				int u = parent[v];
				residual[u, v] -= pathFlow;
				residual[v, u] += pathFlow; // zpětná hrana
				v = u;
			}

			// Přičteme k celkovému toku
			maxFlow += pathFlow;
		}

		return maxFlow;
	}

	/// <summary>
	/// BFS, který hledá augmentační cestu v reziduálním grafu.
	/// parent[v] nastavíme na index vrcholu, ze kterého jsme do v přišli.
	/// Vrací true, pokud cestu našel (v takovém případě je pole parent vyplněno).
	/// </summary>
	private static bool BfsFindPath(int[,] residual, int s, int t, int[] parent)
	{
		int N = residual.GetLength(0);
		bool[] visited = new bool[N];

		for (int i = 0; i < N; i++)
			parent[i] = -1;

		// Fronta pro BFS
		Queue<int> queue = new Queue<int>();
		queue.Enqueue(s);
		visited[s] = true;
		parent[s] = -1;

		while (queue.Count > 0)
		{
			int u = queue.Dequeue();

			// Projdeme všechny sousedy u
			for (int v = 0; v < N; v++)
			{
				// Pokud zbývající kapacita > 0 a není navštíveno
				if (!visited[v] && residual[u, v] > 0)
				{
					queue.Enqueue(v);
					parent[v] = u;
					visited[v] = true;

					// Pokud jsme došli do T, můžeme BFS ukončit
					if (v == t)
					{
						return true;
					}
				}
			}
		}

		return visited[t];
	}
}
