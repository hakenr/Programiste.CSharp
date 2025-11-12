namespace ElevatorSimulation;

/// <summary>
/// Naive strategy that always selects the first request in the queue (FIFO).
/// This is the simplest possible strategy with no optimization.
/// </summary>
public class NaiveStrategy : IElevatorStrategy
{
	/// <inheritdoc />
	public Request SelectNext(Elevator elevator)
		=> elevator.PendingRequests.FirstOrDefault();
}