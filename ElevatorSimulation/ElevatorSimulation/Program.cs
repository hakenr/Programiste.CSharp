namespace ElevatorSimulation;

public static class Program
{
	public static void Main()
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8;
		Console.WriteLine("=== ELEVATOR SIMULATION ===\n");

		var building = new Building(minFloor: 0, maxFloor: 9);
		var random = new Random(14); // Fixed seed for reproducibility

		// Test different strategies
		RunSimulation("NAIVE STRATEGY", new NaiveStrategy(), building, random, simulationSteps: 50);
		Console.WriteLine("\n");
		RunSimulation("DIRECTIONAL STRATEGY", new DirectionalStrategy(), building, random, simulationSteps: 50);
		Console.WriteLine("\n");
		RunSimulation("OPTIMIZING STRATEGY", new OptimizingStrategy(), building, random, simulationSteps: 50);
	}

	private static void RunSimulation(string strategyName, IElevatorStrategy strategy, Building building, Random random, int simulationSteps)
	{
		Console.WriteLine($"\n{new string('=', 60)}");
		Console.WriteLine($"  {strategyName}");
		Console.WriteLine(new string('=', 60));

		var elevator = new Elevator(strategy, building);
		int time = 0;
		int requestCount = 0;

		while (time < simulationSteps)
		{
			// Generate random requests (20% probability per step)
			if (random.NextDouble() < 0.2)
			{
				var req = building.CreateRandomRequest(random, time);
				elevator.AddRequest(req);
				requestCount++;
				Console.WriteLine($"[{time:00}] 📞 Request #{requestCount}: floor {req.From} → {req.To}");
			}

			elevator.Step(time);
			time++;
		}

		// Handle remaining requests
		Console.WriteLine($"\n[--] Processing remaining requests...");
		while (elevator.PendingRequests.Count > 0 || elevator.ActiveRequest != null)
		{
			elevator.Step(time);
			time++;

			// Safety limit to prevent infinite loops
			if (time > simulationSteps + 200)
			{
				Console.WriteLine($"[--] ⚠️ Timeout: stopping simulation with {elevator.PendingRequests.Count} pending requests");
				break;
			}
		}

		Console.WriteLine($"\n[{time:00}] ✅ Simulation completed");
		elevator.Statistics.PrintSummary();
	}
}
