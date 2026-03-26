namespace ElevatorSimulation;

/// <summary>
/// Holds results for a single strategy across multiple simulations.
/// </summary>
public class StrategyResult
{
	public string StrategyName { get; set; }
	public int CompletedRequests { get; set; }
	public double AverageWaitTime { get; set; }
	public double AverageTravelTime { get; set; }
	public double AverageTotalTime { get; set; }
	public int TotalCumulativeTime { get; set; }

	/// <summary>
	/// Creates a result from collected statistics.
	/// </summary>
	public static StrategyResult FromStatistics(string strategyName, Statistics stats)
	{
		return new StrategyResult
		{
			StrategyName = strategyName,
			CompletedRequests = stats.CompletedCount,
			AverageWaitTime = stats.AverageWaitTime,
			AverageTravelTime = stats.AverageTravelTime,
			AverageTotalTime = stats.AverageTotalTime,
			TotalCumulativeTime = stats.TotalCumulativeTime
		};
	}
}
