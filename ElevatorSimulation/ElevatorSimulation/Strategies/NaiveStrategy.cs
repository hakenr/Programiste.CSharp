namespace ElevatorSimulation.Strategies;

/// <summary>
/// Naive strategy that always targets the first request in the queue (FIFO).
/// Decides to open doors when at target, otherwise moves toward the first request.
/// </summary>
public class NaiveStrategy : IElevatorStrategy
{
	/// <inheritdoc />
	public MoveResult DecideNextMove(ElevatorSystem elevator)
	{
		// Check for passengers to drop off at this floor
		if (elevator.ActiveRiders.Any(r => r.To == elevator.CurrentElevatorFloor))
		{
			return MoveResult.OpenDoors;
		}

		// Check for passengers to pick up at this floor
		if (elevator.PendingRequests.Any(r => r.From == elevator.CurrentElevatorFloor))
		{
			return MoveResult.OpenDoors;
		}

		// If no requests, stay idle
		if (elevator.PendingRequests.Count == 0 && elevator.ActiveRiders.Count == 0)
		{
			return MoveResult.NoAction;
		}

		// Naive FIFO: always target the first pending request
		// If we have active riders, prioritize dropping them off first
		int targetFloor;
		if (elevator.ActiveRiders.Count > 0)
		{
			// Find nearest dropoff
			targetFloor = elevator.ActiveRiders
				.Select(r => r.To)
				.MinBy(f => Math.Abs(f - elevator.CurrentElevatorFloor));
		}
		else
		{
			// Target the first pending request
			var firstRequest = elevator.PendingRequests.First();
			targetFloor = firstRequest.From;
		}

		return MoveTowards(elevator.CurrentElevatorFloor, targetFloor);
	}

	private static MoveResult MoveTowards(int currentFloor, int targetFloor)
	{
		return (targetFloor - currentFloor) switch
		{
			> 0 => MoveResult.MoveUp,
			< 0 => MoveResult.MoveDown,
			_ => MoveResult.NoAction,
		};
	}
}