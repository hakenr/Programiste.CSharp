namespace ElevatorSimulation.Strategies;

/// <summary>
/// Optimizing strategy that always moves toward the nearest target (pickup or dropoff).
/// Minimizes total travel distance by selecting closest floor with pending action.
/// </summary>
public class OptimizingStrategy : IElevatorStrategy
{
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

		// Find nearest target floor (either for pickup or dropoff)
		var targetFloors = elevator.PendingRequests.Select(r => r.From)
			.Concat(elevator.ActiveRiders.Select(r => r.To))
			.ToList();

		var nearestFloor = targetFloors.MinBy(f => Math.Abs(f - elevator.CurrentElevatorFloor));
		return MoveTowards(elevator.CurrentElevatorFloor, nearestFloor);
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
