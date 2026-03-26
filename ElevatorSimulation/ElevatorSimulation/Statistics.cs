namespace ElevatorSimulation;

/// <summary>
/// Tracks and calculates statistics for elevator simulation.
/// </summary>
public class Statistics
{
	private readonly List<RiderRequest> _completedRequests = new();

	/// <summary>
	/// Registers a completed request for statistics tracking.
	/// </summary>
	public void RecordCompletedRequest(RiderRequest request)
	{
		if (request.CompletedAt.HasValue)
		{
			_completedRequests.Add(request);
		}
	}

	/// <summary>
	/// Gets the average waiting time (time from request creation to pickup).
	/// </summary>
	public virtual double AverageWaitTime
	{
		get
		{
			if (_completedRequests.Count == 0) return 0;
			return _completedRequests
				.Where(r => r.PickedUpAt.HasValue)
				.Average(r => r.PickedUpAt!.Value - r.CreatedAt);
		}
	}

	/// <summary>
	/// Gets the average travel time (time from pickup to dropoff).
	/// </summary>
	public virtual double AverageTravelTime
	{
		get
		{
			if (_completedRequests.Count == 0) return 0;
			return _completedRequests
				.Where(r => r.PickedUpAt.HasValue && r.CompletedAt.HasValue)
				.Average(r => r.CompletedAt!.Value - r.PickedUpAt!.Value);
		}
	}

	/// <summary>
	/// Gets the average total time (from request creation to completion).
	/// </summary>
	public virtual double AverageTotalTime
	{
		get
		{
			if (_completedRequests.Count == 0) return 0;
			return _completedRequests
				.Where(r => r.CompletedAt.HasValue)
				.Average(r => r.CompletedAt!.Value - r.CreatedAt);
		}
	}

	/// <summary>
	/// Gets the number of completed requests.
	/// </summary>
	public virtual int CompletedCount => _completedRequests.Count;

	/// <summary>
	/// Gets the total cumulative time (sum of all total times).
	/// This is the primary metric for comparing strategies.
	/// </summary>
	public virtual int TotalCumulativeTime
	{
		get
		{
			if (_completedRequests.Count == 0) return 0;
			return _completedRequests
				.Where(r => r.CompletedAt.HasValue)
				.Sum(r => r.CompletedAt!.Value - r.CreatedAt);
		}
	}

	/// <summary>
	/// Prints a summary of statistics to the console.
	/// </summary>
	public void PrintSummary()
	{
		Console.WriteLine("\n" + new string('=', 50));
		Console.WriteLine("SIMULATION STATISTICS");
		Console.WriteLine(new string('=', 50));
		Console.WriteLine($"Completed requests:      {CompletedCount}");
		Console.WriteLine($"Average wait time:       {AverageWaitTime:F2} steps");
		Console.WriteLine($"Average travel time:     {AverageTravelTime:F2} steps");
		Console.WriteLine($"Average total time:      {AverageTotalTime:F2} steps");
		Console.WriteLine($"Total cumulative time:   {TotalCumulativeTime} steps");
		Console.WriteLine(new string('=', 50));
	}

	/// <summary>
	/// Creates a Statistics object with pre-calculated aggregated values.
	/// Used for combining statistics from multiple simulation runs.
	/// </summary>
	internal static Statistics CreateAggregated(int completedCount, double avgWait, double avgTravel, double avgTotal, int totalCumulative)
	{
		return new AggregatedStatistics(completedCount, avgWait, avgTravel, avgTotal, totalCumulative);
	}

	/// <summary>
	/// Internal class for storing pre-calculated aggregated statistics.
	/// </summary>
	private class AggregatedStatistics : Statistics
	{
		private readonly int _completedCount;
		private readonly double _avgWait;
		private readonly double _avgTravel;
		private readonly double _avgTotal;
		private readonly int _totalCumulative;

		public AggregatedStatistics(int completedCount, double avgWait, double avgTravel, double avgTotal, int totalCumulative)
		{
			_completedCount = completedCount;
			_avgWait = avgWait;
			_avgTravel = avgTravel;
			_avgTotal = avgTotal;
			_totalCumulative = totalCumulative;
		}

		public override double AverageWaitTime => _avgWait;
		public override double AverageTravelTime => _avgTravel;
		public override double AverageTotalTime => _avgTotal;
		public override int CompletedCount => _completedCount;
		public override int TotalCumulativeTime => _totalCumulative;
	}
}
