using ElevatorSimulation.Strategies;

namespace ElevatorSimulation;

public static class Program
{
	public const int TimeForRequests = 20;
	public const int RandomSeed = 42017;
	public const int MaxFloor = 9;
	public const double RequestDensityPercent = 0.30;

	// Tournament configuration
	public const bool TournamentMode = true; // Set to false for single strategy testing
	public static readonly int[] TournamentSeeds = { 42017, 12345, 99999, 54321, 77777 };

	public static void Main()
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8;
		Console.WriteLine("=== ELEVATOR SIMULATION ===\n");

		var building = new Building(minFloor: 0, maxFloor: MaxFloor);

		if (TournamentMode)
		{
			RunTournament(building);
		}
		else
		{
			// Test single strategy
			RunSimulation("FIFO STRATEGY", new FifoStrategy(), building, seed: RandomSeed);
			Console.WriteLine("\n");
			RunSimulation("NEAREST FIRST STRATEGY", new NearestFirstStrategy(), building, seed: RandomSeed);
			//Console.WriteLine("\n");
			//RunSimulation("STRATEGY 2", new Strategy2(), building, seed: RandomSeed);
		}
	}

	/// <summary>
	/// Runs a tournament with all discovered strategies.
	/// </summary>
	private static void RunTournament(Building building)
	{
		Console.WriteLine("🏁 STARTING STRATEGY TOURNAMENT");
		Console.WriteLine($"   Testing with {TournamentSeeds.Length} different scenarios (seeds)");
		Console.WriteLine();

		// Discover all strategies automatically
		var strategies = StrategyTournament.DiscoverStrategies();

		if (strategies.Count == 0)
		{
			Console.WriteLine("❌ No strategies found! Make sure you have classes implementing IElevatorStrategy.");
			return;
		}

		Console.WriteLine($"📋 Found {strategies.Count} strategies:");
		foreach (var (name, _) in strategies)
		{
			Console.WriteLine($"   - {name}");
		}
		Console.WriteLine();

		// Run tournament
		var tournament = new StrategyTournament(building, TournamentSeeds);
		var results = tournament.RunTournament(strategies);

		// Print results
		StrategyTournament.PrintTournamentResults(results);
	}

	private static void RunSimulation(string strategyName, IElevatorStrategy strategy, Building building, int seed)
	{
		Console.WriteLine(new string('=', 60));
		Console.WriteLine($"  {strategyName}");
		Console.WriteLine(new string('=', 60));

		var random = new Random(seed);

		var elevator = new ElevatorSystem(strategy, building);

		// Generate random requests (some may be null)
		var randomRequests = Enumerable.Range(0, TimeForRequests)
			.Select(_ => GenerateRandomRequest(building, random))
			.ToList();

		var requestEnumerator = randomRequests.GetEnumerator();
		int requestNumber = 0;

		while (true)
		{
			RiderRequest request = null;
			if (requestEnumerator.MoveNext())
			{
				request = requestEnumerator.Current;
			}

			if (request is not null)
			{
				requestNumber++;
				Console.WriteLine($"[{elevator.CurrentTime:00}] 📞 Request #{requestNumber}: floor {request.From} → {request.To}");
				elevator.ReceiveRequest(request);
			}

			var moveResult = elevator.TickOneTimeUnit();

			// Stop when no more requests and elevator is idle
			if ((elevator.CurrentTime >= TimeForRequests)
				&& (elevator.PendingRequests.Count == 0)
				&& (elevator.ActiveRiders.Count == 0))
			{
				break;
			}
		}

		Console.WriteLine($"\n[{elevator.CurrentTime:00}] ✅ Simulation completed");
		elevator.Statistics.PrintSummary();
	}

	private static RiderRequest GenerateRandomRequest(Building building, Random random)
	{
		if (random.NextDouble() > RequestDensityPercent)
		{
			return null; // no request this tick
		}

		return building.CreateRandomRequest(random, 0); // Time will be set by elevator
	}
}
